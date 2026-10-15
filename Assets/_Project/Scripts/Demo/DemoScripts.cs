using UnityEngine;
using UnityEngine.UI;

// Kutu 3 test sahnesi icin gecici demo bilesenleri.
public class SpinCube : MonoBehaviour
{
    public Vector3 spin = new Vector3(25f, 60f, 0f);

    void Update()
    {
        transform.Rotate(spin * Time.deltaTime, Space.World);
    }
}

public class FpsCounter : MonoBehaviour
{
    Text label;
    float timer;
    int frames;

    void Awake()
    {
        label = GetComponent<Text>();
    }

    void Update()
    {
        frames++;
        timer += Time.unscaledDeltaTime;
        if (timer >= 0.5f)
        {
            int fps = Mathf.RoundToInt(frames / timer);
            if (label != null) label.text = "FPS: " + fps;
            frames = 0;
            timer = 0f;
        }
    }
}
