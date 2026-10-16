using UnityEditor;
using UnityEngine;

// CI derleme kontrolu - APK uretmez, sadece script/asset dogrulamasi
public static class EditorCheck
{
    public static void RunCheck()
    {
        Debug.Log("[Check] Kod kontrolu basliyor - derleme zaten Unity acilisiyla yapildi");
        Debug.Log("[Check] Sahne dosyalari mevcut mu kontrol ediliyor...");
        if (!System.IO.File.Exists("Assets/_Project/Scenes/MainMenu.unity"))
            Debug.LogError("[Check] MainMenu.unity yok!");
        if (!System.IO.File.Exists("Assets/_Project/Scenes/Boot.unity"))
            Debug.LogError("[Check] Boot.unity yok!");
        Debug.Log("[Check] Kontrol tamam - hicbir hata yoksa Unity zaten derledi");
        Debug.Log("[Check] OK");
    }
}
