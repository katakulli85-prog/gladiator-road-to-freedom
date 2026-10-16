using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Hasar")]
    public float lightDamage = 12f;
    public float heavyDamage = 28f;
    public float comboMultiplier = 1.6f;
    public float comboWindow = 0.6f;
    public float attackRange = 2.4f;
    public float attackCooldown = 0.45f;

    [Header("Stamina")]
    public float stamina = 100f;
    public float staminaRegen = 12f;
    public float parryCost = 15f;

    [Header("Parry")]
    public float parryDuration = 0.35f;

    float comboStep;
    float comboTimer;
    float attackTimer;
    float parryTimer;

    public bool IsParrying() { return parryTimer > 0f; }

    EnemyAI NearestEnemy()
    {
        EnemyAI best = null;
        float bd = attackRange + 1.5f;
        foreach (var e in Object.FindObjectsOfType<EnemyAI>())
        {
            if (e == null || e.health <= 0f) continue;
            float d = Vector3.Distance(transform.position, e.transform.position);
            if (d < bd) { bd = d; best = e; }
        }
        return best;
    }

    void DoAttack(float dmg, bool heavy)
    {
        var e = NearestEnemy();
        if (e == null) return;
        Vector3 toE = e.transform.position - transform.position;
        toE.y = 0f;
        transform.rotation = Quaternion.LookRotation(toE.normalized);
        e.TakeDamage(dmg);
        ScreenShake.Shake(heavy ? 0.25f : 0.12f, heavy ? 0.25f : 0.12f);
        HapticManager.Vibrate(heavy ? 60 : 15);
    }

    public void TryLightAttack()
    {
        if (attackTimer > 0f) return;
        attackTimer = attackCooldown;
        comboTimer = comboWindow;
        float dmg = lightDamage * (comboStep >= 2f ? comboMultiplier : 1f);
        comboStep = (comboStep >= 2f) ? 0f : comboStep + 1f;
        DoAttack(dmg, false);
    }

    public void TryHeavyAttack()
    {
        if (attackTimer > 0f) return;
        attackTimer = attackCooldown + 0.25f;
        comboStep = 0f;
        DoAttack(heavyDamage, true);
    }

    public void TryParry()
    {
        if (stamina < parryCost) return;
        stamina -= parryCost;
        parryTimer = parryDuration;
        HapticManager.Vibrate(30);
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        parryTimer -= Time.deltaTime;
        if (comboTimer > 0f) comboTimer -= Time.deltaTime;
        else comboStep = 0f;
        stamina = Mathf.Min(stamina + staminaRegen * Time.deltaTime, 100f);
    }
}
