using Box2DSharp.Testbed.Unity;
using Testbed.TestCases;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.All)]
    public class InputBox2D_InputAndMoveLSUnit: AEvent<Scene, InputBox2DBody>
    {
        // 发送角色控制的输入
        protected override async ETTask Run(Scene scene, InputBox2DBody args)
        {
            long tLSUnitId = args.LSUnitId;
            LSInput tLSInput= args.lsInput;

            Debug.Log("测试 InitBox2D_CreateBox2DBody");
            
            // 找到Box2DControllerSystem
            Scene tNowScene = scene.Scene();
            Room room = tNowScene.GetComponent<Room>();
            
            // Box2D 物理引擎
            Box2DControllerComponent tBox2DController = room.GetComponent<Box2DControllerComponent>();
            Box2DComponent box2DComponent = tBox2DController.GetChild<Box2DComponent>(tLSUnitId);
            
            // 输入Box2D物理的变化
            Box2DComponentSystem.InputeAndUpdateBox2D(box2DComponent, tLSInput);
            // Box2DComponent tBox2DComponent = tBox2DController.AddChild<Box2DComponent, InitBox2DBody>(args);

            await ETTask.CompletedTask;
        }
    }
}