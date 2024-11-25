using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

namespace Box2DSharp.Testbed.Unity.Inspection
{
    [ExecuteInEditMode]
    public class UnityDrawer : MonoBehaviour
    {
        public static UnityDrawer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private const string SceneCameraName = "SceneCamera";

        private const string MainCameraTag = "MainCamera";

        private static Material _lineMaterial;

        private readonly List<Color> _colors = new List<Color>();

        private readonly List<List<(Vector3 begin, Vector3 end)>> _lines = new List<List<(Vector3, Vector3)>>();

        private readonly List<(Vector3 Center, float Radius, Color color)> _points =
                new List<(Vector3 Center, float Radius, Color color)>();

        private static readonly int _srcBlend = Shader.PropertyToID("_SrcBlend");

        private static readonly int _dstBlend = Shader.PropertyToID("_DstBlend");

        private static readonly int _cull = Shader.PropertyToID("_Cull");

        private static readonly int _zWrite = Shader.PropertyToID("_ZWrite");

        public static UnityDrawer GetDrawer()
        {
            var drawLines = FindObjectOfType<UnityDrawer>();
            if (drawLines == default)
            {
                drawLines = GameObject.FindWithTag(MainCameraTag).AddComponent<UnityDrawer>();
            }

            return drawLines;
        }
        
        public void PostLines(List<(Vector3 begin, Vector3 end)> lines, Color color)
        {
            // Debug.Log("绘制线条------------------------增加");
            _lines.Add(lines);
            _colors.Add(color);
        }

        public void PostPoint((Vector3 Center, float Radius, Color color) point)
        {
            _points.Add(point);
        }

        /// <summary>
        /// 绘制线条
        /// </summary>
        private void DrawLinesByDebugDraw()
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                Color tColor = _colors[i];
                foreach (var line in _lines[i])
                {
                    Debug.DrawLine(line.begin, line.end, tColor);
                }
            }
        }
        
        private void Update()
        {
            // Debug.DrawLine(Vector3.zero, new Vector3(10,10,0), Color.red);
            // 绘制线条
            DrawLinesByDebugDraw();

            _lines.Clear();
            _colors.Clear();
            _points.Clear();
        }

        private static void CreateLineMaterial()
        {
            if (_lineMaterial)
            {
                return;
            }

            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            _lineMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };

            // Turn on alpha blending
            _lineMaterial.SetInt(_srcBlend, (int) BlendMode.SrcAlpha);
            _lineMaterial.SetInt(_dstBlend, (int) BlendMode.OneMinusSrcAlpha);

            // Turn backface culling off
            _lineMaterial.SetInt(_cull, (int) CullMode.Off);

            // Turn off depth writes
            _lineMaterial.SetInt(_zWrite, 0);
        }
    }
}