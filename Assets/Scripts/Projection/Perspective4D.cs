using UnityEngine;
using FourD.Math;

namespace FourD.Projection {
    public static class Perspective4D {
        // Projects a 4D point into 3D using perspective along w.
        // d is distance from 3D camera to 4D origin along w (viewer position on w-axis).
        // Returns Vector3 in 3D coordinate space.
        public static Vector3 Project(Vec4 p, float d = 3.0f) {
            float denom = (d - p.w);
            // Avoid division by zero and extreme values
            if (Mathf.Abs(denom) < 1e-4f) denom = Mathf.Sign(denom) * 1e-4f;
            float inv = 1.0f / denom;
            return new Vector3(p.x * inv, p.y * inv, p.z * inv);
        }

        // Parallel (orthographic) projection: drop w
        public static Vector3 Orthographic(Vec4 p) {
            return new Vector3(p.x, p.y, p.z);
        }
    }
}
