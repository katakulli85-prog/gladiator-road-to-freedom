using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// EditMode testler - CI'da calisir (APK gerekmez)
public class SaveManagerTests
{
    // Her test temiz bir kayit klasoruyle baslar; testler birbirini etkilemez.
    [SetUp]
    public void SetUp() { SaveManager.DeleteAll(); }

    [TearDown]
    public void TearDown() { SaveManager.DeleteAll(); }

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
        SaveManager.Save(new GameData { Gold = 200 }); // main=200, backup=100
        System.IO.File.WriteAllText(SaveManager.MainPath, "{bozuk-json!");
        var loaded = SaveManager.Load();
        Assert.IsNotNull(loaded, "Bozuk main -> yedekten yuklemeli");
        // Yedek, bir onceki kayittir: 100. (Eski test 200 bekliyordu; bu mantiken imkansiz.)
        Assert.AreEqual(100, loaded.Gold, "Bozuk main sonrasi bir onceki kayit (backup) yuklenmeli");
    }

    [Test]
    public void ImpossibleValues_Rejected()
    {
        var d = new GameData { Gold = -50 }; // imkansiz: negatif altin
        SaveManager.Save(d);
        // Load burada bilerek Debug.LogError yazar; Test Framework bunu hata saymasin.
        LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex("Gecerli kayit bulunamadi"));
        var loaded = SaveManager.Load();
        Assert.IsNull(loaded, "Tek kayit gecersizse Load null donmeli");
    }
}
