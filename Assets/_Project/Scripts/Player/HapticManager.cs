using UnityEngine;

public static class HapticManager
{
    public static bool Enabled = true;
    public static float Intensity = 1f;

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
                try
                {
                    using (var cls = new AndroidJavaClass("android.os.VibrationEffect"))
                    {
                        int amp = (int)Mathf.Clamp(80f * Intensity, 1f, 255f);
                        using (var eff = cls.CallStatic<AndroidJavaObject>("createOneShot",
                            Mathf.Clamp((int)(milliseconds * Intensity), 10, 500), amp))
                        {
                            vib.Call("vibrate", eff);
                            return;
                        }
                    }
                }
                catch { }
                vib.Call("vibrate", Mathf.Clamp((int)(milliseconds * Intensity), 10, 500));
            }
        }
        catch (System.Exception e) { Debug.LogWarning("[Haptic] " + e.Message); }
#endif
    }
    public static void LightHit() => Vibrate(15);
    public static void HeavyHit() => Vibrate(60);
    public static void Hurt() => Vibrate(45);
    public static void Block() => Vibrate(30);
}
