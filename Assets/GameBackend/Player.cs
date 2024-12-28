using System.Collections.Generic;
using GameBackend.Status;

namespace GameBackend
{
    public class Player<T> : Entity where T : IPlayerInfo, new()
    {
        public InputHandler InputHandler;
        private T info { get; }
        public Player():base()
        {
            info = new T();
            status = info.getPlayerStatus();
        }

        protected override void update(float deltaTime) {}
    }
}