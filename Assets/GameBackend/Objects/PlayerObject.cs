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
        private float movePower = 1f;
        private float jumpPower = 2.5f;
        private bool isJumping = false;
        private Rigidbody2D rigid;

        public GameObject motionHelper1;
        public GameObject motionHelper2;
        public GameObject chamgyuck1;
        public GameObject chamgyuck2;
        
        public ISkill normalSkill{get;private set;}

        public void Start()
        {
            rigid = this.gameObject.GetComponent<Rigidbody2D>();
            this.status = new PlayerStatus(10000, 1000, 100);
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
            base.update(deltaTime);
            Move();
            Jump();
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
                NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
                this.eventActive(evnt);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Plate")
            {
                isJumping = false;
            }
        }
        
        public void balsa()
        {
            GameObject obj = Instantiate(gumgi, this.transform); 
            Gumgi gumgiCompo = obj.GetComponent<Gumgi>();
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

        
        void Move()
        {
            Vector3 moveVelocity= Vector3.zero;
            if(Input.GetKey(InputHandler.MoveLeft))
            {
                moveVelocity = Vector3.left;
            }
            else if(Input.GetKey(InputHandler.MoveRight))
            {
                moveVelocity = Vector3.right;
            }
            this.transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        void Jump()
        {
            if(Input.GetKey(InputHandler.Jump))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    rigid.velocity = Vector2.zero;

                    Vector2 jumpVelocity = new Vector2 (0, jumpPower);
                    rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);
                }
	        }
        }
    }
}