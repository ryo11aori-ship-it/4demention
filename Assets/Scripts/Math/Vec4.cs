using System;
using UnityEngine;

namespace FourD.Math {
    [Serializable]
    public struct Vec4 {
        public float x, y, z, w;
        public Vec4(float x, float y, float z, float w) {
            this.x = x; this.y = y; this.z = z; this.w = w;
        }

        public static Vec4 operator +(Vec4 a, Vec4 b) => new Vec4(a.x+b.x, a.y+b.y, a.z+b.z, a.w+b.w);
        public static Vec4 operator -(Vec4 a, Vec4 b) => new Vec4(a.x-b.x, a.y-b.y, a.z-b.z, a.w-b.w);
        public static Vec4 operator *(Vec4 a, float s) => new Vec4(a.x*s, a.y*s, a.z*s, a.w*s);
        public static Vec4 operator /(Vec4 a, float s) => new Vec4(a.x/s, a.y/s, a.z/s, a.w/s);

        public float Dot(Vec4 other) => x*other.x + y*other.y + z*other.z + w*other.w;
        public float Magnitude => Mathf.Sqrt(Dot(this));
        public Vec4 Normalized {
            get {
                var m = Magnitude;
                if (m == 0) return new Vec4(0,0,0,0);
                return this / m;
            }
        }

        public override string ToString() => $"({x:F3},{y:F3},{z:F3},{w:F3})";

        public Vector3 ToVector3() => new Vector3(x, y, z);
    }
}
