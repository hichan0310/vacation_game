using GameBackend.Events;
using UnityEngine;

namespace GameBackend.Artifects
{
    public class FireStone:Artifect
    {
        public GameObject gumgi;
        
        public override void eventActive<T>(T eventArgs)
        {
            if (eventArgs is NormalAttackExecuteEvent normalAttackExecuteEvent)
            {
                var transform = normalAttackExecuteEvent.attacker.transform;
                
            }
        }
    }
}