using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Jitter2.SoftBodies;
using Stride.Engine;

namespace PhysicsJitter
{
    public class BoxObject : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        private RigidBody body;
        private SoftBody sb;
        
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
            body.AddShape(new BoxShape(2, 2, 2));
            body.Position = new JVector(Entity.Transform.Position.X, Entity.Transform.Position.Y, Entity.Transform.Position.Z);
            body.SetMassInertia(1.0f);
        }

        public override void Update()
        {
            // Do stuff every new frame
            
            PhysicsWorld.World.Step(1.0f/ 60.0f, true);
            
            Entity.Transform.WorldMatrix = GetStrideTransform(body);
            JVector pos = body.Position;
            JQuaternion ori = body.Orientation;
            Entity.Transform.Position = new Vector3(pos.X, pos.Y, pos.Z);
            Entity.Transform.Rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            DebugText.Print($"box Y Position: {body.Position.Y}", new Int2(10, 30));
            
            body.SetActivationState(true);
        }
    }
}
