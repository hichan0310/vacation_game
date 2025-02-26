using GameBackend.Events;
using GameBackend.Objects;
using UnityEngine;

namespace GameBackend.Artifects
{
    public class ElectricStone:Artifect
    {
        public ElectricSpark ElectricSpark;
        public override void eventActive<T>(T eventArgs)
        {
            if (eventArgs is NormalAttackExecuteEvent)
            for (int i = 0; i < 6; i++)
            {
                var obj = Instantiate(ElectricSpark);
                obj.angle = Mathf.PI / 3 * i;
                obj.transform.position =
                    this.player.transform.position + new Vector3(Mathf.Cos(obj.angle), Mathf.Sin(obj.angle), 0)/2;
                
            }
        }
    }
}