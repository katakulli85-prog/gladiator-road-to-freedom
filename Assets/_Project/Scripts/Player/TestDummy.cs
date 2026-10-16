using UnityEngine;

// Test hedefi - hasar alir, cani biterse devre disi kalir
public class TestDummy : MonoBehaviour
{
    public float health = 50f;
    public float healthMax = 50f;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log($"[Dummy] Hasar aldi: {dmg} | Kalan: {health:F0}");
        HapticManager.Hurt();

        if (health <= 0)
        {
            Debug.Log("[Dummy] Oldu - devre disi");
            gameObject.SetActive(false);
        }
    }
}
