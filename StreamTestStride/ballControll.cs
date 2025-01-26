using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.BepuPhysics;
using Stride.Engine;
using Stride.BepuPhysics.Components;

namespace StreamTestStride
{
    public class ballControll : SyncScript, ISimulationUpdate
    {
        // Declared public member fields and properties will show in the game studio
        private BodyComponent rb;
        private bool isGrounded = false;

        public float speed = 3.5f;
        public CollisionMask collisionMask;

        public override void Start()
        {
            // Initialization of the script.
            rb = Entity.Get<BodyComponent>();
        }

        public override void Update()
        {
            // Do stuff every new frame
            rb.Awake = true;
        }
        
        /*
         this is like an update function but used for bepu physics engine simulation update
         aka use it for anything related to the bepu physics engine
         */
        public void SimulationUpdate(BepuSimulation simulation, float simTimeStep)
        {
            if (Input.IsKeyDown(Keys.Z))
            {
                rb.ApplyLinearImpulse(-Vector3.UnitZ * speed * simTimeStep);
            }
            if (Input.IsKeyDown(Keys.S))
            {
                rb.ApplyLinearImpulse(Vector3.UnitZ * speed * simTimeStep);
            }
            if (Input.IsKeyDown(Keys.Q))
            {
                rb.ApplyLinearImpulse(-Vector3.UnitX * speed * simTimeStep);
            }
            if (Input.IsKeyDown(Keys.D))
            {
                rb.ApplyLinearImpulse(Vector3.UnitX * speed * simTimeStep);
            }

            //RayCast to check if the ball is grounded
            if (simulation.RayCast(Entity.Transform.Position, -Vector3.UnitY, .7f, out HitInfo result, collisionMask))
            {
                isGrounded = true;
                DebugText.Print("Grounded", new Int2(20, 20));
            }
            else
            {
                isGrounded = false;
                DebugText.Print("Not Grounded", new Int2(20, 20));
            }
            
            //when the ball is grounded you are able to jump
            if (isGrounded && Input.IsKeyDown(Keys.Space))
            {
                rb.ApplyLinearImpulse(Vector3.UnitY * 20.0f * simTimeStep);
                isGrounded = false;
            }
        }

        public void AfterSimulationUpdate(BepuSimulation simulation, float simTimeStep)
        {
            
        }
    }
}
