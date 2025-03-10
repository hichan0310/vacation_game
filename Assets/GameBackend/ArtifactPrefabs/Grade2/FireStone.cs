using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Objects;
using GameFrontEnd.Effects.ArtifactEffect.FireStone;

namespace GameBackend.ArtifactPrefabs.Grade2
{
    public class FireStone:Artifact
    {
        public Gumgi gumgi;
        private float cooldown = 0;
        private float coolTime = 6;
        
        public override int grade => 2;

        private void Start()
        {
            this.name = "화염의 돌";
            this.description = "주기적으로 6초마다 일반 공격 시 화염 검기를 날리고 명중한 적은 방어력 4초동안 50% 감소시킨다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            if (cooldown == 0)
            {
                if (eventArgs is NormalAttackExecuteEvent normalAttackExecuteEvent)
                {
                    cooldown = coolTime;
                    var transform = normalAttackExecuteEvent.attacker.transform;
                    Gumgi gumg = Instantiate(gumgi);
                    gumg.direction = player.transform.localScale.x > 0;
                    gumg.position = player.transform.position;
                    gumg.time = 0.3f;
                    gumg.gumgiSpeed = 6;
                    if (!gumg.direction)
                    {
                        var scale = gumg.transform.localScale;
                        scale.x *= -1;
                        gumg.transform.localScale = scale;
                    }

                    List<AtkTags> tag = new List<AtkTags>
                    {
                        AtkTags.fireAttack,
                        AtkTags.artifactDamage
                    };
                    gumg.dmgInfo = new DmgInfo(player.status.calculateTrueDamage(tag, atkCoef: 30), 0.1f, player, tag);
                    gumg.apply();
                }
            }
        }

        public override void update(float deltaTime)
        {
            cooldown -= deltaTime;
            if (cooldown < 0)
            {
                cooldown = 0;
            } 
        }
    }
}