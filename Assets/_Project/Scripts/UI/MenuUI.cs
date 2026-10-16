using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public void OnNewGame()
    {
        Debug.Log("[Menu] Yeni Oyun - LoadScene(1)");
        SceneManager.LoadScene(1);
    }
    public void OnContinue() { OnNewGame(); }
    public void OnAbout()
    {
        var p = transform.Find("AboutPanel");
        if (p != null) p.gameObject.SetActive(true);
    }
    public void OnCloseAbout()
    {
        var p = transform.Find("AboutPanel");
        if (p != null) p.gameObject.SetActive(false);
    }
}
