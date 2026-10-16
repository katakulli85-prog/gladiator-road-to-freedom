using UnityEngine;

// Kamera sarsintisi - agir vuruslarda kisa tepki (ayarlardan kapatilabilir)
public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }
    public bool Enabled = true;
    public float Intensity = 1f;

    Vector3 baseOffset;
    float shakeTime;
    float shakeAmount;

    void Awake()
    {
        Instance = this;
    }

    public static void Shake(float amount, float duration = 0.15f)
    {
        if (Instance == null || !Instance.Enabled) return;
        Instance.shakeAmount = amount * Instance.Intensity;
        Instance.shakeTime = duration;
    }

    void LateUpdate()
    {
        if (shakeTime > 0 && target != null)
        {
            shakeTime -= Time.deltaTime;
            Vector3 shake = Random.insideUnitSphere * shakeAmount;
            transform.position += shake;
        }
    }

    Transform target;
    void Start()
    {
        var cam = GetComponent<ThirdPersonCamera>();
        if (cam != null) target = cam.target;
    }
}
