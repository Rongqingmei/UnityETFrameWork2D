using System;

using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Room))]
    // public class Box2DControllerComponent: Entity, IAwake, IUpdate
    public class Box2DControllerComponent: Entity, IAwake, IUpdate, IDestroy
    {
        // public EntityRef<Scene> mainScene;
    }
}
