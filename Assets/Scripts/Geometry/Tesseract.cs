using System.Collections.Generic;
using UnityEngine;
using FourD.Math;

namespace FourD.Geometry {
    public class Tesseract {
        // Generates 4D hypercube vertices and edge index pairs.
        // center optional, size radius.
        public static void Generate(float size, out List<Vec4> vertices, out List<(int,int)> edges) {
            vertices = new List<Vec4>(16);
            // vertices: all combinations of ±size on each axis (x,y,z,w)
            for (int xi = -1; xi <= 1; xi += 2) {
                for (int yi = -1; yi <= 1; yi += 2) {
                    for (int zi = -1; zi <= 1; zi += 2) {
                        for (int wi = -1; wi <= 1; wi += 2) {
                            vertices.Add(new Vec4(xi * size, yi * size, zi * size, wi * size));
                        }
                    }
                }
            }

            edges = new List<(int,int)>();
            int n = vertices.Count; // 16
            // Two vertices are connected if they differ in exactly one coordinate.
            for (int i = 0; i < n; i++) {
                for (int j = i+1; j < n; j++) {
                    int diff = 0;
                    if (!Mathf.Approximately(vertices[i].x, vertices[j].x)) diff++;
                    if (!Mathf.Approximately(vertices[i].y, vertices[j].y)) diff++;
                    if (!Mathf.Approximately(vertices[i].z, vertices[j].z)) diff++;
                    if (!Mathf.Approximately(vertices[i].w, vertices[j].w)) diff++;
                    if (diff == 1) edges.Add((i,j));
                }
            }
        }
    }
}
