using Box2DSharp.Testbed.Unity;
using Testbed.TestCases;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.All)]
    public class InitBox2D_CreateBox2DBody: AEvent<Scene, InitBox2DBody>
    {
        // 创建和角色绑定的Box2D的方块
        protected override async ETTask Run(Scene scene, InitBox2DBody args)
        {
            long tLSUnitId = args.LSUnitId;

            Debug.Log("测试 InitBox2D_CreateBox2DBody");
            
            // 找到Box2DControllerSystem
            // Scene tNowScene = scene.Root();
            Scene tNowScene = LSSingleton.mainScene;
            Room room = tNowScene.GetComponent<Room>(); 
            // Room room = scene.Parent as Room;
            
            // Box2D 物理引擎
            Box2DControllerComponent tBox2DController = room.GetComponent<Box2DControllerComponent>();
            Box2DComponent tBox2DComponent = tBox2DController.AddChildWithId<Box2DComponent, InitBox2DBody>(tLSUnitId, args);

            await ETTask.CompletedTask;
        }
    }
}