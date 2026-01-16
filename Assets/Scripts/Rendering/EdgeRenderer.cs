using System.Collections.Generic;
using UnityEngine;
using FourD.Math;
using FourD.Projection;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EdgeRenderer : MonoBehaviour {
    public PointCloudRenderer source;
    public Material lineMaterial;
    public float lineWidth = 0.02f;
    public Color lineColor = Color.white;
    public bool visible = true;

    Mesh lineMesh;

    void Awake() {
        var mf = GetComponent<MeshFilter>();
        lineMesh = new Mesh();
        lineMesh.name = "EdgeMesh";
        mf.sharedMesh = lineMesh;

        var mr = GetComponent<MeshRenderer>();
        if (lineMaterial != null) mr.sharedMaterial = lineMaterial;
    }

    void Update() {
        if (source == null || !visible) {
            lineMesh.Clear();
            return;
        }

        var verts4 = source.GetCurrent4DVertices();
        var edges = source.GetEdges();

        int vcount = verts4.Count;
        Vector3[] verts3 = new Vector3[vcount];
        for (int i = 0; i < vcount; i++) {
            verts3[i] = Perspective4D.Project(verts4[i], source.wViewDistance);
            verts3[i] = source.transform.TransformPoint(verts3[i]);
        }

        // Build line mesh: for each edge add two vertices (duplicated) and indices
        int ecount = edges.Count;
        Vector3[] lineVerts = new Vector3[ecount * 2];
        int[] indices = new int[ecount * 2];
        for (int i = 0; i < ecount; i++) {
            var (a,b) = edges[i];
            lineVerts[i*2] = verts3[a];
            lineVerts[i*2 + 1] = verts3[b];
            indices[i*2] = i*2;
            indices[i*2 + 1] = i*2 + 1;
        }

        lineMesh.Clear();
        lineMesh.vertices = lineVerts;
        lineMesh.SetIndices(indices, MeshTopology.Lines, 0);
        // set material properties
        if (GetComponent<MeshRenderer>().sharedMaterial != null) {
            GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", lineColor);
        }
    }

    // Optional: draw gizmos for debugging
    void OnDrawGizmosSelected() {
        if (source == null) return;
        var verts4 = source.GetCurrent4DVertices();
        var edges = source.GetEdges();
        for (int i = 0; i < edges.Count; i++) {
            var (a,b) = edges[i];
            var pa = Perspective4D.Project(verts4[a], source.wViewDistance);
            var pb = Perspective4D.Project(verts4[b], source.wViewDistance);
            Gizmos.color = lineColor;
            Gizmos.DrawLine(source.transform.TransformPoint(pa), source.transform.TransformPoint(pb));
        }
    }
}
