using UnityEngine;

// Haptik yonetici - kademeli titresim (13-profil planinin temel hali)
// Ayar: GameData.HapticsEnabled + HapticIntensity (Faz 9'da tam baglanti)
public static class HapticManager
{
    public static bool Enabled = true;
    public static float Intensity = 1f; // 0.5 hafif / 1 normal / 1.5 guclu

    public static void Vibrate(int milliseconds)
    {
        if (!Enabled) return;
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (var up = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = up.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var vib = activity.Call<AndroidJavaObject>("getSystemService", "vibrator"))
            {
                vib.Call("vibrate", Mathf.Clamp((int)(milliseconds * Intensity), 10, 500));
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("[Haptic] Desteklenmiyor: " + e.Message);
        }
#endif
    }

    // Hafif vurus (15ms)
    public static void LightHit() => Vibrate(15);
    // Agir vurus (60ms)
    public static void HeavyHit() => Vibrate(60);
    // Kalkan blok (30ms - tok)
    public static void Block() => Vibrate(30);
    // Darbe alma (45ms)
    public static void Hurt() => Vibrate(45);
    // Kritik (80ms)
    public static void Critical() => Vibrate(80);
    // Bitirici (120ms)
    public static void Finisher() => Vibrate(120);
}
