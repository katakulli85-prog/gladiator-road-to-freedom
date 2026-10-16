using NUnit.Framework;
using UnityEngine;

// EditMode testler - CI'da calisir (APK gerekmez)
public class SaveManagerTests
{
    [Test]
    public void SaveThenLoad_RoundTrip_PreservesData()
    {
        var d = new GameData { Gold = 500, CurrentBattle = 7, Level = 3, SelectedGladiator = "Test" };
        Assert.IsTrue(SaveManager.Save(d), "Save basarili olmali: " + SaveManager.LastError);
        var loaded = SaveManager.Load();
        Assert.IsNotNull(loaded, "Load null olmamali: " + SaveManager.LastError);
        Assert.AreEqual(500, loaded.Gold);
        Assert.AreEqual(7, loaded.CurrentBattle);
        Assert.AreEqual("Test", loaded.SelectedGladiator);
    }

    [Test]
    public void Save_Twice_BackupRotationWorks()
    {
        SaveManager.Save(new GameData { Gold = 100 });
        SaveManager.Save(new GameData { Gold = 200 });
        Assert.IsTrue(System.IO.File.Exists(SaveManager.BackupPath), "Backup dosyasi olusmali");
    }

    [Test]
    public void CorruptMainFile_FallsBackToBackup()
    {
        SaveManager.Save(new GameData { Gold = 100 });
        SaveManager.Save(new GameData { Gold = 200 });
        System.IO.File.WriteAllText(SaveManager.MainPath, "{bozuk-json!");
        var loaded = SaveManager.Load();
        Assert.IsNotNull(loaded, "Bozuk main -> yedekten yuklemeli");
        Assert.AreEqual(200, loaded.Gold, "Yedekten gelen veri son yazilan olmali");
    }

    [Test]
    public void ImpossibleValues_Rejected()
    {
        var d = new GameData { Gold = -50 }; // imkansiz: negatif altin
        SaveManager.Save(d);
        var loaded = SaveManager.Load();
        if (loaded != null) Assert.AreNotEqual(-50, loaded.Gold, "Negatif altin gecerli kayit olamaz");
    }
}
