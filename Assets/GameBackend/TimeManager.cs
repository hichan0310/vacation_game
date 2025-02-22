using System.Collections.Generic;
using UnityEngine;

namespace GameBackend
{
    public static class TimeManager
    {
        private static float _timeRate=1;
        private static List<Entity> entities = new();
        public static float timeRate
        {
            get => _timeRate;
            set
            {
                _timeRate = value;
                foreach (Entity entity in entities)
                {
                    entity.animator.speed = _timeRate*entity.speed;
                }
            }
        }

        public static void registrarEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public static void removerEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public static float deltaTime
        {
            get { return Time.deltaTime * timeRate; }
        }
    }
}