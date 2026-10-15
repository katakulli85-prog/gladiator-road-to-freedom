using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

// Bulut derlemesinde calisir:
// 1) Test sahnesini kodla uretir (zemin + donen kup + yazilar + FPS sayaci)
// 2) Android APK'yi build/Android/Android.apk olarak derler
public static class BuildCommand
{
    const string ScenePath = "Assets/_Project/Scenes/Boot.unity";
    const string ApkPath = "build/Android/Android.apk";

    public static void PerformBuild()
    {
        Debug.Log("[Bootstrap] APK derlemesi basliyor...");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

        ApplyPlayerSettings();
        CreateBootScene();

        var options = new BuildPlayerOptions
        {
            scenes = new[] { ScenePath },
            locationPathName = ApkPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        Directory.CreateDirectory("build/Android");
        var report = BuildPipeline.BuildPlayer(options);
        var summary = report.summary;
        Debug.Log("[Bootstrap] Sonuc: " + summary.result +
                  " | Boyut: " + summary.totalSize + " byte | Hata: " + summary.totalErrors);

        if (summary.result != BuildResult.Succeeded)
        {
            EditorApplication.Exit(1);
        }
        Debug.Log("[Bootstrap] APK hazir.");
    }

    static void ApplyPlayerSettings()
    {
        PlayerSettings.companyName = "katakulli85-prog";
        PlayerSettings.productName = "Gladiator Road to Freedom";
        PlayerSettings.bundleVersion = "0.1.0";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.katakulli85prog.gladiatorrtf");
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
        PlayerSettings.Android.minSdkVersion = (AndroidSdkVersions)24;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
        PlayerSettings.allowedAutorotateToPortrait = false;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
        PlayerSettings.allowedAutorotateToLandscapeLeft = true;
        PlayerSettings.allowedAutorotateToLandscapeRight = true;
    }

    static void CreateBootScene()
    {
        Directory.CreateDirectory("Assets/_Project/Scenes");
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(5f, 1f, 5f);
        ground.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.72f, 0.63f, 0.47f));

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "RotatingCube";
        cube.transform.position = new Vector3(0f, 0.7f, 0f);
        cube.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.65f, 0.16f, 0.12f));
        cube.AddComponent<SpinCube>();

        var cameraGO = GameObject.Find("Main Camera");
        if (cameraGO != null) cameraGO.transform.position = new Vector3(0f, 2.2f, -5.5f);

        var canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);

        MakeLabel(canvasGO.transform, "TitleText", "GLADIATOR v0.1",
            new Vector2(0.5f, 1f), new Vector2(0f, -50f), 72);

        var fpsGO = MakeLabel(canvasGO.transform, "FpsText", "FPS: -",
            new Vector2(0.5f, 0f), new Vector2(0f, 30f), 48);
        fpsGO.AddComponent<FpsCounter>();

        EditorSceneManager.SaveScene(scene, ScenePath);
        Debug.Log("[Bootstrap] Test sahnesi uretildi: " + ScenePath);
    }

    static Material MakeMat(Color color)
    {
        var mat = new Material(Shader.Find("Standard"));
        mat.color = color;
        return mat;
    }

    static GameObject MakeLabel(Transform parent, string name, string text,
        Vector2 anchor, Vector2 pos, int fontSize)
    {
        var go = new GameObject(name, typeof(Text));
        go.transform.SetParent(parent, false);
        var label = go.GetComponent<Text>();
        label.text = text;
        label.font = LoadBuiltinFont();
        label.fontSize = fontSize;
        label.color = Color.white;
        var rt = label.rectTransform;
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(1200f, 100f);
        return go;
    }

    static Font LoadBuiltinFont()
    {
        Font font = null;
        try { font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); } catch { }
        if (font == null)
        {
            try { font = Resources.GetBuiltinResource<Font>("Arial.ttf"); } catch { }
        }
        return font;
    }
}
