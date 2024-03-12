using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform Player;
    public float Distance = 5.0f;
    public float XSpeed = 120.0f;
    public float YSpeed = 120.0f;

    private float X = 0.0f;
    private float Y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        X = angles.y;
        Y = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        X += Input.GetAxis("Mouse X") * XSpeed * Distance * 0.02f;
        Y -= Input.GetAxis("Mouse Y") * YSpeed * 0.02f;

        Quaternion rotation = Quaternion.Euler(Y, X, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -Distance) + Player.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}