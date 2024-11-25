using Box2DSharp.Common;
using Testbed.TestCases;
using TrueSync;
using UnityEngine;

namespace ET.Client
{

    // [LSEntitySystemOf(typeof(Box2DComponent))]
    [EntitySystemOf(typeof(Box2DComponent))]
    [FriendOf(typeof(Box2DComponent))]
    public static partial class Box2DComponentSystem
    {
        [EntitySystem]
        private static void Awake(this Box2DComponent self, InitBox2DBody box2DInfo)
        {
            // 创建一个BOX2D
            self.body = HelloWorld.Instance.CreateOneBox2D(box2DInfo.Position.x, box2DInfo.Position.y);
        }

        public static void LSUpdate(this Box2DComponent self)
        {
            // // 获取单位的 GameObject
            // LSUnit lsUnit = GetUnit(self);
            //
            // // 把box2D的位置赋值给 GameObjectComponent
            // FVector2 tBox2DPos = self.body.GetPosition();
            //
            // // 赋值外在的表现
            // TSVector oldUnitPos = lsUnit.Position;
            //
            // // Box2D 和 LS 定点数转换
            // TrueSync.FP tX = tBox2DPos.X;
            // TrueSync.FP tY = tBox2DPos.Y;
            //
            // //记录当前box2D的位置，用来回退
            // lsUnit.Position = new TSVector(tX, tY, 0.0f);
            // lsUnit.Forward = lsUnit.Position - oldUnitPos;
            //
            // // 记录当前box2D的角度，用来回退
            // lsUnit.Rotation = TSQuaternion.Euler(0, 0, self.body.GetTransform().Rotation.Angle);
            //
            // // 记录当前 box2D 的 AngularVelocity，用来回退
            // lsUnit.AngularVelocity = self.body.AngularVelocity;
            //
            // // 记录当前 box2D 的 LinearVelocityY，用来回退
            // lsUnit.LinearVelocity = new TSVector2(self.body.LinearVelocity.X, self.body.LinearVelocity.Y);
        }

        /// <summary>
        /// 回退 Box2D
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pLSInput"></param>
        public static void RollBackUpdateBox2D(this Box2DComponent self)
        {
            // Debug.Log("---------------打印，RollBackUpdateBox2D------------------");
            //
            // // 获取单位的 GameObject
            // LSUnit lsUnit = GetUnit(self);
            //
            // // 强制更新位置
            // // Box2DSharp.Common.FP v2X = new Box2DSharp.Common.FP(lsUnit.Position.x._serializedValue);
            // // Box2DSharp.Common.FP v2Y = new Box2DSharp.Common.FP(lsUnit.Position.y._serializedValue);
            // FVector2 newBox2DPos = new FVector2(lsUnit.Position.x, lsUnit.Position.y);
            //
            // // 还原 Rotation
            // // Box2DSharp.Common.FP tRot = new Box2DSharp.Common.FP(lsUnit.Rotation.eulerAngles.z._serializedValue);
            //
            // // 还原 Box2D 的位置
            // self.body.SetTransform(newBox2DPos, lsUnit.Rotation.eulerAngles.z);
            //
            // // 还原 AngularVelocity
            // // Box2DSharp.Common.FP tAngularV = new Box2DSharp.Common.FP(lsUnit.AngularVelocity._serializedValue);
            // self.body.SetAngularVelocity(lsUnit.AngularVelocity);
            //
            // // 还原 SetLinearVelocity
            // // Box2DSharp.Common.FP tLinearV2X = new Box2DSharp.Common.FP(lsUnit.LinearVelocity.x._serializedValue);
            // // Box2DSharp.Common.FP tLinearV2Y = new Box2DSharp.Common.FP(lsUnit.LinearVelocity.y._serializedValue);
            // FVector2 tLinearVelocity = new FVector2(lsUnit.LinearVelocity.x, lsUnit.LinearVelocity.y);
            // self.body.SetLinearVelocity(tLinearVelocity);
            //
            // // 刷新数值
            // LSUpdate(self);
        }

        /// <summary>
        /// 输入并且更新 Box2D
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pLSInput"></param>
        public static void InputeAndUpdateBox2D(this Box2DComponent self, LSInput pLSInput)
        {
            // 获取单位的 GameObject
            LSUnit lsUnit = GetUnit(self);
            
            FVector2 tBox2DPos = self.body.GetPosition();
            // LSInputComponent lsInputComp = lsUnit.GetComponent<LSInputComponent>();
            
            TSVector2 v2 = pLSInput.V * 6 * 50 / 1000;
            
            // if (v2.LengthSquared().AsFloat() < 0.0001f)
            // {
            //     // 移动太小，直接赋值
            //     lsUnit.Position = new TSVector(tBox2DPos.X.AsFloat(), tBox2DPos.Y.AsFloat(), 0.0f);
            //     return;
            // }

            // 应用走路
            FVector2 oldBox2DPos = tBox2DPos;
            FVector2 newBox2DPos = oldBox2DPos + new FVector2(v2.x, v2.y);
            
            // 设置 Box2D 的位置和角度
            self.body.SetTransform(newBox2DPos, 0);

            // 测试应用走路
            // lsUnit.Position += new TSVector(v2.x, v2.y, 0.0f);
            // lsUnit.Rotation = TSQuaternion.Euler(0, 0, 0);
            
            // LSUpdate(self);
        }

        public static FVector2 GetBodyPos(this Box2DComponent self)
        {
            return self.body.GetPosition();
        }
        
        /// <summary>
        /// 根据 Box2dComponet 记录的id找到对应的 LSUnit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        private static LSUnit GetUnit(this Box2DComponent self)
        {
            LSUnit unit = self.Unit;
            if (unit != null)
            {
                return unit;
            }

            self.Unit = (self.IScene as Room).LSWorld.GetComponent<LSUnitComponent>().GetChild<LSUnit>(self.Id);
            return self.Unit;
        }
    }
}