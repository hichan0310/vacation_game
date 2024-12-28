namespace GameBackend.Status
{
    public interface IPlayerInfo
    {
        public PlayerStatus getPlayerStatus();
        public string name { get; }
        public string explaination { get; }
    }

    public class CommonPlayerInfo : IPlayerInfo
    {
        public string name { get; }
        public string explaination { get; }

        public CommonPlayerInfo()
        {
            this.name = "name";
            this.explaination = "empty";
        }

        public PlayerStatus getPlayerStatus()
        {
            return new PlayerStatus(10000, 1000, 1000);
        }
    }
}