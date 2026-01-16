using UnityEngine;

// Coordinates control of 4D rotations and w-offset; updates PointCloudRenderer fields.
[RequireComponent(typeof(PointCloudRenderer))]
public class WAxisController : MonoBehaviour {
    public PointCloudRenderer rendererSource;
    public EdgeRenderer edgeRenderer;

    [Header("Rotation speed (rad/s)")]
    public float rotSpeed = Mathf.PI / 4f; // 45 deg/s

    [Header("w control")]
    public float wStep = 0.2f;
    public float wSpeed = 1.0f;

    void Start() {
        if (rendererSource == null) rendererSource = GetComponent<PointCloudRenderer>();
    }

    void Update() {
        // Rotation keys: Q/E: XW, Z/C: YW, R/F: ZW
        float dt = Time.deltaTime;
        float rxw = 0f, ryw = 0f, rzw = 0f;
        if (Input.GetKey(KeyCode.Q)) rxw -= rotSpeed * dt;
        if (Input.GetKey(KeyCode.E)) rxw += rotSpeed * dt;
        if (Input.GetKey(KeyCode.Z)) ryw -= rotSpeed * dt;
        if (Input.GetKey(KeyCode.C)) ryw += rotSpeed * dt;
        if (Input.GetKey(KeyCode.R)) rzw -= rotSpeed * dt;
        if (Input.GetKey(KeyCode.F)) rzw += rotSpeed * dt;

        rendererSource.rotXW = rxw;
        rendererSource.rotYW = ryw;
        rendererSource.rotZW = rzw;

        // w offset control using mouse wheel (or keys -/+)
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(wheel) > 1e-5f) {
            rendererSource.wOffset += wheel * wSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            rendererSource.gameObject.SetActive(true);
            if (edgeRenderer != null) edgeRenderer.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (edgeRenderer != null) edgeRenderer.visible = true;
            rendererSource.showPoints = false;
            rendererSource.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            rendererSource.showPoints = true;
            if (edgeRenderer != null) edgeRenderer.visible = true;
            rendererSource.gameObject.SetActive(true);
        }

        // Update edge renderer visibility toggles
        if (edgeRenderer != null) {
            edgeRenderer.visible = rendererSource.showEdges;
        }
    }
}
