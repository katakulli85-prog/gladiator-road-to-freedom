using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArenaUI : MonoBehaviour
{
    void Start()
    {
        Bind("BtnBackMenu", () => SceneManager.LoadScene(0));
        Bind("BtnLight", () => { var pc = Object.FindObjectOfType<PlayerCombat>(); if (pc != null) pc.TryLightAttack(); });
        Bind("BtnHeavy", () => { var pc = Object.FindObjectOfType<PlayerCombat>(); if (pc != null) pc.TryHeavyAttack(); });
        Bind("BtnRetry", () => SceneManager.LoadScene(1));
        Bind("BtnRetryD", () => SceneManager.LoadScene(1));
        Bind("BtnMenuD", () => SceneManager.LoadScene(0));
    }

    void Bind(string n, UnityEngine.Events.UnityAction a)
    {
        var t = transform.Find(n);
        if (t != null) t.GetComponent<Button>().onClick.AddListener(a);
        else Debug.LogWarning("[ArenaUI] Buton yok: " + n);
    }
}
