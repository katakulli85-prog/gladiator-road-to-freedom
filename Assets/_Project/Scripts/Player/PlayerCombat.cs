using UnityEngine;

// Oyuncu saldiri sistemi - hedefe donus + ScreenShake + EnemyAI hasar
// Kurallar: tek hasar per vurus, saldiri penceresi icinde tek tetik
public class PlayerCombat : MonoBehaviour
{
    public float lightDamage = 12f;
    public float heavyDamage = 28f;
    public float lightRange = 2.2f;
    public float heavyRange = 2.6f;
    public float attackCooldown = 0.45f;
    public float staminaMax = 100f;
    public float stamina = 100f;
    public float lightStaminaCost = 10f;
    public float heavyStaminaCost = 25f;
    public float staminaRegen = 18f;

    float cooldownTimer;
    float attackTimer;
    float pendingDamage;
    float pendingRange;
    bool damageDealt;

    void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (!damageDealt) TryDealDamage();
            if (attackTimer <= 0)
            {
                attackTimer = 0;
                damageDealt = false;
            }
        }
        if (attackTimer <= 0 && cooldownTimer <= 0 && stamina < staminaMax)
            stamina = Mathf.Min(staminaMax, stamina + staminaRegen * Time.deltaTime);
    }

    public void TryLightAttack()
    {
        if (CanAttack()) StartAttack(lightDamage, lightRange, lightStaminaCost, 0.18f, 15);
    }

    public void TryHeavyAttack()
    {
        if (CanAttack()) StartAttack(heavyDamage, heavyRange, heavyStaminaCost, 0.32f, 60);
    }

    bool CanAttack()
    {
        return cooldownTimer <= 0 && attackTimer <= 0 && stamina >= lightStaminaCost;
    }

    void StartAttack(float dmg, float range, float cost, float duration, int hapticMs)
    {
        // Hedefe don (kilit v1)
        var t = TargetLock.Nearest(transform, 8f);
        if (t != null)
        {
            var dir = t.position - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(dir.normalized);
        }

        stamina -= cost;
        cooldownTimer = attackCooldown;
        attackTimer = duration;
        pendingDamage = dmg;
        pendingRange = range;
        damageDealt = false;
        HapticManager.Vibrate(hapticMs);
        Debug.Log($"[Combat] Saldiri: dmg={dmg} stamina={stamina:F0}");
    }

    void TryDealDamage()
    {
        var hits = Physics.OverlapSphere(transform.position + transform.forward * 1.2f, pendingRange);
        foreach (var h in hits)
        {
            var enemy = h.GetComponent<EnemyAI>();
            if (enemy != null && !damageDealt)
            {
                enemy.TakeDamage(pendingDamage);
                damageDealt = true;
                ScreenShake.Shake(0.06f, 0.1f);
                Debug.Log($"[Combat] Hasar verildi: {pendingDamage}");
                break; // tek hasar kurali
            }
        }
    }
}
