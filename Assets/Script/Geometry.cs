// CircleDefine.cs
// Created by Cui Lingzhi
// on 2024 - 02 - 26

using System;
using UnityEngine;

namespace Script
{
    public static partial class Geometry
    {
        public enum DotGeometryR
        {
            In,
            Out,
            On,
        }

        /// <summary>
        /// 点和圆的关系
        /// </summary>
        public static DotGeometryR DotAndCircle(Vector2 dot, Circle circle)
        {
            float num1 = dot.x - circle.center.x;
            float num2 = dot.y - circle.center.y;
            float distSqrt = num1 * num1 + num2 * num2;
            float radiusSqrt = circle.radius * circle.radius;

            if (Mathf.Abs(distSqrt - radiusSqrt) < 0.0001f)
            {
                return DotGeometryR.On;
            }

            if (distSqrt > radiusSqrt)
            {
                return DotGeometryR.Out;
            }

            return DotGeometryR.In;
        }

        /// <summary>
        /// 两圆是否相交
        /// </summary>
        public static bool TwoCirclesIntersect(Circle c1, Circle c2)
        {
            c2.radius += c1.radius;
            DotGeometryR r = DotAndCircle(c1.center, c2);
            if (r == DotGeometryR.Out)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///  点和矩形
        /// </summary>
        public static DotGeometryR DotAndRect(Vector2 dot, Rect rect)
        {
            float dot1 = GetRectDot(dot,rect.topLeft, rect.bottomLeft);
            float dot2 = GetRectDot(dot,rect.bottomLeft, rect.bottomRight);
            float dot3 = GetRectDot(dot,rect.bottomRight, rect.topRight);
            float dot4 = GetRectDot(dot,rect.topRight, rect.topLeft);
            
            //Debug.Log($"{dot1}=={dot2}=={dot3}=={dot4}");
            if (dot1 < 0 || dot2 < 0 || dot3 < 0 || dot4 < 0)
            {
                return DotGeometryR.Out;
            }

            if (Mathf.Approximately(dot1, 0) ||
                Mathf.Approximately(dot2, 0) ||
                Mathf.Approximately(dot3, 0) ||
                Mathf.Approximately(dot4, 0))
            {
                return DotGeometryR.On;
            }

            return DotGeometryR.In;
        }

        private static float GetRectDot(Vector2 dot,Vector2 vertex1,Vector2 vertex2)
        {
            Vector2 edge1 = dot - vertex1;
            Vector2 edge2 = vertex2 - vertex1;
            return Vector2.Dot(edge1, edge2);
        }

        /// <summary>
        ///  两矩形相交
        /// </summary>
        public static bool TwoRectIntersect(Vector2 dot, Circle circle)
        {
            return true;
        }

        /// <summary>
        ///  点和扇形
        /// </summary>
        public static DotGeometryR DotAndSector(Vector2 dot, Sector sector)
        {
            float num1 = dot.x - sector.center.x;
            float num2 = dot.y - sector.center.y;
            float distSqrt = num1 * num1 + num2 * num2;
            float radiusSqrt = sector.radius * sector.radius;
            //距离超过半径，肯定在扇形之外
            if (distSqrt > radiusSqrt)
            {
                return DotGeometryR.Out;
            }

            //把扇形搞到(0,0)点
            dot -= sector.center;
            sector.center = Vector2.zero;
            //并对齐(0,1)
           Quaternion q =  Quaternion.Euler(0, 0, -sector.startAngle);
           dot = q * dot;
           sector.startAngle = 0;
           var signedAngle = Vector2.SignedAngle(Sector.baseVec, dot);
           //判断此时目标向量和(0,1)的夹角是否在圆心角范围内
           if (signedAngle < 0 || signedAngle > sector.centralAngle)
           {
               return DotGeometryR.Out;
           }
           
           if (Mathf.Abs(distSqrt - radiusSqrt) < 0.0001f)
           {
               return DotGeometryR.On;
           }


            return DotGeometryR.In;
        }

        /// <summary>
        /// 两扇形是否相交
        /// </summary>
        public static bool TwoSectorIntersect(Sector s1, Sector s2)
        {
            float num1 = s1.center.x - s2.center.x;
            float num2 = s1.center.y - s2.center.y;
            float distSqrt = num1 * num1 + num2 * num2;
            float radiusSum = s1.radius + s2.radius;
            float radiusSqrt = radiusSum * radiusSum;
            //距离超过半径=>不相交
            if (distSqrt > radiusSqrt)
            {
                return false;
            }
            //考虑到>180°的凹扇形，没有很好的办法。只能尝试细分扇形，判断是否有交点划过另一个扇形。
            if (CheckSector(s1, s2)) return true;
            if (CheckSector(s2, s1)) return true;

            return false;
        }

        private static bool CheckSector(Sector s1, Sector s2)
        {
            Vector2 topPoint = Geometry.Sector.baseVec * s1.radius;
            float minEuler = 1;

            for (float e = s1.startAngle; e <= s1.endAngle; e += minEuler)
            {
                Quaternion q = Quaternion.Euler(0, 0, e);
                Vector2 p = s1.center + (Vector2)(q * topPoint);
                if (DotAndSector(p, s2) != DotGeometryR.Out)
                {
                    return true;
                }
            }

            return false;
        }
    }
}