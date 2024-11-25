using Box2DSharp.Common;
using Box2DSharp.Testbed.Unity;
// using Box2DSharp.Testbed.Unity;
using Box2DSharp.Testbed.Unity.Inspection;
using Testbed.Abstractions;
using Testbed.TestCases;
// using Testbed.Abstractions;
// using Testbed.TestCases;
using UnityEngine;
using Camera = UnityEngine.Camera;

// using Box2DSharp.Testbed.Unity.Inspection;

namespace ET.Client
{
    [EntitySystemOf(typeof(Box2DShowComponent))]
    [FriendOf(typeof(Box2DShowComponent))]
    public static partial class Box2DShowComponentSystem
    {
        [EntitySystem]
        private static void Awake(this Box2DShowComponent self)
        {
            
        }
        
        [EntitySystem]
        private static void Update(this Box2DShowComponent self)
        {
            // Client 绘制Box2D物体
            // Debug.Log("渲染Box2D 画面 -----------------");
            if (HelloWorld.Instance != null)
            {
                HelloWorld.Instance.Render();
            }
        }
    }
}