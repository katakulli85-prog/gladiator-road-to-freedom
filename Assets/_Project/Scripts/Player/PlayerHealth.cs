using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float healthMax = 100f;
    public GameObject defeatPanel;
    public PlayerCombat combat;
    bool dead;

    public void TakeDamage(float dmg)
    {
        if (dead) return;
        if (combat != null && combat.IsParrying())
        {
            Debug.Log("[Player] PARRY! Hasar bloklandi - karsi saldiri firsati");
            HapticManager.Block();
            var enemy = Object.FindObjectOfType<EnemyAI>();
            if (enemy != null) enemy.Stun(1.2f);
            return;
        }
        health -= dmg;
        HapticManager.Hurt();
        ScreenShake.Shake(0.08f, 0.12f);
        if (health <= 0)
        {
            dead = true;
            if (defeatPanel != null) defeatPanel.SetActive(true);
            var m = GetComponent<PlayerMotor>(); if (m != null) m.enabled = false;
            var c = GetComponent<PlayerCombat>(); if (c != null) c.enabled = false;
        }
    }
}
