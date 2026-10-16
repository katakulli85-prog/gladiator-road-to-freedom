using UnityEngine;
using UnityEngine.UI;

// Oyun hakkinda - icerik sabitleri
public class GameInfo : MonoBehaviour
{
    public const string GameName = "GLADIATOR: ROAD TO FREEDOM";
    public const string Developer = "Created by Orhan Biyik";
    public const string Copyright = "(c) 2026 Orhan Biyik. Tum haklari saklidir.";
    public const string Version = "v0.5.0 - Faz 1";

    public static void BuildAboutPanel(Transform parent)
    {
        var panel = new GameObject("AboutPanelRich", typeof(Image));
        panel.transform.SetParent(parent, false);
        panel.GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 0.96f);
        var art = panel.GetComponent<RectTransform>();
        art.anchorMin = Vector2.zero; art.anchorMax = Vector2.one;
        art.offsetMin = Vector2.zero; art.offsetMax = Vector2.zero;

        Make(panel, "T1", GameName, new Vector2(0.5f, 0.86f), 54, new Color(0.85f, 0.68f, 0.3f));
        Make(panel, "T2", "SAVAS - ONUR - OZGURLUK", new Vector2(0.5f, 0.79f), 30, new Color(0.75f, 0.6f, 0.35f));
        Make(panel, "B1",
            "Kole bir gladyator olarak arenaya adim at.\n" +
            "100 savasi kazan, ozgurlugunu kazan.\n\n" +
            "Roma'dan bozkirlara uzanan arenalarda,\n" +
            "kilic, mizrak ve kalkanlarla dovus.\n" +
            "Zirhini guclendir, ruhunu kaybetme.",
            new Vector2(0.5f, 0.56f), 34, new Color(0.92f, 0.88f, 0.78f));
        Make(panel, "D1", Developer, new Vector2(0.5f, 0.26f), 34, new Color(0.85f, 0.68f, 0.3f));
        Make(panel, "D2", Copyright, new Vector2(0.5f, 0.20f), 26, new Color(0.7f, 0.68f, 0.62f));
        Make(panel, "D3", Version, new Vector2(0.5f, 0.15f), 26, new Color(0.6f, 0.58f, 0.52f));
    }

    static void Make(Transform parent, string name, string text,
        Vector2 anchor, int fontSize, Color color)
    {
        var go = new GameObject(name, typeof(Text));
        go.transform.SetParent(parent, false);
        var t = go.GetComponent<Text>();
        t.text = text;
        t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if (t.font == null) t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        t.fontSize = fontSize;
        t.color = color;
        t.alignment = TextAnchor.MiddleCenter;
        var rt = t.rectTransform;
        rt.anchorMin = anchor; rt.anchorMax = anchor;
        rt.sizeDelta = new Vector2(1600f, 260f);
    }
}
