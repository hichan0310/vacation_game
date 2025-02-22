using System.Collections.Generic;
using GameBackend.Status;
using UnityEngine;

namespace GameBackend
{
    public class Player<T> : Entity where T : IPlayerInfo, new()
    {
        private T info { get; }
        public Player():base()
        {
            info = new T();
            status = info.getPlayerStatus();
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            
        }

        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
        }

        public override void Update()
        {
            update(Time.deltaTime);
        }
    }
}