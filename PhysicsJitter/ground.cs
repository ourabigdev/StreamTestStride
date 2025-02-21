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
using Stride.Engine;

namespace PhysicsJitter
{
    public class ground : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        private RigidBody body;
        
        public override void Start()
        {

            // Create static ground body
            body = PhysicsWorld.World.CreateRigidBody();
            body.AddShape(new BoxShape(50, 0.00000001f, 50)); 
            body.Position = new JVector(Entity.Transform.Position.X, 
                Entity.Transform.Position.Y - 0.5f, 
                Entity.Transform.Position.Z);
            body.IsStatic = true; // Makes it not move
        }

        public override void Update()
        {
            PhysicsWorld.World.Step(1.0f / 60.0f, true);
        }
    }
}
