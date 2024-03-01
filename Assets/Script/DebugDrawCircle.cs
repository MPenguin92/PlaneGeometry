// DebugDrawCircle.cs
// Created by Cui Lingzhi
// on 2024 - 02 - 28

using UnityEngine;

namespace Script
{
    public class DebugDrawCircle : LineRendererDebugger
    {
       public Geometry.Circle circleDefine;


        protected override void Draw()
        {
            circleDefine.center = transform.position;
            DrawCircle(circleDefine);
        }

        /// <summary>
        /// 绘制一个圆
        /// </summary>
        private void DrawCircle(Geometry.Circle c)
        {
            Vector2 center = c.center;
            Vector2 topPoint = new Vector2(0, c.radius);

            float minEuler = 10;
            lineRenderer.positionCount = (int)(360 / minEuler) + 1;
            int index = 0;
            for (float e = 0; e <= 360; e += minEuler)
            {
                Quaternion q = Quaternion.Euler(0, 0, e);
                Vector2 p = center + (Vector2)(q * topPoint);
                lineRenderer.SetPosition(index++, p);
            }
        }
    }
}