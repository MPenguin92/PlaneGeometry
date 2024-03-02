// Geometry.define.cs
// Created by Cui Lingzhi
// on 2024 - 02 - 28

using System;
using UnityEngine;

namespace Script
{
    public static partial class Geometry
    {
        [Serializable]
        public struct Circle
        {
            public Vector2 center;
            public float radius;

            public Circle(Vector2 center, float radius)
            {
                this.center = center;
                this.radius = radius;
            }
        }

        [Serializable]
        public struct Rect
        {
            public Vector2 center;
            public float width;
            public float height;

            public Vector2 topLeft => center + new Vector2(-0.5f * width, 0.5f * height);
            public Vector2 topRight => center + new Vector2(0.5f * width, 0.5f * height);
            public Vector2 bottomLeft => center + new Vector2(-0.5f * width, -0.5f * height);
            public Vector2 bottomRight => center + new Vector2(0.5f * width, -0.5f * height);

            public Rect(Vector2 center, float width, float height)
            {
                this.center = center;
                this.width = width;
                this.height = height;
            }
        }

        [Serializable]
        public struct Dot
        {
            public Vector2 center;

            public Dot(Vector2 center)
            {
                this.center = center;
            }
        }

        [Serializable]
        public struct Sector
        {
            public Vector2 center;
            public float radius;
            public float startAngle;
            public float centralAngle;
            public float endAngle => startAngle + centralAngle;

            public static Vector2 baseVec = new Vector2(0, 1);

            public Sector(Vector2 center, float radius, float startAngle, float  centralAngle)
            {
                this.center = center;
                this.radius = radius;
                this.startAngle = startAngle;
                this.centralAngle = centralAngle;
            }

            public Vector2 GetStartVector()
            {
                Quaternion q = Quaternion.Euler(0, 0, startAngle);
                return center + (Vector2)(q * (baseVec * radius));
            }

            public Vector2 GetEndVector()
            {
                Quaternion q = Quaternion.Euler(0, 0, centralAngle);
                return center + (Vector2)(q * (baseVec * radius));
            }
        }
    }
}