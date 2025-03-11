using System;
using GameFrontEnd.Effects;
using GameFrontEnd.Effects.SkillEffects.WaterThrust;
using GameFrontEnd.Objects;
using UnityEngine;

namespace GameBackend.Skills.NormalSkill
{
    public class WaterThrust:NormalSkill
    {
        float cooldown=1;
        bool followUp=false;
        float followUpTimer=0.7f;
        bool followUpEnd=false;
        float followUpEndTimer=0.9f;
        bool waterSpreaded=false;

        public WaterThrustWater water;
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }
        
        bool IsCollidingWithEnemy(Collider2D polygonCollider)
        {
            if (!polygonCollider) return false;

            // 레이어 마스크 (Enemy 레이어만 체크)
            int enemyLayerMask = LayerMask.GetMask("Enemy");

            // 충돌 검사
            Collider2D[] results = new Collider2D[10]; // 최대 10개 충돌 검사
            int hitCount = polygonCollider.OverlapCollider(new ContactFilter2D { layerMask = enemyLayerMask, useLayerMask = true }, results);

            return hitCount > 0;
        }

        public override void update(float deltaTime)
        {
            cooldown -= deltaTime;
            if (followUp)
            {
                followUpTimer -= deltaTime;
                if (!IsCollidingWithEnemy(player.GetComponent<PolygonCollider2D>()))
                {
                    var vector3 = player.transform.position;
                    vector3.x += (followUpTimer+0.1f)*player.status.movePower*deltaTime*((player.transform.localScale.x>0)?5.5f:-5.5f);
                    player.transform.position = vector3;
                }
                if (followUpTimer <= 0)
                {
                    followUp = false;
                    followUpTimer = 0.7f;
                    if (player is PlayerObject playerObject)
                    {
                        playerObject.attackable = true;
                        playerObject.jumpable=true;
                        playerObject.moveable = true;
                    }
                }
            }

            if (followUpEnd)
            {
                followUpEndTimer -= deltaTime;
                if (followUpEndTimer <= 0)
                {
                    followUpEnd = false;
                    followUpEndTimer = 0.9f;
                    waterSpreaded=false;
                    if (player is PlayerObject playerObject)
                    {
                        playerObject.controlable = true;
                    }
                }
                
                if (!waterSpreaded && followUpEndTimer <= 0.7f)
                {
                    waterSpreaded = true;
                    for (int i = 0; i < 7; i++)
                    {
                        WaterThrustWater wat = Instantiate(water);
                        wat.transform.position = player.transform.position-new Vector3(0f,-0.3f,0f);
                        wat.velocity = new Vector3(Mathf.Cos(Mathf.PI * (1 / 6 + i / 36)),
                            Mathf.Sin(Mathf.PI * (1 / 6 + i / 36)), 0);
                    }
                }
            }
            if (timeleft < 0) timeleft = 0;
        }

        public override string skillName { get; }
        public override string description { get; }

        public override bool active
        {
            get
            {
                if (followUp)
                {
                    return true;
                }
                if (cooldown > 0) return false;
                if (player is PlayerObject playerObject)
                {
                    return !playerObject.isJumping;
                }
                else
                {
                    return false;
                }
            }
        }

        public override float timeleft { get; set; }
        public override void execute()
        {
            if (!followUp)
            {
                followUp = true;
                cooldown = 1;
                player.animator.SetTrigger("waterThrust");
                if (player is PlayerObject playerObject)
                {
                    playerObject.attackable = false;
                    playerObject.jumpable=false;
                    playerObject.moveable = false;
                }
            }
            else
            {
                followUp = false;
                followUpTimer = 0.7f;
                player.animator.SetTrigger("waterThrustFollowUp");
                if (player is PlayerObject playerObject)
                {
                    playerObject.attackable = true;
                    playerObject.jumpable=true;
                    playerObject.moveable = true;
                    playerObject.controlable = false;
                    followUpEnd = true;
                }
            }
        }
    }
}