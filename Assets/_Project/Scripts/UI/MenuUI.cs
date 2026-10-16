using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    void Start()
    {
        Bind("BtnNew", OnNewGame);
        Bind("BtnAbout", ShowAbout);
        var p = transform.Find("AboutPanel");
        if (p != null)
        {
            var c = p.Find("BtnCloseAbout");
            if (c != null) c.GetComponent<Button>().onClick.AddListener(HideAbout);
        }
    }

    void Bind(string n, UnityEngine.Events.UnityAction a)
    {
        var t = transform.Find(n);
        if (t != null) t.GetComponent<Button>().onClick.AddListener(a);
        else Debug.LogWarning("[MenuUI] Buton yok: " + n);
    }

    public void OnNewGame()
    {
        Debug.Log("[Menu] Yeni Oyun -> Arena");
        SceneManager.LoadScene(1);
    }
    public void ShowAbout()
    {
        var p = transform.Find("AboutPanel");
        if (p != null) p.gameObject.SetActive(true);
    }
    public void HideAbout()
    {
        var p = transform.Find("AboutPanel");
        if (p != null) p.gameObject.SetActive(false);
    }
    public void OnAbout() => ShowAbout();
    public void OnCloseAbout() => HideAbout();
}
