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
        Debug.Log("[Bootstrap] Faz1 P2-P3 APK derlemesi basliyor...");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        ApplyPlayerSettings();
        Directory.CreateDirectory("Assets/_Project/Scenes");
        BuildMenuScene();
        BuildArenaScene();

        EditorBuildSettings.scenes = new[] {
            new EditorBuildSettingsScene(MenuScenePath, true),
            new EditorBuildSettingsScene(ArenaScenePath, true)
        };

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
        PlayerSettings.bundleVersion = "0.5.0";
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
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            Debug.Log("[UI] EventSystem olusturuldu");
        }
    }

    static Font LoadBuiltinFont()
    {
        Font font = null;
        try { font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); } catch { }
        if (font == null) { try { font = Resources.GetBuiltinResource<Font>("Arial.ttf"); } catch { } }
        return font;
    }

    static Material MakeMat(Color color)
    {
        var mat = new Material(Shader.Find("Standard"));
        mat.color = color;
        return mat;
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
        rt.sizeDelta = new Vector2(420f, 90f);
        go.GetComponent<Image>().color = new Color(0.25f, 0.2f, 0.12f, 0.95f);

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

    static GameObject MakePanel(Transform parent, string name, string title, string body)
    {
        var panel = new GameObject(name, typeof(Image));
        panel.transform.SetParent(parent, false);
        panel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.93f);
        var art = panel.GetComponent<RectTransform>();
        art.anchorMin = Vector2.zero; art.anchorMax = Vector2.one;
        art.offsetMin = Vector2.zero; art.offsetMax = Vector2.zero;
        MakeLabel(panel.transform, "PTitle", title, new Vector2(0.5f, 0.72f), 64, new Color(0.85f, 0.68f, 0.3f));
        MakeLabel(panel.transform, "PBody", body, new Vector2(0.5f, 0.55f), 40, Color.white);
        panel.SetActive(false);
        return panel;
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
        canvasGO.AddComponent<ArenaUI>();

        MakeLabel(canvasGO.transform, "Title", "GLADIATOR", new Vector2(0.5f, 0.80f), 110, new Color(0.85f, 0.68f, 0.3f));
        MakeLabel(canvasGO.transform, "SubTitle", "ROAD TO FREEDOM", new Vector2(0.5f, 0.70f), 46, new Color(0.75f, 0.6f, 0.35f));
        MakeButton(canvasGO.transform, "BtnNew", "YENI OYUN", new Vector2(0.5f, 0.46f), 54, menuUI.OnNewGame);
        MakeButton(canvasGO.transform, "BtnAbout", "HAKKINDA", new Vector2(0.5f, 0.33f), 42, menuUI.OnAbout);

        var about = MakePanel(canvasGO.transform, "AboutPanel",
            "GLADIATOR: ROAD TO FREEDOM",
            "Created by Orhan Biyik\n\nKole bir gladyator olarak basla,\n100 savasi kazanarak ozgurlugunu kazan.\n\nSurum v0.5.0 - Faz 1 P2-P3");
        MakeButton(about.transform, "BtnCloseAbout", "KAPAT", new Vector2(0.5f, 0.15f), 42, menuUI.OnCloseAbout);

        var flowGO = new GameObject("GameFlow", typeof(GameFlowRunner));

        EditorSceneManager.SaveScene(scene, MenuScenePath);
        Debug.Log("[Flow] Ana menu sahnesi uretildi");
    }

    static void BuildArenaScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(8f, 1f, 8f);
        ground.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.85f, 0.72f, 0.55f));
        ground.transform.localScale = new Vector3(10f, 1f, 10f);

        // Arena duvarlari (4 yon - basit ama atmosferik)
        for (int i = 0; i < 4; i++)
        {
            var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.name = "ArenaWall_" + i;
            wall.transform.SetParent(ground.transform.parent, false);
            float sz = 40f;
            if (i == 0) { wall.transform.position = new Vector3(0, 3, 15f); wall.transform.localScale = new Vector3(sz, 6, 1); }
            if (i == 1) { wall.transform.position = new Vector3(0, 3, -15f); wall.transform.localScale = new Vector3(sz, 6, 1); }
            if (i == 2) { wall.transform.position = new Vector3(15f, 3, 0); wall.transform.localScale = new Vector3(1, 6, sz); }
            if (i == 3) { wall.transform.position = new Vector3(-15f, 3, 0); wall.transform.localScale = new Vector3(1, 6, sz); }
            wall.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.45f, 0.35f, 0.25f));
        }

        // 4 meşale ışığı (arena köşelerinde, sıcak turuncu)
        for (int i = 0; i < 4; i++)
        {
            var torch = new GameObject("Torch_" + i, typeof(Light));
            torch.GetComponent<Light>().type = LightType.Point;
            torch.GetComponent<Light>().color = new Color(1f, 0.55f, 0.2f);
            torch.GetComponent<Light>().intensity = 1.8f;
            torch.GetComponent<Light>().range = 12f;
            float tx = (i == 0 || i == 2) ? 12f : -12f;
            float tz = (i < 2) ? 12f : -12f;
            torch.transform.position = new Vector3(tx, 4f, tz);
        }

        // Toz partiküllerini simüle etmek için küçük yarı saydam küpler
        for (int i = 0; i < 20; i++)
        {
            var dust = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dust.name = "Dust_" + i;
            dust.transform.SetParent(ground.transform.parent, false);
            dust.transform.position = new Vector3(
                Random.Range(-14f, 14f), Random.Range(0.5f, 6f), Random.Range(-14f, 14f));
            float dsz = Random.Range(0.05f, 0.2f);
            dust.transform.localScale = new Vector3(dsz, dsz, dsz);
            dust.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.9f, 0.8f, 0.6f, 0.3f));
            Object.DestroyImmediate(dust.GetComponent<Collider>(), true);
        }

        var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0f, 1f, 0f);
        player.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.6f, 0.45f, 0.3f));

        // Gladyator görünümü: kalkan + kılıç (çocuk parçalar)
        var shield = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shield.name = "Shield";
        shield.transform.SetParent(player.transform, false);
        shield.transform.localPosition = new Vector3(-0.55f, 0.1f, 0.2f);
        shield.transform.localScale = new Vector3(0.15f, 0.7f, 0.7f);
        shield.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.5f, 0.4f, 0.25f));
        Object.DestroyImmediate(shield.GetComponent<Collider>(), true);

        var sword = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sword.name = "Sword";
        sword.transform.SetParent(player.transform, false);
        sword.transform.localPosition = new Vector3(0.55f, 0.3f, 0.3f);
        sword.transform.localScale = new Vector3(0.08f, 0.08f, 1.2f);
        sword.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.75f, 0.75f, 0.8f));
        Object.DestroyImmediate(sword.GetComponent<Collider>(), true);
        var pc = player.GetComponent<CapsuleCollider>();
        if (pc != null) UnityEngine.Object.DestroyImmediate(pc, true);
        player.AddComponent<CharacterController>();
        player.AddComponent<PlayerMotor>();
        player.AddComponent<PlayerCombat>();
        var phealth = player.AddComponent<PlayerHealth>();

        var enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        enemy.name = "Enemy";
        enemy.transform.position = new Vector3(0f, 1f, 4f);
        enemy.GetComponent<Renderer>().sharedMaterial = MakeMat(new Color(0.45f, 0.18f, 0.18f));
        var eai = enemy.AddComponent<EnemyAI>();
        eai.target = player.transform;

        var camGO = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
        camGO.transform.position = new Vector3(0f, 3.2f, -5.2f);
        var camScript = camGO.AddComponent<ThirdPersonCamera>();
        camScript.target = player.transform;
        camGO.AddComponent<ScreenShake>();

        var lightGO = new GameObject("Directional Light", typeof(Light));
        lightGO.GetComponent<Light>().type = LightType.Directional;
        lightGO.GetComponent<Light>().intensity = 1.2f;
        lightGO.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        var canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        EnsureEventSystem();

        var hudGO = new GameObject("HUD", typeof(HUD));
        var hud = hudGO.GetComponent<HUD>();

        var joyBG = new GameObject("JoystickBG", typeof(Image));
        joyBG.transform.SetParent(canvasGO.transform, false);
        joyBG.AddComponent<VirtualJoystick>();
        var joyRT = joyBG.GetComponent<RectTransform>();
        joyRT.anchorMin = new Vector2(0.08f, 0.12f); joyRT.anchorMax = new Vector2(0.08f, 0.12f);
        joyRT.sizeDelta = new Vector2(240f, 240f);
        joyBG.GetComponent<Image>().color = new Color(0.2f, 0.18f, 0.12f, 0.45f);

        var pHud = new GameObject("PlayerBarBG", typeof(Image));
        pHud.transform.SetParent(canvasGO.transform, false);
        var prt = pHud.GetComponent<RectTransform>();
        prt.anchorMin = new Vector2(0.28f, 0.93f); prt.anchorMax = new Vector2(0.28f, 0.93f);
        prt.sizeDelta = new Vector2(440f, 34f);
        pHud.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        var pFill = new GameObject("Fill", typeof(Image));
        pFill.transform.SetParent(pHud.transform, false);
        var pfrt = pFill.GetComponent<RectTransform>();
        pfrt.anchorMin = Vector2.zero; pfrt.anchorMax = Vector2.one;
        pfrt.offsetMin = new Vector2(3, 3); pfrt.offsetMax = new Vector2(-3, -3);
        var pimg = pFill.GetComponent<Image>();
        pimg.color = new Color(0.3f, 0.85f, 0.3f);
        var eHud = new GameObject("EnemyBarBG", typeof(Image));
        eHud.transform.SetParent(canvasGO.transform, false);
        var ert = eHud.GetComponent<RectTransform>();
        ert.anchorMin = new Vector2(0.72f, 0.93f); ert.anchorMax = new Vector2(0.72f, 0.93f);
        ert.sizeDelta = new Vector2(440f, 34f);
        eHud.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        var eFill = new GameObject("Fill", typeof(Image));
        eFill.transform.SetParent(eHud.transform, false);
        var efrt = eFill.GetComponent<RectTransform>();
        efrt.anchorMin = Vector2.zero; efrt.anchorMax = Vector2.one;
        efrt.offsetMin = new Vector2(3, 3); efrt.offsetMax = new Vector2(-3, -3);
        eFill.GetComponent<Image>().color = new Color(0.85f, 0.3f, 0.25f);
        hud.playerBar = pimg;
        hud.enemyBar = eFill.GetComponent<Image>();
        hud.RegisterPlayer(100f);
        hud.RegisterEnemy(eai.healthMax);

        MakeButton(canvasGO.transform, "BtnLight", "HAFIF", new Vector2(0.78f, 0.22f), 30,
            () => { var pcx = Object.FindObjectOfType<PlayerCombat>(); if (pcx != null) pcx.TryLightAttack(); });
        MakeButton(canvasGO.transform, "BtnHeavy", "AGIR", new Vector2(0.90f, 0.22f), 30,
            () => { var pcx = Object.FindObjectOfType<PlayerCombat>(); if (pcx != null) pcx.TryHeavyAttack(); });

        var backGO = new GameObject("BtnBackMenu", typeof(Button), typeof(Image));
        backGO.transform.SetParent(canvasGO.transform, false);
        var brt = backGO.GetComponent<RectTransform>();
        brt.anchorMin = new Vector2(0.92f, 0.9f); brt.anchorMax = new Vector2(0.92f, 0.9f);
        brt.sizeDelta = new Vector2(280f, 70f);
        backGO.GetComponent<Image>().color = new Color(0.2f, 0.15f, 0.1f, 0.9f);
        var blblGO = new GameObject("Label", typeof(Text));
        blblGO.transform.SetParent(backGO.transform, false);
        var blbl = blblGO.GetComponent<Text>();
        blbl.text = "MENU";
        blbl.font = LoadBuiltinFont(); blbl.fontSize = 28; blbl.color = Color.white;
        blbl.alignment = TextAnchor.MiddleCenter;
        var blrt = blbl.rectTransform; blrt.anchorMin = Vector2.zero; blrt.anchorMax = Vector2.one;
        blrt.offsetMin = Vector2.zero; blrt.offsetMax = Vector2.zero;
        backGO.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("[Arena] Menuye donuluyor");
            SceneManager.LoadScene(0);
        });

        var victory = MakePanel(canvasGO.transform, "VictoryPanel", "ZAFER",
            "Dusmani yendin!\nIlk duello tamam.");
        eai.victoryPanel = victory;
        MakeButton(victory.transform, "BtnRetry", "TEKRAR", new Vector2(0.5f, 0.3f), 40,
            () => SceneManager.LoadScene(1));

        var defeat = MakePanel(canvasGO.transform, "DefeatPanel", "YENILGI",
            "Dustun... ama ayaga kalk.\n\nYuz kez dustum, yuz kez kalktim.");
        phealth.defeatPanel = defeat;
        MakeButton(defeat.transform, "BtnRetryD", "TEKRAR", new Vector2(0.5f, 0.3f), 40,
            () => SceneManager.LoadScene(1));
        MakeButton(defeat.transform, "BtnMenuD", "MENU", new Vector2(0.5f, 0.18f), 36,
            () => SceneManager.LoadScene(0));

        EditorSceneManager.SaveScene(scene, ArenaScenePath);
        Debug.Log("[Flow] Arena sahnesi: HUD + dusman AI + paneller tamam");
    }
}
