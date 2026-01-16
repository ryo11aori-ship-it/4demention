using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    float yaw = 0f, pitch = 0f;

    void Start() {
        Vector3 e = transform.eulerAngles;
        yaw = e.y; pitch = e.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        // Mouse look
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        yaw += mx * lookSpeed;
        pitch -= my * lookSpeed;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        // Movement
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) dir += Vector3.back;
        if (Input.GetKey(KeyCode.A)) dir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) dir += Vector3.right;
        if (Input.GetKey(KeyCode.Space)) dir += Vector3.up;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) dir += Vector3.down;

        transform.position += transform.TransformDirection(dir.normalized) * moveSpeed * Time.deltaTime;

        // Unlock cursor
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
