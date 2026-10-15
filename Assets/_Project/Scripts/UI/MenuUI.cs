using UnityEngine;
using UnityEngine.UI;

// Ana menu - kodla kurulur; butonlar editor'de degil script'te baglanir
public class MenuUI : MonoBehaviour
{
    public void OnNewGame()
    {
        Debug.Log("[Menu] Yeni Oyun -> Arena sahnesi");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Boot");
    }

    public void OnContinue()
    {
        // Faz 9'da kayit sistemiyle doldurulacak; simdi arenaya gonderir
        OnNewGame();
    }

    public void OnAbout()
    {
        var aboutPanel = transform.Find("AboutPanel");
        if (aboutPanel != null) aboutPanel.gameObject.SetActive(true);
    }

    public void OnCloseAbout()
    {
        var aboutPanel = transform.Find("AboutPanel");
        if (aboutPanel != null) aboutPanel.gameObject.SetActive(false);
    }
}
