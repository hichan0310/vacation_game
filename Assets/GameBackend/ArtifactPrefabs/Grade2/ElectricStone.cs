using GameBackend.Events;
using GameBackend.Objects;
using GameFrontEnd.Effects.ArtifactEffect.StoneOfLightning;
using UnityEngine;

namespace GameBackend.ArtifactPrefabs.Grade2
{
    public class ElectricStone:Artifact
    {
        public ElectricSpark ElectricSpark;

        private float timer = 0;

        public override int grade => 2;

        private void Start()
        {
            this.name = "번개의 돌";
            this.description = "일반 공격 시 7초마다 주기적으로 적에게 유도되는 전기볼을 발사한다. 공격력의 100% 번개 피해를 입힌다. ";
        }


        public override void eventActive<T>(T eventArgs)
        {
            if (eventArgs is NormalAttackExecuteEvent && timer == 0)
            {
                timer = 7;
                for (int i = 0; i < 6; i++)
                {
                    var obj = Instantiate(ElectricSpark);
                    obj.angle = Mathf.PI / 3 * i;
                    obj.transform.position =
                        this.player.transform.position + new Vector3(Mathf.Cos(obj.angle), Mathf.Sin(obj.angle), 0) / 2;
                    obj.time = 5;
                }
            }
        }

        public override void update(float deltaTime)
        {
            timer -= deltaTime;
            if(timer<0) timer = 0;
        }
    }
}