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
        private static readonly int Moving = Animator.StringToHash("moving");
        private int atknum = 0;

        private float cooltime_gumgi = 0.5f;
        private float cooltime_click = 0f;
        private bool direction = true;
        private float movePower = 1f;
        private float jumpPower = 2f;
        private bool isJumping = false;
        private bool isJumpatk = false;
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
            // normalSkill.execute();
        }

        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
            animator.SetBool("jumping", isJumping);
            animator.SetInteger("atknum", atknum);
            Move(deltaTime);
            Jump(deltaTime);
            // cooltime_gumgi += deltaTime;
            // animator.SetTrigger(Atk);
            // if (cooltime_gumgi >= 2)
            // {
            //     
            //     animator.SetTrigger(Atk);
            //     cooltime_gumgi = 0;
            //     direction = !direction;
            //     Invoke("balsa", 0.4f);
            //     NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
            //     this.eventActive(evnt);
            // } 
            if(!isJumping) cooltime_click += deltaTime;
            if(cooltime_click > 1f)
            {
                atknum = 0;
                cooltime_click = 0.25f;
            }
            else if(Input.GetMouseButtonDown(0))
            {

                if (!isJumping)
                {
                    if (cooltime_click >= 0.25f)
                    {
                    animator.SetTrigger("atk");
                    NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
                    this.eventActive(evnt);
                    atknum = (atknum < 3) ? atknum + 1 : 0;
                    cooltime_click = 0;
                    }
                }
                else if (!isJumpatk)
                {
                    atknum = 0;
                    cooltime_click = 0;
                    animator.SetTrigger("jumpatk");
                    isJumpatk = true;
                }
            }

        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Plate")
            {
                isJumping = false;
                isJumpatk = false;
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

        
        void Move(float deltaTime)
        {
            Vector3 moveVelocity= Vector3.zero;
            if(Input.GetKey(InputHandler.MoveLeft))
            {
                if (direction == true)
                {
                    direction = false;
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                animator.SetBool(Moving, true);
                moveVelocity = Vector3.left;
            }
            else if(Input.GetKey(InputHandler.MoveRight))
            {
                if (direction == false)
                {
                    direction = true;
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                animator.SetBool(Moving, true);
                moveVelocity = Vector3.right;
            }
            animator.SetBool(Moving, false);
            this.transform.position += moveVelocity * (movePower * deltaTime);
        }
        void Jump(float deltaTime)
        {
            if(Input.GetKey(InputHandler.Jump))
            {
                if (!isJumping)
                {
                    animator.SetTrigger("jump");
                    isJumping = true;
                    rigid.velocity = Vector2.zero;

                    Vector2 jumpVelocity = new Vector2 (0, jumpPower);
                    rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);
                }
	        }
        }
    }
}