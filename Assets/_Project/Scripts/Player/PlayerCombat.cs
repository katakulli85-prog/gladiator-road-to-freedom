using UnityEngine;

// Oyuncu saldiri sistemi - Temel hali (kombo/kaçınma sonraki turlarda)
// Kurallar: tek hasar per vurus, silah temas olmadan hasar yok (6. belge)
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
    float attackTimer;         // vurus penceresi acik mi
    float pendingDamage;
    float pendingRange;
    bool damageDealt;          // tek hasar kurali

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

    // Butonlardan cagrilacak (TUR 4'te HUD butonlarina baglanir)
    public void TryLightAttack()
    {
        if (CanAttack())
        {
            StartAttack(lightDamage, lightRange, lightStaminaCost, 0.18f);
            HapticManager.LightHit();
        }
    }

    public void TryHeavyAttack()
    {
        if (CanAttack())
        {
            StartAttack(heavyDamage, heavyRange, heavyStaminaCost, 0.32f);
            HapticManager.HeavyHit();
        }
    }

    bool CanAttack()
    {
        return cooldownTimer <= 0 && attackTimer <= 0 && stamina >= lightStaminaCost;
    }

    void StartAttack(float dmg, float range, float cost, float duration)
    {
        stamina -= cost;
        cooldownTimer = attackCooldown;
        attackTimer = duration;
        pendingDamage = dmg;
        pendingRange = range;
        damageDealt = false;
        Debug.Log($"[Combat] Saldiri: dmg={dmg} range={range} stamina={stamina:F0}");
    }

    void TryDealDamage()
    {
        // Onumuzdeki hedefi bul (basit: en yakin TestDummy, ileriye raycast sonra)
        var hits = Physics.OverlapSphere(transform.position + transform.forward * 1.2f, pendingRange);
        foreach (var h in hits)
        {
            var dummy = h.GetComponent<TestDummy>();
            if (dummy != null && !damageDealt)
            {
                dummy.TakeDamage(pendingDamage);
                damageDealt = true;
                Debug.Log($"[Combat] Hasar verildi: {pendingDamage}");
                break; // tek hasar kurali
            }
        }
    }
}
