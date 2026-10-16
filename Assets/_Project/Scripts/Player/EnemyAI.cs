using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.4f;
    public float attackRange = 1.7f;
    public float attackCooldown = 1.6f;
    public float attackDamage = 7f;
    public float health = 60f;
    public float healthMax = 60f;
    public Transform target;
    public GameObject victoryPanel;
    float cooldown;
    bool dead;
    float stunTimer;

    public void Stun(float dur)
    {
        stunTimer = dur;
        HapticManager.Critical();
        ScreenShake.Shake(0.1f, 0.15f);
    }

    void Update()
    {
        if (dead || target == null) return;
        if (stunTimer > 0) { stunTimer -= Time.deltaTime; return; }
        cooldown -= Time.deltaTime;
        Vector3 toT = target.position - transform.position; toT.y = 0f;
        float dist = toT.magnitude;
        if (dist > attackRange)
        {
            transform.position += toT.normalized * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toT.normalized), 8f * Time.deltaTime);
        }
        else if (cooldown <= 0)
        {
            cooldown = attackCooldown;
            var ph = target.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (dead) return;
        health -= dmg;
        if (health <= 0)
        {
            dead = true;
            if (victoryPanel != null) victoryPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
