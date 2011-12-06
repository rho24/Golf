namespace Golf.Core.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        public void Start() {
            Running = true;
        }

        public bool Running { get; private set; }
    }
}