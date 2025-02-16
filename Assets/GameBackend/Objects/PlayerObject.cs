using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Status;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend.Objects
{
    public class PlayerObject:Player<TestPlayerInfo>
    {
        private static readonly int Atk = Animator.StringToHash("atk");
        
        private float cooltime_gumgi = 0.5f;
        private bool direction = true;
        protected override void update(float deltaTime)
        {
            cooltime_gumgi += deltaTime;
            animator.SetBool(Atk, false);
            if (cooltime_gumgi >= 2)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                animator.SetBool(Atk, true);
                cooltime_gumgi = 0;
                direction = !direction;
                Invoke("balsa", 0.4f);
            }
        }
        
        public void balsa()
        {
            GameObject obj = Instantiate(gumgi); 
            Gumgi gumgiCompo=obj.GetComponent<Gumgi>();
            gumgiCompo.direction = direction;
            gumgiCompo.position=transform.position;
            gumgiCompo.time = 0.4f;
            gumgiCompo.speed = 6;
            List<AtkTags> atkTag = new List<AtkTags>();
            atkTag.Add(AtkTags.fireAttack);
            atkTag.Add(AtkTags.normalAttack);
            gumgiCompo.dmgInfo = new DmgInfo(100, 10, this, atkTag);
            gumgiCompo.apply();
        }
    }
}