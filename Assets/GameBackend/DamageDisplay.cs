using System;
using System.Collections;
using System.Collections.Generic;
using GameBackend;
using GameBackend.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            text=GetComponent<TextMeshPro>();
            transform.position=value.target.transform.position;
            text.text = value.realDmg.ToString();
            if (value.atkTags.Contains(AtkTags.criticalHit)) text.fontSize = 20; 
            else text.fontSize = 10;
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