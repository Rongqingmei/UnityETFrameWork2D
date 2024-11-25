using System;
using Box2DSharp.Dynamics;
using ET.Client;

namespace ET
{
    [ChildOf(typeof(Box2DControllerComponent))]
    public class Box2DComponent: Entity, IAwake<InitBox2DBody>
    {
        public Body body;
        public EntityRef<LSUnit> Unit;
    }
}
