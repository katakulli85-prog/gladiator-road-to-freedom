using UnityEngine;
using UnityEngine.UI;

// Savas HUD - can barlari (oyuncu + dusman)
// Sonra: stamina bar, kombo sayaci (TUR 3-4 zaten var)
public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    public Image playerBar;
    public Image enemyBar;
    public Text playerNum;
    public Text enemyNum;

    float lastPlayer = -1f;
    float lastEnemy = -1f;
    float playerMax = 100f;
    float enemyMax = 100f;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(float max)
    {
        playerMax = max;
        SetPlayer(max);
    }

    public void RegisterEnemy(float max)
    {
        enemyMax = max;
        SetEnemy(max);
    }

    void Update()
    {
        var ph = Object.FindObjectOfType<PlayerHealth>();
        if (ph != null && lastPlayer != ph.health)
        {
            lastPlayer = ph.health;
            SetPlayer(ph.health);
        }
        var en = Object.FindObjectOfType<EnemyAI>();
        if (en != null && lastEnemy != en.health)
        {
            lastEnemy = en.health;
            SetEnemy(en.health);
        }
    }

    void SetPlayer(float cur)
    {
        if (playerBar != null)
            playerBar.fillAmount = Mathf.Clamp01(cur / playerMax);
        if (playerNum != null)
            playerNum.text = Mathf.Max(0, cur).ToString("F0");
    }

    void SetEnemy(float cur)
    {
        if (enemyBar != null)
            enemyBar.fillAmount = Mathf.Clamp01(cur / enemyMax);
        if (enemyNum != null)
            enemyNum.text = Mathf.Max(0, cur).ToString("F0");
    }
}
EOF
