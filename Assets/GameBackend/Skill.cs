using System.Collections;
using System.Collections.Generic;
using GameBackend.Buffs;
using GameBackend.Events;
using GameBackend.Objects;
using GameBackend.Status;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend
{
    public interface ISkill : IEntityEventListener
    {
        public string name { get; }
        public string description { get; }
        public bool active { get; }
        public float timeleft { get; }
        public void execute();
        public void requireObjects(List<GameObject> objects);
    }
}