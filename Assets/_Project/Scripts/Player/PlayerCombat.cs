using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Hasar")]
    public float lightDamage = 12f;
    public float heavyDamage = 28f;
    [Header("Menzil")]
    public float lightRange = 2.2f;
    public float heavyRange = 2.6f;
    [Header("Zamanlama")]
    public float attackCooldown = 0.45f;
    public float comboWindow = 0.6f;
    [Header("Dayaniklilik")]
    public float staminaMax = 100f;
    public float stamina = 100f;
    public float lightStaminaCost = 10f;
    public float heavyStaminaCost = 25f;
    public float staminaRegen = 18f;
    [Header("Parry")]
    public float parryDuration = 0.35f;
    public float parryCost = 15f;

    float cooldownTimer;
    float attackTimer;
    float pendingDamage;
    float pendingRange;
    bool damageDealt;
    int comboStep;
    float comboTimer;
    bool parryActive;
    float parryTimer;
    [HideInInspector] public bool enemyStunned;

    void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        if (parryActive) { parryTimer -= Time.deltaTime; if (parryTimer <= 0) parryActive = false; }
        if (comboTimer > 0) { comboTimer -= Time.deltaTime; if (comboTimer <= 0) comboStep = 0; }
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (!damageDealt) TryDealDamage();
            if (attackTimer <= 0) { attackTimer = 0; damageDealt = false; }
        }
        if (attackTimer <= 0 && cooldownTimer <= 0 && stamina < staminaMax)
            stamina = Mathf.Min(staminaMax, stamina + staminaRegen * Time.deltaTime);
    }

    public void TryLightAttack()
    {
        if (!CanAttack()) return;
        bool combo3 = comboStep == 2;
        float dmg = combo3 ? lightDamage * 1.6f : lightDamage;
        string tag = combo3 ? "KOMBO BITIRICI!" : (comboStep > 0 ? "KOMBO x" + (comboStep + 1) : "HAFIF");
        StartAttack(dmg, lightRange, lightStaminaCost, 0.18f, 15);
        comboStep = (comboStep + 1) % 3;
        comboTimer = comboWindow;
        Debug.Log($"[Combat] {tag} dmg={dmg:F0}");
    }

    public void TryHeavyAttack()
    {
        if (!CanAttack()) return;
        StartAttack(heavyDamage, heavyRange, heavyStaminaCost, 0.32f, 60);
        comboStep = 0;
        ScreenShake.Shake(0.06f, 0.12f);
    }

    public void TryParry()
    {
        if (parryActive || cooldownTimer > 0 || stamina < parryCost) return;
        stamina -= parryCost;
        parryActive = true;
        parryTimer = parryDuration;
        HapticManager.Block();
        Debug.Log("[Combat] PARRY aktif 0.35sn");
    }

    public bool IsParrying() { return parryActive; }

    public void OnParried() // düsman tarafindan cagrilir - karsi saldiri firsati
    {
        enemyStunned = true;
        Debug.Log("[Combat] Parry basarili! Dusman sersemledi");
    }

    bool CanAttack() { return cooldownTimer <= 0 && attackTimer <= 0 && stamina >= lightStaminaCost; }

    void StartAttack(float dmg, float range, float cost, float duration, int hapticMs)
    {
        var t = TargetLock.Nearest(transform, 8f);
        if (t != null)
        {
            var dir = t.position - transform.position; dir.y = 0f;
            if (dir.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(dir.normalized);
        }
        stamina -= cost;
        cooldownTimer = attackCooldown;
        attackTimer = duration;
        pendingDamage = dmg;
        pendingRange = range;
        damageDealt = false;
        HapticManager.Vibrate(hapticMs);
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
                break;
            }
        }
    }
}
