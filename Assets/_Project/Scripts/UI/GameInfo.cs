using UnityEngine;
using UnityEngine.UI;

// Oyun hakkinda - icerik sabitleri (tek kaynak: dil dosyasi Faz 3'e tasinacak)
public class GameInfo : MonoBehaviour
{
    public const string GameName = "GLADIATOR: ROAD TO FREEDOM";
    public const string Developer = "Created by Orhan Biyik";
    public const string Copyright = "© 2026 Orhan Biyik. Tüm hakları saklıdır.";
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
        Make(panel, "T2", "SAVAŞ · ONUR · ÖZGÜRLÜK", new Vector2(0.5f, 0.79f), 30, new Color(0.75f, 0.6f, 0.35f));
        Make(panel, "B1",
            "Köle bir gladyatör olarak arenaya adım at.\n" +
            "100 savaşı kazan, özgürlüğünü kazan.\n\n" +
            "Roma'dan bozkırlara uzanan arenalarda,\n" +
            "kılıç, mızrak ve kalkanlarla dövüş.\n" +
            "Zırhını güçlendir, ruhunu kaybetme.",
            new Vector2(0.5f, 0.56f), 34, new Color(0.92f, 0.88f, 0.78f));
        Make(panel, "D1", Developer, new Vector2(0.5f, 0.26f), 34, new Color(0.85f, 0.68f, 0.3f));
        Make(panel, "D2", Copyright, new Vector2(0.5f, 0.20f), 26, new Color(0.7f, 0.68f, 0.62f));
        Make(panel, "D3", Version, new Vector2(0.5f, 0.15f), 26, new Color(0.6f, 0.58f, 0.52f));
        Make(panel, "D4", "Destek ve gizlilik: yayin oncesi eklenecek", new Vector2(0.5f, 0.10f), 22, new Color(0.55f, 0.53f, 0.48f));
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
        t.supportRichText = true;
        var rt = t.rectTransform;
        rt.anchorMin = anchor; rt.anchorMax = anchor;
        rt.sizeDelta = new Vector2(1600f, 260f);
    }
}
EOF
