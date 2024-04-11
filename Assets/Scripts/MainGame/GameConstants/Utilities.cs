
namespace Mahad.GameConstants
{
    public static class Utilities 
    {
        public delegate void VoidSignal();
        public static int DirectionToInt(Direction dir)
        {
            return dir == Direction.Right ? 1 : -1;
        }
    }
    public enum AIState
    {
        Patrol,
        InRange,
        MaintainDistance,
        Attack
    }

    public enum Direction
    {
        Left,
        Right
    }
    
   
}
