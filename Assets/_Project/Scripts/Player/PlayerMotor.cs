using UnityEngine;

// Kapsul karakter hareketi: joystick ile yurume + donus
// Sonra: animasyon (TUR 3-6), dövüş girdisi (TUR 3)
public class PlayerMotor : MonoBehaviour
{
    public float walkSpeed = 3.2f;
    public float runSpeed = 6.0f;
    public float turnSpeed = 10f;
    public float runThreshold = 0.85f; // joystick cekimi

    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (cc == null) cc = gameObject.AddComponent<CharacterController>();
        cc.height = 2f;
        cc.radius = 0.4f;
    }

    void Update()
    {
        Vector2 input = VirtualJoystick.Input;
        if (input.sqrMagnitude < 0.05f) { Input = Vector2.zero; return; }

        float speed = input.magnitude > runThreshold ? runSpeed : walkSpeed;

        // Kamera bagli yon (kamera bakiyor -> yukari = ileri)
        Vector3 forward = Camera.main != null ? Camera.main.transform.forward : Vector3.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude < 0.01f) forward = Vector3.forward;
        forward.Normalize();
        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        Vector3 move = (forward * input.y + right * input.x).normalized * speed * Mathf.Clamp01(input.magnitude);
        cc.Move(move * Time.deltaTime);
        if (!cc.isGrounded)
            cc.Move(Physics.gravity * 0.5f * Time.deltaTime);

        // Yonune donus
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion look = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, look, turnSpeed * Time.deltaTime);
        }
    }

    Vector3 Input;
}
