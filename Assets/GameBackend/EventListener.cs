namespace GameBackend
{
    [System.Serializable]
    public class EventArgs
    {
        public string name;
    }
    
    public interface IEntityEventListener
    {
        public void eventActive<T>(T eventArgs) where T : EventArgs;
        public void registrarTarget(Entity target);
        public void removeSelf();
    }
}