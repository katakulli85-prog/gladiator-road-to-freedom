using UnityEngine;

// Ucuncu sahis takip kamerasi - karakteri arkadan izler
// Sonra: hedef kilitleme uyumu (TUR 4), duvar engelleme (Faz 11)
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2.0f, -4.0f);
    public float followSpeed = 8f;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);
        Vector3 lookAt = target.position + Vector3.up * 1.4f;
        transform.LookAt(lookAt);
    }
}
