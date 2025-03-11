using System;
using GameFrontEnd.Effects;
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
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public override void update(float deltaTime)
        {
            cooldown -= deltaTime;
            if (followUp)
            {
                followUpTimer -= deltaTime;
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
                Debug.Log(followUpEndTimer);
                followUpEndTimer -= deltaTime;
                if (followUpEndTimer <= 0)
                {
                    followUpEnd = false;
                    followUpEndTimer = 0.9f;
                    if (player is PlayerObject playerObject)
                    {
                        playerObject.controlable = true;
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