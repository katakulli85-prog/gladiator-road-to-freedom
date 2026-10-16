using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class BuildCommand
{
    const string MenuScenePath = "Assets/_Project/Scenes/MainMenu.unity";
    const string ArenaScenePath = "Assets/_Project/Scenes/Boot.unity";
    const string ApkPath = "build/Android/Android.apk";

    public static void PerformBuild()
    {
        Debug.Log("[Bootstrap] Faz1 APK derlemesi basliyor...");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        ApplyPlayerSettings();
        Directory.CreateDirectory("Assets/_Project/Scenes");
        BuildMenuScene();
        BuildArenaScene();

        Directory.CreateDirectory("build/Android");
        var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = new[] { MenuScenePath, ArenaScenePath },
            locationPathName = ApkPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        });
        var s = report.summary;
        Debug.Log($"[Bootstrap] Sonuc: {s.result} | Boyut: {s.totalSize} | Hata: {s.totalErrors}");
        if (s.result != BuildResult.Succeeded) EditorApplication.Exit(1);
        Debug.Log("[Bootstrap] APK hazir.");
    }

    static void ApplyPlayerSettings()
    {
        PlayerSettings.companyName = "katakulli85-prog";
        PlayerSettings.productName = "Gladiator Road to Freedom";
        PlayerSettings.bundleVersion = "0.2.0";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.katakulli85prog.gladiatorrtf");
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
        PlayerSettings.Android.minSdkVersion = (AndroidSdkVersions)24;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
        PlayerSettings.allowedAutorotateToPortrait = false;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
        PlayerSettings.allowedAutorotateToLandscapeLeft = true;
        PlayerSettings.allowedAutorotateToLandscapeRight = true;
        PlayerSettings.Android.renderOutsideSafeArea = true;
    }

    static void EnsureEventSystem()
    {
        if (EventSystem.current == null)
        {
            var es = new GameObject("EventSystem",
                typeof(EventSystem),
                typeof(StandaloneInputModule));
            Debug.Log("[UI] EventSystem olusturuldu (buton tklamalari icin)");
        }
    }

    static void BuildMenuScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var cam = new GameObject("Main Camera", typeof(Camera));
        cam.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        cam.GetComponent<Camera>().backgroundColor = new Color(0.08f, 0.06f, 0.05f);

        var canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        var menuUI = canvasGO.AddComponent<MenuUI>();

        EnsureEventSystem();

        MakeLabel(canvasGO.transform, "Title", "GLADIATOR", new Vector2(0.5f, 0.80f), 110, new Color(0.85f, 0.68f, 0.3f));
        MakeLabel(canvasGO.transform, "SubTitle", "ROAD TO FREEDOM", new Vector2(0.5f, 0.70f), 46, new Color(0.75f, 0.6f, 0.35f));
        MakeButton(canvasGO.transform, "BtnNew", "YENI OYUN", new Vector2(0.5f, 0.46f), 54, menuUI.OnNewGame);
        MakeButton(canvasGO.transform, "BtnAbout", "HAKKINDA", new Vector2(0.5f, 0.33f), 42, menuUI.OnAbout);

        var about = new GameObject("AboutPanel", typeof(Image));
        about.transform.SetParent(canvasGO.transform, false);
        about.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.92f);
        var art = about.GetComponent<RectTransform>();
        art.anchorMin = Vector2.zero; art.anchorMax = Vector2.one;
        art.offsetMin = Vector2.zero; art.offsetMax = Vector2.zero;
        MakeLabel(about.transform, "AboutTitle", "GLADIATOR: ROAD TO FREEDOM", new Vector2(0.5f, 0.72f), 52, Color.white);
        MakeLabel(about.transform, "AboutText",
            "Created by Orhan Biyik\n\nKole bir gladyator olarak basla,\n100 savasi kazanarak ozgurlugunu kazan.\n\nSurum v0.2.0 - Faz 1",
            new Vector2(0.5f, 0.5f), 38, new Color(0.9f, 0.85f, 0.7f));
        MakeButton(about.transform, "BtnCloseAbout", "KAPAT", new Vector2(0.5f, 0.15f), 42, menuUI.OnCloseAbout);
        about.SetActive(false);

        var flowGO = new GameObject("GameFlow", typeof(GameFlowRunner));

        EditorSceneManager.SaveScene(scene, MenuScenePath);
        Debug.Log("[Flow] Ana menu sahnesi uretildi: " + MenuScenePath);
    }

    static void BuildArenaScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(5f, 1f, 5f);
        ground.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.72f, 0.63f, 0.47f));

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "RotatingCube";
        cube.transform.position = new Vector3(0f, 0.7f, 0f);
        cube.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.65f, 0.16f, 0.12f));
        cube.AddComponent<SpinCube>();

        var cameraGO = new GameObject("Main Camera", typeof(Camera));
        cameraGO.transform.position = new Vector3(0f, 2.2f, -5.5f);

        var canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        EnsureEventSystem();

        MakeLabel(canvasGO.transform, "ArenaText", "ARENA - Faz 1 test sahnesi", new Vector2(0.5f, 1f), 40, Color.white);

        var btnGO = new GameObject("BtnBackMenu", typeof(Button), typeof(Image));
        btnGO.transform.SetParent(canvasGO.transform, false);
        var rt = btnGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0f); rt.anchorMax = new Vector2(0.5f, 0f);
        rt.anchoredPosition = new Vector2(0f, 60f);
        rt.sizeDelta = new Vector2(340f, 90f);
        btnGO.GetComponent<Image>().color = new Color(0.2f, 0.15f, 0.1f, 0.9f);
        var lblGO = new GameObject("Label", typeof(Text));
        lblGO.transform.SetParent(btnGO.transform, false);
        var lbl = lblGO.GetComponent<Text>();
        lbl.text = "MENUYE DON";
        lbl.font = LoadBuiltinFont(); lbl.fontSize = 36; lbl.color = Color.white;
        lbl.alignment = TextAnchor.MiddleCenter;
        var lrt = lbl.rectTransform; lrt.anchorMin = Vector2.zero; lrt.anchorMax = Vector2.one;
        lrt.offsetMin = Vector2.zero; lrt.offsetMax = Vector2.zero;
        btnGO.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("[Arena] Menuye donuluyor");
            SceneManager.LoadScene("MainMenu");
        });

        EditorSceneManager.SaveScene(scene, ArenaScenePath);
        Debug.Log("[Flow] Arena sahnesi guncellendi");
    }

    static GameObject MakeLabel(Transform parent, string name, string text,
        Vector2 anchor, int fontSize, Color color)
    {
        var go = new GameObject(name, typeof(Text));
        go.transform.SetParent(parent, false);
        var label = go.GetComponent<Text>();
        label.text = text;
        label.font = LoadBuiltinFont();
        label.fontSize = fontSize;
        label.color = color;
        label.alignment = TextAnchor.MiddleCenter;
        var rt = label.rectTransform;
        rt.anchorMin = anchor; rt.anchorMax = anchor;
        rt.sizeDelta = new Vector2(1400f, 120f);
        return go;
    }

    static GameObject MakeButton(Transform parent, string name, string text,
        Vector2 anchor, int fontSize, UnityEngine.Events.UnityAction action)
    {
        var go = new GameObject(name, typeof(Button), typeof(Image));
        go.transform.SetParent(parent, false);
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchor; rt.anchorMax = anchor;
        rt.sizeDelta = new Vector2(520f, 100f);
        go.GetComponent<Image>().color = new Color(0.2f, 0.15f, 0.1f, 0.9f);

        var lblGO = new GameObject("Label", typeof(Text));
        lblGO.transform.SetParent(go.transform, false);
        var lbl = lblGO.GetComponent<Text>();
        lbl.text = text;
        lbl.font = LoadBuiltinFont(); lbl.fontSize = fontSize; lbl.color = new Color(0.85f, 0.68f, 0.3f);
        lbl.alignment = TextAnchor.MiddleCenter;
        var lrt = lbl.rectTransform;
        lrt.anchorMin = Vector2.zero; lrt.anchorMax = Vector2.one;
        lrt.offsetMin = Vector2.zero; lrt.offsetMax = Vector2.zero;

        go.GetComponent<Button>().onClick.AddListener(action);
        return go;
    }

    static Material MakeMat(Color color)
    {
        var mat = new Material(Shader.Find("Standard"));
        mat.color = color;
        return mat;
    }

    static Font LoadBuiltinFont()
    {
        Font font = null;
        try { font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); } catch { }
        if (font == null) { try { font = Resources.GetBuiltinResource<Font>("Arial.ttf"); } catch { } }
        return font;
    }
}
