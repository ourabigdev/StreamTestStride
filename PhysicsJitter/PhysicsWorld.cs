using Jitter2;
using Jitter2.LinearMath;

namespace PhysicsJitter
{
    public static class PhysicsWorld
    {
        public static World World { get; private set; }

        static PhysicsWorld()
        {
            World = new World();
            World.SubstepCount = 4;
            World.Gravity = new JVector(0, -15.0f, 0);
        }
        
    }
}
