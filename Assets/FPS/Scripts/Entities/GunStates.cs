namespace FPS.Scripts.Entities
{
    public class GunIdleState : State
    {
        public GunIdleState() : base()
        {
            state = GunState.IDLE;
        }
    
        public void Enter()
        {
            base.Enter();
        }
    }
}
