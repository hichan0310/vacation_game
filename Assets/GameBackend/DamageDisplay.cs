using System;
using System.Collections;
using System.Collections.Generic;
using GameBackend;
using GameBackend.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DamageDisplay : MonoBehaviour
{
    private float moveSpeed=1; // 텍스트 이동속도
    private float alphaSpeed=2; // 투명도 변환속도
    private float destroyTime=2;
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
            if (value.atkTags.Contains(AtkTags.criticalHit)) text.fontSize = 10; 
            else text.fontSize = 6;
            text.color=Color.black;
        }
    }

    private void Awake()
    {
        Invoke("destroy", destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        var col = text.color;
        col.a=Mathf.Lerp(col.a, 0, Time.deltaTime * alphaSpeed);
        text.color = col;
    }

    void destroy()
    {
        Destroy(gameObject);
    }
}