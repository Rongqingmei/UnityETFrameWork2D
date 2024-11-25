using Box2DSharp.Testbed.Unity;
using Testbed.TestCases;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.LockStepClient)]
    public class RollBackBox2D_StartRollBackLSUnit: AEvent<LSWorld, RollBackBox2D>
    {
        // 创建和角色绑定的Box2D的方块
        protected override async ETTask Run(LSWorld scene, RollBackBox2D args)
        {
            // long tLSUnitId = args.LSUnitId;

            Debug.Log("测试 InitBox2D_CreateBox2DBody");
            
            // 找到Box2DControllerSystem
            Scene tNowScene = scene.Scene();
            Room room = tNowScene.GetComponent<Room>();
            
            // Box2D 物理引擎
            Box2DControllerComponent tBox2DController = room.GetComponent<Box2DControllerComponent>();
            foreach (long argsLsUnitId in args.LSUnitIds)
            {
                Box2DComponent tBox2DComponent = tBox2DController.GetChild<Box2DComponent>(argsLsUnitId);
                Box2DComponentSystem.RollBackUpdateBox2D(tBox2DComponent);
            }

            await ETTask.CompletedTask;
        }
    }
}