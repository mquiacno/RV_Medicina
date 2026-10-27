using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class GamepadMove : MonoBehaviour
{
    public Transform head;                // arrastra la Main Camera
    public float moveSpeed = 1.6f;
    public float turnSpeed = 90f;
    public float deadzone = 0.25f;        // evita que se mueva solo
    public bool turnWithRightStick = true;

    CharacterController cc;
    float verticalVelocity;

    void Awake() { cc = GetComponent<CharacterController>(); }

    void Update()
    {
        Vector2 move = Vector2.zero;
        Vector2 look = Vector2.zero;

        var pad = Gamepad.current;
        if (pad != null)
        {
            move = Deadzone(pad.leftStick.ReadValue());
            look = Deadzone(pad.rightStick.ReadValue());
        }

        if (turnWithRightStick && look.x != 0f)
            transform.Rotate(0f, look.x * turnSpeed * Time.deltaTime, 0f);

        Vector3 fwd = head.forward; fwd.y = 0f; fwd.Normalize();
        Vector3 right = head.right; right.y = 0f; right.Normalize();
        Vector3 horizontal = (fwd * move.y + right * move.x) * moveSpeed;

        if (cc.isGrounded && verticalVelocity < 0f) verticalVelocity = -1f;
        verticalVelocity += -9.81f * Time.deltaTime;

        cc.Move((horizontal + Vector3.up * verticalVelocity) * Time.deltaTime);
    }

    Vector2 Deadzone(Vector2 v) { return v.magnitude < deadzone ? Vector2.zero : v; }
}