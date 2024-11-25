using Box2DSharp.Testbed.Unity;
using Testbed.TestCases;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.LockStepClient)]
    public class UpdateBox2D_DriveBox2D: AEvent<LSWorld, UpdateBox2D>
    {
        protected override async ETTask Run(LSWorld scene, UpdateBox2D args)
        {
            long[] tLSUnitIds = args.LSUnitIds;
            // GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            // if (gameObjectComponent == null)
            // {
            //     return;
            // }
            //
            // Transform transform = gameObjectComponent.Transform;
            // transform.position = unit.Position;
            
            Debug.Log("测试UpdateBox2D_DriveBox2D");
            
            // 更新BOX2D的帧
            // Debug.Log("加载Box2D 更新 ----------------5");
            // HelloWorld.Instance.Render(); 
            
            // 获取权威帧时间
            // Room room = self.GetParent<Room>();
            // long tFrameTime = room.FixedTimeCounter.FrameTime(room.AuthorityFrame);
            
            // 所有人都一样，一个服务器帧，物理系统就运行 0.033f 秒
            float tFrameTime = 0.033f;
            
            // Box2D 跑物理帧
            CustomFixedUpdate.Instance.Update(tFrameTime);
            
            // 更新每个Box2D组件, 记录所有LSUnit的帧数据
            Room tNowRoom = scene.Parent as Room;
            
            Scene tNowScene = scene.Root();
            Room room = tNowScene.GetComponent<Room>();
            
            // 更新每个Box2D组件
            Box2DControllerComponent tBox2DController = room.GetComponent<Box2DControllerComponent>();
            foreach (long argsLsUnitId in args.LSUnitIds)
            {
                Box2DComponent tBox2DComponent = tBox2DController.GetChild<Box2DComponent>(argsLsUnitId);
                Box2DComponentSystem.LSUpdate(tBox2DComponent);
            }
            
            
            await ETTask.CompletedTask;
        }
    }
}