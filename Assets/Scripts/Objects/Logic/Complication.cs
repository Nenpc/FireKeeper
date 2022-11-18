namespace GameLogic
{
    public enum ComplicationType
    {
       Wind,
       Rain
    } 
    
    public abstract class Complication
    {
        protected ComplicationType complicationType;
        protected int duration;
    }
}