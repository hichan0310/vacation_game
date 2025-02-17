using System;
using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Status;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend.Objects
{
    public class PlayerObject:Player<TestPlayerInfo1>
    {
        private static readonly int Atk = Animator.StringToHash("atk");
        
        private float cooltime_gumgi = 0.5f;
        private bool direction = true;

        public GameObject motionHelper1;
        public GameObject motionHelper2;
        public GameObject chamgyuck1;
        public GameObject chamgyuck2;
        
        public ISkill normalSkill{get;private set;}

        public void Start()
        {
            normalSkill = new TestSkill();
            List<GameObject> objs = new List<GameObject>();
            objs.Add(motionHelper1);
            objs.Add(motionHelper2);
            objs.Add(chamgyuck1);
            objs.Add(chamgyuck2);
            normalSkill.requireObjects(objs);
            normalSkill.registrarTarget(this);
            normalSkill.execute();
        }

        protected override void update(float deltaTime)
        {
            foreach (var listener in this.eventListener)
            {
                listener.update(deltaTime);
            }
            
            cooltime_gumgi += deltaTime;
            animator.SetBool(Atk, false);
            if (cooltime_gumgi >= 2)
            {
                NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
                this.eventActive(evnt);
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