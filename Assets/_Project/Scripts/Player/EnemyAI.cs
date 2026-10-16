using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.4f;
    public float attackRange = 1.7f;
    public float attackCooldown = 1.2f;
    public float attackDamage = 10f;
    public float health = 60f;
    public float healthMax = 60f;
    public Transform target;
    public GameObject victoryPanel;

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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 8f * Time.deltaTime);
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
