using Box2DSharp.Common;
using Box2DSharp.Testbed.Unity;
// using Box2DSharp.Testbed.Unity;
using Box2DSharp.Testbed.Unity.Inspection;
using Testbed.Abstractions;
using Testbed.TestCases;
using TrueSync;
// using Testbed.Abstractions;
// using Testbed.TestCases;
using UnityEngine;
using Camera = UnityEngine.Camera;

// using Box2DSharp.Testbed.Unity.Inspection;

namespace ET.Client
{
    // [LSEntitySystemOf(typeof(Box2DControllerComponent))]
    [EntitySystemOf(typeof(Box2DControllerComponent))]
    [FriendOf(typeof(Box2DControllerComponent))]
    public static partial class Box2DControllerComponentSystem
    {
        [EntitySystem]
        private static void Destroy(this Box2DControllerComponent self)
        {
            int a = 10;
            Debug.LogError("打印一下为什么这里删除了");
        }

        [EntitySystem]
        private static void Awake(this Box2DControllerComponent self)
        {
            // 初始化BOX2D的帧
            // Debug.Log("加载Box2D ----------------3");
            InitBox2D();
            // Debug.Log("加载Box2D ----------------4");
            
            // 给每个角色添加BOX2D
            AddPlayerBox(self);
            // LSWorld lsWorld = room.LSWorld;
            // lsWorld.AddComponent<Box2DControllerComponent>();
        }

        // 给每个角色添加BOX2D
        // public static void SetMainScene(this Box2DControllerComponent self, Scene tScene)
        // {
        //     self.mainScene = tScene;
        // }

        // 给每个角色添加BOX2D
        private static void AddPlayerBox(this Box2DControllerComponent self)
        {
            // 给每个LSUnit加上Box2D 再在当前节点

            // Room room = self.Room();
            // LSUnitComponent lsUnitComponent = room.LSWorld.GetComponent<LSUnitComponent>();
            // Scene root = self.Root();
            // foreach (long playerId in room.PlayerIds)
            // {
            //     LSUnit lsUnit = lsUnitComponent.GetChild<LSUnit>(playerId);
            //     // 给 lsUnit 添加上 Box2D组件
            //     lsUnit.AddComponent<Box2DComponent>();
            // }
            
            // Room room = self.Room();
            // LSUnitComponent lsUnitComponent = room.LSWorld.GetComponent<LSUnitComponent>();
            // Scene root = self.Root();
            // foreach (long playerId in room.PlayerIds)
            // {
            //     LSUnit lsUnit = lsUnitComponent.GetChild<LSUnit>(playerId);
            //     // LSUnitView lsUnitView = self.AddChildWithId<LSUnitView, GameObject>(lsUnit.Id, unitGo);
            //     // lsUnitView.AddComponent<LSAnimatorComponent>();
            //     
            //     // 根据 LSUnity创建 Component 这样之后就能拿到LSUnit
            //     self.AddChildWithId<Box2DComponent>(lsUnit.Id);
            // }
        }
        
        // 这个函数没用，不知道为啥
        // [EntitySystem]
        // private static void Update(this Box2DControllerComponent self)
        // {
        //     // Client 绘制Box2D物体
        //     Debug.Log("渲染Box2D 画面 -----------------");
        //     HelloWorld.Instance.Render(); 
        // }

        // [EntitySystem]
        // private static void Update(this Box2DControllerComponent self)
        [EntitySystem]
        private static void Update(this Box2DControllerComponent self)
        {
        // 更新BOX2D的帧
        // Debug.Log("加载Box2D 更新 ----------------5");
        HelloWorld.Instance.Render(); 
             
        //     // 获取权威帧时间
        //     // Room room = self.GetParent<Room>();
        //     // long tFrameTime = room.FixedTimeCounter.FrameTime(room.AuthorityFrame);
        //
        //     // 所有人都一样，一个服务器帧，物理系统就运行 0.033f 秒
        //     float tFrameTime = 0.033f;
        //     
        //     // Box2D 跑物理帧
        //     CustomFixedUpdate.Instance.Update(tFrameTime);
        }

        private static void InitBox2D()
        {
            // 配置显示器组装
            UnityDrawer box2DDrawer = Camera.main.gameObject.GetComponent<UnityDrawer>();
            if (box2DDrawer == null)
            {
                // 如果没有就创建一个
                box2DDrawer = Camera.main.gameObject.AddComponent<UnityDrawer>();
            }

            // 配置显示器
            DebugDrawer.GetInstance().Drawer = box2DDrawer;

            // 配置BOX2D参数
            TestSettings TestSettings = new TestSettings();

            // 配置物理帧更新
            CustomFixedUpdate.Instance = new CustomFixedUpdate((FP.One / 60).AsFloat(),
                time =>
                {
                    // BOX2D 的每物理帧调用
                    RunBox2DTick();
                });

            // 配置输入组件
            // Input = new UnityInput();

            // 构建生成物体模型
            HelloWorld.Instance = new HelloWorld();
            if (HelloWorld.Instance != null)
            {
                // helloworld.Input = Input; 
                HelloWorld.Instance.Drawer = DebugDrawer.GetInstance();
                HelloWorld.Instance.TestSettings = TestSettings;
                HelloWorld.Instance.World.Drawer = DebugDrawer.GetInstance();
                HelloWorld.Instance.TextIncrement = 20;
            }
        }

        // box2d的帧
        private static void RunBox2DTick()
        {
            HelloWorld.Instance.Step();
        }
    }
}