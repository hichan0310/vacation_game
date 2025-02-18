﻿using System.Collections.Generic;
using GameBackend.Status;
using UnityEngine;

namespace GameBackend
{
    public class Player<T> : Entity where T : IPlayerInfo, new()
    {
        public GameObject gumgi;
        private T info { get; }
        public Player():base()
        {
            info = new T();
            status = info.getPlayerStatus();
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