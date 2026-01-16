using System.Collections.Generic;
using UnityEngine;
using FourD.Math;
using FourD.Projection;
using FourD.Geometry;

[RequireComponent(typeof(ParticleSystem))]
public class PointCloudRenderer : MonoBehaviour {
    [Header("Geometry")]
    public float size = 1.0f;
    public bool showPoints = true;
    public bool showEdges = true;

    [Header("Projection")]
    public float wViewDistance = 3.0f; // d in projection

    [Header("Rendering")]
    public float pointSize = 0.05f;
    public Color pointColor = Color.cyan;

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    List<Vec4> baseVertices;
    List<(int,int)> edges;

    // transformations applied each frame
    public float rotXW = 0f, rotYW = 0f, rotZW = 0f;
    public float wOffset = 0f;

    void Awake() {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.maxParticles = 1024;
        main.startSize = pointSize;
        main.startColor = pointColor;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
    }

    void Start() {
        Tesseract.Generate(size, out baseVertices, out edges);
        particles = new ParticleSystem.Particle[baseVertices.Count];
        UpdateParticles(); // initial
    }

    void Update() {
        UpdateParticles();
    }

    void UpdateParticles() {
        int n = baseVertices.Count;
        if (particles.Length < n) particles = new ParticleSystem.Particle[n];

        for (int i = 0; i < n; i++) {
            Vec4 v = baseVertices[i];

            // apply rotations (order: XW, YW, ZW — these are user controllable)
            if (!Mathf.Approximately(rotXW, 0f)) v = FourD.Math.Rotation4D.RotateXW(v, rotXW);
            if (!Mathf.Approximately(rotYW, 0f)) v = FourD.Math.Rotation4D.RotateYW(v, rotYW);
            if (!Mathf.Approximately(rotZW, 0f)) v = FourD.Math.Rotation4D.RotateZW(v, rotZW);

            // apply w offset (slice movement)
            v.w += wOffset;

            // project to 3D
            Vector3 p = Perspective4D.Project(v, wViewDistance);

            // fill particle
            particles[i].position = transform.TransformPoint(p); // allow object transform to position whole thing
            particles[i].startColor = pointColor;
            particles[i].startSize = pointSize;
        }

        ps.SetParticles(particles, n);
    }

    // expose data for edge renderer
    public IReadOnlyList<Vec4> GetCurrent4DVertices() {
        // return vertices after rotation+offset (projected step omitted)
        List<Vec4> outv = new List<Vec4>(baseVertices.Count);
        foreach (var b in baseVertices) {
            Vec4 v = b;
            if (!Mathf.Approximately(rotXW, 0f)) v = FourD.Math.Rotation4D.RotateXW(v, rotXW);
            if (!Mathf.Approximately(rotYW, 0f)) v = FourD.Math.Rotation4D.RotateYW(v, rotYW);
            if (!Mathf.Approximately(rotZW, 0f)) v = FourD.Math.Rotation4D.RotateZW(v, rotZW);
            v.w += wOffset;
            outv.Add(v);
        }
        return outv;
    }

    public IReadOnlyList<(int,int)> GetEdges() => edges;
}
