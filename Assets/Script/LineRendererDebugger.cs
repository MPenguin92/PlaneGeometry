using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(LineRenderer))]
    public abstract class LineRendererDebugger : MonoBehaviour
    {
        [SerializeField] protected LineRenderer lineRenderer;
        [SerializeField] protected Color color;
        [SerializeField] protected Material mat;

        private static readonly int Color1 = Shader.PropertyToID("_Color");

        // Start is called before the first frame update
        void Start()
        {
            if (lineRenderer == null)
            {
                lineRenderer = this.GetComponent<LineRenderer>();
            }

            lineRenderer.material = mat;
          
        }

        protected abstract void Draw();


        private void Update()
        {
            lineRenderer.material.SetColor(Color1,color);
            Draw();
            //Debug.Log(GeometryDefine.DotAndCircle(dot, circleDefine));
            //Debug.Log(GeometryDefine.TwoCirclesIntersect(circleDefine, circleDefine2));
        }

    }
}