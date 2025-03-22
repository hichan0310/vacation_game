using GameBackend.Events;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameBackend
{
    public class DamageDisplay : MonoBehaviour
    {
        private float moveSpeed=1; // 텍스트 이동속도
        private float timer = 0;
        private float destroyTime=0.8f;
        private TextMeshPro text;

        public DmgTakeEvent dmgEvent
        {
            set
            {
                float x = Random.Range(-0.3f, 0.3f); // X 좌표: -1 ~ 1
                float y = Random.Range(-0.3f, 0.3f); // Y 좌표: -1 ~ 1
                text=GetComponent<TextMeshPro>();
                transform.position=value.target.transform.position+new Vector3(x, y, 0f);
                text.text = value.realDmg.ToString();
                if (value.atkTags.Contains(AtkTags.criticalHit)) text.fontSize = 6; 
                else if(value.atkTags.Contains(AtkTags.statusEffect)) text.fontSize = 2;
                else text.fontSize = 4;
                if (value.atkTags.Contains(AtkTags.physicalAttack)) text.color=Color.black;
                else if (value.atkTags.Contains(AtkTags.fireAttack)) text.color=Color.red;
                else if (value.atkTags.Contains(AtkTags.waterAttack)) text.color=Color.blue;
                else if (value.atkTags.Contains(AtkTags.lightningAttack)) text.color=Color.yellow;
                else if (value.atkTags.Contains(AtkTags.shadowAttack)) text.color=Color.magenta;
                else if (value.atkTags.Contains(AtkTags.iceAttack)) text.color=Color.cyan;
                else if (value.atkTags.Contains(AtkTags.windAttack)) text.color=Color.green;
                else text.color=Color.white;
            }
        }

        private void Awake()
        {
            Invoke("destroy", destroyTime);
        }

        void Update()
        {
            timer += Time.deltaTime;
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
            if (timer >= 0.4f)
            {
                var col = text.color;
                col.a = Mathf.Lerp(1, 0, (timer-0.5f)/(destroyTime-0.5f));
                text.color = col;
            }
        }

        void destroy()
        {
            Destroy(gameObject);
        }
    }
}