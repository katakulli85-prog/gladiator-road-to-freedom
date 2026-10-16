using UnityEngine;

// Oyuncu cani - olum: motor/kombat kapanir, YENILGI paneli acilir
public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float healthMax = 100f;
    bool dead;

    public void TakeDamage(float dmg)
    {
        if (dead) return;
        health -= dmg;
        HapticManager.Hurt();
        ScreenShake.Shake(0.08f, 0.12f);
        Debug.Log($"[Player] Hasar aldi: {dmg} | Kalan: {health:F0}");
        if (health <= 0)
        {
            dead = true;
            Debug.Log("[Player] Oldu - YENILGI");
            var dp = GameObject.Find("DefeatPanel");
            if (dp != null) dp.SetActive(true);
            var motor = GetComponent<PlayerMotor>();
            if (motor != null) motor.enabled = false;
            var combat = GetComponent<PlayerCombat>();
            if (combat != null) combat.enabled = false;
        }
    }
}
