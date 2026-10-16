using System;

// Kayit semasi v1 - versiyon numarali; yeni alanlar eklendikce SaveVersion artar
[Serializable]
public class GameData
{
    public int SaveVersion = 1;

    // Ilerleme
    public int CurrentBattle = 1;          // 1-100
    public int Gold = 0;
    public int Xp = 0;
    public int Level = 1;

    // Karakter
    public string SelectedGladiator = "Ağır Savaşçı";
    public float PlayerHealthMax = 100f;
    public float PlayerStaminaMax = 100f;

    // Ekipman (Faz 9'da doldurulur; sema yerinde dursun)
    public string EquippedWeapon = "";
    public string EquippedArmor = "";
    public System.Collections.Generic.List<string> OwnedEquipment = new System.Collections.Generic.List<string>();

    // Ilerleme bayraklari
    public bool TutorialDone = false;
    public bool IntroSeen = false;

    // Ayarlar (kayit v1 icinde; SettingsManager bunu kullanir)
    public float VolumeMaster = 1f;
    public float VolumeMusic = 0.8f;
    public float VolumeEffects = 1f;
    public float HapticIntensity = 1f;     // 0=kapali, 1=hafif..3=güçlü ölçekli
    public bool HapticsEnabled = true;
    public int GraphicsProfile = 1;        // 0=cok dusuk..4=ultra
    public int TargetFps = 60;
    public float CameraSensitivity = 1f;
    public bool CameraShakeEnabled = true;

    public GameData DeepCopy()
    {
        var copy = (GameData)System.Activator.CreateInstance(GetType());
        var json = JsonUtility.ToJson(this);
        JsonUtility.FromJsonOverwrite(json, copy);
        return copy;
    }
}
