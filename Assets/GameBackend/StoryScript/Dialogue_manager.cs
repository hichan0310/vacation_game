using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue_manager : MonoBehaviour
{
    private int text_index;
    public static string[] name1 = {"아로나", "아로나", "아로나"};
    public static string[] talk1 = {"좋은 아침이에요, 선생님!", "요 며칠 샬레에 대한 소문도 많이 퍼져나간것 같고, 다른 학생들의 도움 요청 편지도 도착한 것도 있어요.", "좋은 신호에요! 저희의 활약이 시작될 거란 얘기니까요!"}; 
    public static float[] speed1 = {0.05f, 0.03f, 0.05f};
    private TMP_Text textfield_name;
    private TMP_Text textfield_text;

    private void Awake()
    {
        text_index = 0;
        textfield_name = GameObject.Find("CharName").GetComponent<TMP_Text>();
        textfield_text = GameObject.Find("dialogue").GetComponent<TMP_Text>();
    }

    public void Update()
    {
        if(Input.GetMouseButton(0) && !Dialogue.isTalking && text_index < 3)
        {
            StartCoroutine(Dialogue.Typing(textfield_name, textfield_text, name1[text_index], talk1[text_index], speed1[text_index]));
            text_index++;
        }
    }
}
