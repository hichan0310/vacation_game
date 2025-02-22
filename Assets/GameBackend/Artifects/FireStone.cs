using GameBackend.Events;
using GameBackend.Objects;
using GameBackend.Status;
using UnityEngine;

namespace GameBackend.Artifects
{
    public class FireStone:Artifect
    {
        public Gumgi gumgi;
        
        public override void eventActive<T>(T eventArgs)
        {
            if (eventArgs is NormalAttackExecuteEvent normalAttackExecuteEvent)
            {
                var transform = normalAttackExecuteEvent.attacker.transform;
                Gumgi gumg = Instantiate(gumgi, transform);
            }
        }
    }
}