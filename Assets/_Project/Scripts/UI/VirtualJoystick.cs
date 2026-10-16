using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Sol altta sanal joystick - coklu dokunma uyumlu (kendi pointerId'sini takip eder)
public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static Vector2 Input { get; private set; }

    [Header("Ayarlar")]
    public float radius = 110f;

    RectTransform rect;
    RectTransform knob;
    int pointerId = -999;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        var knobTr = transform.Find("Knob");
        knob = knobTr as RectTransform;
        if (knob == null)
        {
            var go = new GameObject("Knob", typeof(Image));
            go.transform.SetParent(transform, false);
            knob = go.GetComponent<RectTransform>();
            knob.sizeDelta = new Vector2(110, 110);
            var img = go.GetComponent<Image>();
            img.color = new Color(0.85f, 0.68f, 0.3f, 0.9f);
        }
        Input = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (pointerId == -999) { pointerId = e.pointerId; OnDrag(e); }
    }

    public void OnDrag(PointerEventData e)
    {
        if (e.pointerId != pointerId) return;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, e.position, e.pressEventCamera, out pos);
        Vector2 dir = Vector2.ClampMagnitude(pos, radius);
        knob.anchoredPosition = dir;
        Input = dir / radius;
    }

    public void OnPointerUp(PointerEventData e)
    {
        if (e.pointerId != pointerId) return;
        pointerId = -999;
        knob.anchoredPosition = Vector2.zero;
        Input = Vector2.zero;
    }
}
