// DebugDrawCircle.cs
// Created by Cui Lingzhi
// on 2024 - 02 - 28

using UnityEngine;

namespace Script
{
    public class DebugDrawRect : LineRendererDebugger
    {
       public Geometry.Rect rectDefine;


        protected override void Draw()
        {
            rectDefine.center = transform.position;
            DrawRect(rectDefine);
        }

        /// <summary>
        /// 绘制一个矩形
        /// </summary>
        private void DrawRect(Geometry.Rect rect)
        {
            lineRenderer.positionCount =5;
            lineRenderer.SetPosition(0, rect.topLeft);
            lineRenderer.SetPosition(1, rect.bottomLeft);
            lineRenderer.SetPosition(2, rect.bottomRight);
            lineRenderer.SetPosition(3, rect.topRight);
            lineRenderer.SetPosition(4, rect.topLeft);
        }
    }
}