using UnityEngine;

// Dusman AI v1 - agresif arketip (6. belge): yaklas -> menzilde vur
// Sonra: savunma/kaçınma davranışları, baskı/azalma profilleri (TUR 7+)
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.4f;
    public float attackRange = 1.7f;
    public float attackCooldown = 1.2f;
    public float attackDamage = 10f;
    public float health = 60f;
    public float healthMax = 60f;
    public Transform target;

    float cooldown;
    bool dead;

    void Update()
    {
        if (dead || target == null) return;
        cooldown -= Time.deltaTime;

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;
        float dist = toTarget.magnitude;

        if (dist > attackRange)
        {
            Vector3 dir = toTarget.normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(dir), 8f * Time.deltaTime);
        }
        else if (cooldown <= 0)
        {
            cooldown = attackCooldown;
            var ph = target.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(attackDamage);
            transform.rotation = Quaternion.LookRotation(toTarget.normalized);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (dead) return;
        health -= dmg;
        Debug.Log($"[Enemy] Hasar aldi: {dmg} | Kalan: {health:F0}");
        if (health <= 0)
        {
            dead = true;
            Debug.Log("[Enemy] Oldu - ZAFER");
            var vp = GameObject.Find("VictoryPanel");
            if (vp != null) vp.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
