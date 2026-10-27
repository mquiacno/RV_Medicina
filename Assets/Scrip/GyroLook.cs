using UnityEngine;
using UnityEngine.InputSystem;

public class GyroLook : MonoBehaviour
{
    public float autoRecenterDelay = 1f;
    [Range(0f, 30f)] public float smoothing = 12f;   // 0 = sin suavizado
    [Tooltip("Solo si tras compilar sigue girado: prueba 90, -90 o 180")]
    public float extraRoll = 0f;

    Quaternion yawOffset = Quaternion.identity;
    float startTime;
    bool autoDone;
    ScreenOrientation lastOrientation;

    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        // Solo horizontal (ambos lados)
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;

        // El Input System corrige el sensor según la orientación de la pantalla
        InputSystem.settings.compensateForScreenOrientation = true;
    }

    void OnEnable()
    {
        if (AttitudeSensor.current != null)
        {
            InputSystem.EnableDevice(AttitudeSensor.current);
            AttitudeSensor.current.samplingFrequency = 60f;
        }
        startTime = Time.time;
        lastOrientation = Screen.orientation;
    }

    Quaternion ReadDevice()
    {
        Quaternion q = AttitudeSensor.current.attitude.ReadValue();
        return Quaternion.Euler(90f, 0f, 0f)
             * new Quaternion(q.x, q.y, -q.z, -q.w)
             * Quaternion.Euler(0f, 0f, extraRoll);
    }

    void Recenter(Quaternion device)
    {
        Vector3 f = device * Vector3.forward;
        float yaw = Mathf.Atan2(f.x, f.z) * Mathf.Rad2Deg;
        yawOffset = Quaternion.Euler(0f, -yaw, 0f);
    }

    void Update()
    {
        if (AttitudeSensor.current == null) return;

        Quaternion device = ReadDevice();

        bool orientationChanged = Screen.orientation != lastOrientation;
        lastOrientation = Screen.orientation;

        if (!autoDone && Time.time - startTime > autoRecenterDelay)
        {
            Recenter(device);
            autoDone = true;
        }

        if (orientationChanged ||
          (Touchscreen.current != null &&
           Touchscreen.current.primaryTouch.press.wasPressedThisFrame) ||
          (Gamepad.current != null &&
           Gamepad.current.buttonSouth.wasPressedThisFrame))
        {
            Recenter(device);
        }

        Quaternion target = yawOffset * device;
        transform.localRotation = smoothing > 0f
            ? Quaternion.Slerp(transform.localRotation, target,
                               1f - Mathf.Exp(-smoothing * Time.deltaTime))
            : target;
    }
}