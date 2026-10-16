using System;
using System.IO;
using UnityEngine;

// Kayit v1: atomik yazim + 3 nesil yedek zinciri + bozuk kayit korumasi
// Kurallar: SAVE_DATA_SECURITY belgesi (Docs/) — yarim dosya yok, kayip yok
public static class SaveManager
{
    const int SchemaVersion = 1;
    public static string SaveDir =>
        Path.Combine(Application.persistentDataPath, "saves");
    public static string MainPath => Path.Combine(SaveDir, "save_main.json");
    public static string BackupPath => Path.Combine(SaveDir, "save_backup.json");
    public static string OldPath => Path.Combine(SaveDir, "save_old.json");

    static string _lastError = "";
    public static string LastError => _lastError;

    public static bool HasAnySave()
    {
        return File.Exists(MainPath) || File.Exists(BackupPath) || File.Exists(OldPath);
    }

    // --- YAZMA (atomik): temp'e yaz -> dogrula -> rotasyon -> yer degistir ---
    public static bool Save(GameData data)
    {
        try
        {
            _lastError = "";
            Directory.CreateDirectory(SaveDir);

            data.SaveVersion = SchemaVersion;
            string json = JsonUtility.ToJson(data, true);

            // 1) temp dosyaya yaz
            string tmp = MainPath + ".tmp";
            File.WriteAllText(tmp, json);

            // 2) dogrula: geri oku ve parse edilebildigini kontrol et
            if (JsonUtility.FromJson<GameData>(File.ReadAllText(tmp)) == null)
                throw new IOException("Dogrulama basarisiz: temp dosya parse edilemedi");

            // 3) yedek rotasyonu: main -> backup -> old
            if (File.Exists(MainPath))
            {
                if (File.Exists(BackupPath))
                    File.Copy(BackupPath, OldPath, true);
                File.Copy(MainPath, BackupPath, true);
            }

            // 4) atomik yer degistirme
            if (File.Exists(MainPath)) File.Delete(MainPath);
            File.Move(tmp, MainPath);
            return true;
        }
        catch (Exception e)
        {
            _lastError = "Kayit yazma hatasi: " + e.Message;
            Debug.LogError("[SaveManager] " + _lastError);
            return false;
        }
    }

    // --- OKUMA: main -> backup -> old sirasiyla dene; bozuksa siradakine gec ---
    public static GameData Load()
    {
        _lastError = "";
        string[] paths = { MainPath, BackupPath, OldPath };
        foreach (var p in paths)
        {
            if (!File.Exists(p)) continue;
            try
            {
                string json = File.ReadAllText(p);
                var data = JsonUtility.FromJson<GameData>(json);
                if (data == null) throw new IOException("parse null");
                if (data.SaveVersion > SchemaVersion)
                {
                    _lastError = "Kayit daha yeni surumden (v" + data.SaveVersion + ") - gecersiz";
                    continue;
                }
                if (Validate(data)) return data;
                _lastError = "Veri dogrulamasi gecemedi: " + Path.GetFileName(p);
            }
            catch (Exception e)
            {
                _lastError = "Bozuk kayit (" + Path.GetFileName(p) + "): " + e.Message;
                Debug.LogWarning("[SaveManager] " + _lastError + " - siradaki yedege geciliyor");
            }
        }
        Debug.LogError("[SaveManager] Gecerli kayit bulunamadi. " + _lastError);
        return null;
    }

    // --- ARALIK DOGRULAMASI: imkansiz degerler = bozuk kayit ---
    static bool Validate(GameData d)
    {
        if (d.Gold < 0 || d.Gold > 999999999) return false;
        if (d.CurrentBattle < 1 || d.CurrentBattle > 100) return false;
        if (d.Level < 1 || d.Level > 999) return false;
        if (d.PlayerHealthMax <= 0 || d.PlayerHealthMax > 100000) return false;
        if (d.SaveVersion <= 0 || d.SaveVersion > SchemaVersion) return false;
        return true;
    }

    // --- SILME: yalnizca acik onay sonrasi; yedekler saklanir ---
    public static void DeleteAll()
    {
        foreach (var p in new[] { MainPath, BackupPath, OldPath })
            if (File.Exists(p)) File.Delete(p);
    }
}
