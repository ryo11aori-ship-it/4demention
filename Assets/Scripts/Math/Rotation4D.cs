using UnityEngine;

namespace FourD.Math {
    public static class Rotation4D {
        // Rotate in plane (a,b) by angle theta.
        // We represent coordinate indices: 0=x,1=y,2=z,3=w
        // Returns rotated vector, original not changed.

        public static Vec4 RotateXY(Vec4 v, float theta) {
            float c = Mathf.Cos(theta), s = Mathf.Sin(theta);
            return new Vec4(v.x*c - v.y*s, v.x*s + v.y*c, v.z, v.w);
        }

        public static Vec4 RotateXZ(Vec4 v, float theta) {
            float c = Mathf.Cos(theta), s = Mathf.Sin(theta);
            return new Vec4(v.x*c - v.z*s, v.y, v.x*s + v.z*c, v.w);
        }

        public static Vec4 RotateYZ(Vec4 v, float theta) {
            float c = Mathf.Cos(theta), s = Mathf.Sin(theta);
            return new Vec4(v.x, v.y*c - v.z*s, v.y*s + v.z*c, v.w);
        }

        public static Vec4 RotateXW(Vec4 v, float theta) {
            float c = Mathf.Cos(theta), s = Mathf.Sin(theta);
            return new Vec4(v.x*c - v.w*s, v.y, v.z, v.x*s + v.w*c);
        }

        public static Vec4 RotateYW(Vec4 v, float theta) {
            float c = Mathf.Cos(theta), s = Mathf.Sin(theta);
            return new Vec4(v.x, v.y*c - v.w*s, v.z, v.y*s + v.w*c);
        }

        public static Vec4 RotateZW(Vec4 v, float theta) {
            float c = Mathf.Cos(theta), s = Mathf.Sin(theta);
            return new Vec4(v.x, v.y, v.z*c - v.w*s, v.z*s + v.w*c);
        }
    }
}
