using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using SharpDX.MediaFoundation;
using Stride.Engine;
using Quaternion = Stride.Core.Mathematics.Quaternion;

namespace PhysicsJitter
{
    public class ball : SyncScript
    {
        // Declared public member fields and properties will show in the game studio

        private RigidBody body;
        
        private float accumulatedTime = 0.0f;
        private const float fixedTimeStep = 1.0f / 60.0f;
        private float speed = 7000.0f;
        
        static Matrix GetStrideTransform(RigidBody body)
        {
            JMatrix ori = JMatrix.CreateFromQuaternion(body.Orientation);
            JVector pos = body.Position;
            
            return new Matrix(ori.M11, ori.M12, ori.M13, pos.X,
                ori.M21, ori.M22, ori.M23, pos.Y,
                ori.M31, ori.M32, ori.M33, pos.Z,
                0, 0, 0, 1.0f);
        }

        public override void Start()
        {
            // Initialization of the script.
            
            body = PhysicsWorld.World.CreateRigidBody();
            body.AddShape(new SphereShape(.9f));
            body.Position = new JVector(Entity.Transform.Position.X, Entity.Transform.Position.Y, Entity.Transform.Position.Z);

            body.Damping = (0.05f, 0.05f);
            //body.SetMassInertia(100.0f);

            body.Restitution = 0.05f;
            body.Friction = 1.0f;
        }
        
        
        
        public override void Update()
        {
            // Do stuff every new frame
            
            
            
            accumulatedTime += (float)Game.UpdateTime.Elapsed.TotalSeconds;
            
            while (accumulatedTime >= fixedTimeStep)
            {
                PhysicsWorld.World.Step(fixedTimeStep, true);
                accumulatedTime -= fixedTimeStep;
            }
            
            Entity.Transform.WorldMatrix = GetStrideTransform(body);
            JVector pos = body.Position;
            JQuaternion ori = body.Orientation;
            Entity.Transform.Position = new Vector3(pos.X, pos.Y, pos.Z);
            Entity.Transform.Rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            DebugText.Print($"Ball Y Position: {body.Position.Y}", new Int2(10, 10));
            
            body.SetActivationState(true);
            
            if (Input.IsKeyDown(Keys.D))
            {
                body.AddForce(new JVector(speed * fixedTimeStep, 0, 0));
            }
            if (Input.IsKeyDown(Keys.Q))
            {
                body.AddForce(new JVector(-speed * fixedTimeStep, 0, 0));
            }
            if (Input.IsKeyDown(Keys.Z))
            {
                body.AddForce(new JVector(0, 0, -speed * fixedTimeStep));
            }
            if (Input.IsKeyDown(Keys.S))
            {
                body.AddForce(new JVector(0, 0, speed * fixedTimeStep));
            }
            
        }
    }
}
