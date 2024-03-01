// Main.cs
// Created by Cui Lingzhi
// on 2024 - 02 - 28

using System;
using System.Text;
using UnityEngine;

namespace Script
{
    public class Main : MonoBehaviour
    {
        public Transform dot1;
        public DebugDrawCircle circle1;
        public DebugDrawCircle circle2;

        public DebugDrawSector sector1;
        public DebugDrawSector sector2;
        private void Update()
        {
            // Log("dot1", Geometry.DotAndCircle(dot1.position, circle1.circleDefine), "circle1");
            //
            // Log("circle1 circle2 Intersect", Geometry.TwoCirclesIntersect(circle1.circleDefine, circle2.circleDefine));
            
            Log("dot1", Geometry.DotAndSector(dot1.position, sector2.sectorDefine), "sector2");
            
            Log("sector1", Geometry.TwoSectorIntersect(sector1.sectorDefine, sector2.sectorDefine), "sector2");
        }

        private readonly StringBuilder mStringBuilder = new StringBuilder();

        private void Log(params object[] s)
        {
            mStringBuilder.Clear();
            for (int i = 0; i < s.Length - 1; i++)
            {
                mStringBuilder.Append(s[i]);

                mStringBuilder.Append(',');
            }
            mStringBuilder.Append(s[^1]);
            Debug.Log(mStringBuilder.ToString());
        }
    }
}