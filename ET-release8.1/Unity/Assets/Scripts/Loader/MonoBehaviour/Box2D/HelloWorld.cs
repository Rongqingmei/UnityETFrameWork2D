using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using Testbed.Abstractions;
using TrueSync;

namespace Testbed.TestCases
{
    public class HelloWorld : TestBase
    {
        public static HelloWorld Instance;
        private Body _body;

        public HelloWorld()
        {
            var groundBodyDef = new BodyDef {BodyType = BodyType.StaticBody};
            groundBodyDef.Position.Set(0.0f, -10.0f);

            var groundBody = World.CreateBody(groundBodyDef);

            var groundBox = new PolygonShape();
            groundBox.SetAsBox(1000.0f, 10.0f);

            groundBody.CreateFixture(groundBox, 0.0f);

            // Define the dynamic body. We set its position and call the body factory.
            var bodyDef = new BodyDef
            {
                BodyType = BodyType.DynamicBody,
                AllowSleep = false
            };

            bodyDef.Position.Set(0, 4f);

            var dynamicBox = new PolygonShape();
            dynamicBox.SetAsBox(1f, 1f, FVector2.Zero, 45f);

            // Define the dynamic body fixture.
            var fixtureDef = new FixtureDef
            {
                Shape = dynamicBox,
                Density = 1.0f,
                Friction = 0.3f
            };

            // Set the box density to be non-zero, so it will be dynamic.

            // Override the default friction.

            // Add the shape to the body.
            var body = World.CreateBody(bodyDef);
            body.CreateFixture(fixtureDef);

            _body = body;

            // for (int i = 0; i < 100; i++)
            // {
            //     bodyDef.Position = new FVector2(Random.Next(-50, 50), Random.Next(0, 500));
            //     bodyDef.Angle = Random.Next(0, 360);
            //     World.CreateBody(bodyDef).CreateFixture(fixtureDef);
            // }
        }

        // 创建一个
        public Body CreateOneBox2D(FP x, FP y)
        {
            var dynamicBox = new PolygonShape();
            dynamicBox.SetAsBox(1, 1, FVector2.Zero, 0);
            
            var fixtureDef = new FixtureDef
            {
                Shape = dynamicBox,
                Density = 1.0f,
                Friction = 0.3f
            };
            
            var bodyDef = new BodyDef
            {
                BodyType = BodyType.StaticBody,
                AllowSleep = false
            };
            
            bodyDef.Position = new FVector2(x, y);
            bodyDef.Angle = 0; //Random.Next(0, 360);
            Body tempBody = World.CreateBody(bodyDef);
            tempBody.CreateFixture(fixtureDef);

            return tempBody;
        }
    }
}