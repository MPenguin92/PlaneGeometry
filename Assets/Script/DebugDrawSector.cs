// DebugDrawSector.cs
// Created by Cui Lingzhi
// on 2024 - 02 - 28

using UnityEngine;

namespace Script
{
    public class DebugDrawSector : LineRendererDebugger
    {
        public Geometry.Sector sectorDefine;
        protected override void Draw()
        {
            sectorDefine.center = transform.position;
            DrawSector(sectorDefine);
        }

        private void DrawSector(Geometry.Sector s)
        {
            Vector2 center = s.center;
            Vector2 topPoint = Geometry.Sector.baseVec *  s.radius;
            float minEuler = 1;
            float max = s.centralAngle;
            lineRenderer.positionCount = (int)( max / minEuler) + 3;
            int index = 0;
            lineRenderer.SetPosition(index++, center);
            for (float e = s.startAngle; e <= s.endAngle; e += minEuler)
            {
                Quaternion q = Quaternion.Euler(0, 0, e);
                Vector2 p = center + (Vector2)(q * topPoint);
                lineRenderer.SetPosition(index++, p);
            }
            lineRenderer.SetPosition(index, center);
        }
    }
}