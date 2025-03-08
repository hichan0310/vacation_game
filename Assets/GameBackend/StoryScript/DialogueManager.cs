using System;
using System.Collections;
using System.Collections.Generic;
using GameBackend.StoryScript;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public class FunctionCall
    {
        public Delegate Method { get; }
        public object[] Parameters { get; }

        public FunctionCall(Delegate method, params object[] parameters)
        {
            Method = method;
            Parameters = parameters;
        }

        public void Invoke()
        {
            Method.DynamicInvoke(Parameters);
        }
    }
    
    
    private int text_index;
    private IEnumerator coroutine;
    private string[] name1 = {"루나", "나", "루나", "나", "루나", "나", "루나", "나", "루나", " "};
    private string[] talk1 = {
        "좋은 아침이야!", 
        "안녕, 루나.",
        "오늘도 이 꽃을 보러 왔구나.", 
        "이 꽃을 보고 있으면 마음이 편안해져. 루나는 뭐하러 나온거야?",
        "산책하러 나왔어. 오늘은 바람도 선선하고, 하늘도 맑고, 산책하기 딱 좋은 날씨거든.",
        "정말 좋은 날씨네. 계속 방에만 있느라 눈치채지 못했어.",
        "아직도 코드가 제대로 작동하지 않는거야?",
        "coroutine 작동 방식이 너무 어려워서 어떻게 해결해야 할지 모르겠어.",
        "나랑 같이 잠깐 산책하러 가자. 맑은 공기를 마시면 도움이 될거야!",
        " "
    }; 
    
    private TMP_Text textfield_name;
    private TMP_Text textfield_text;
    public Character Luna;

    public List<List<FunctionCall>> functions = new();

    private void Awake()
    {
        text_index = 0;
        textfield_name = GameObject.Find("CharName").GetComponent<TMP_Text>();
        textfield_text = GameObject.Find("dialogue").GetComponent<TMP_Text>();
        
        functions.Add(new List<FunctionCall> {
            //new FunctionCall(new Action(Luna.complete)),
            new FunctionCall(new Action(Luna.appear_left_move))
        });

        functions.Add(new List<FunctionCall> {
            //new FunctionCall(new Action(Luna.complete)),
            new FunctionCall(new Action(Luna.little_jump)),
            new FunctionCall(new Action<float, float>(Luna.move_x), 1f, 0.1f)
        });
        

        functions.Add(new List<FunctionCall> {
            //new FunctionCall(new Action(Luna.complete)),
            new FunctionCall(new Action<float>(Luna.fast_jump), 0f),
            new FunctionCall(new Action<float>(Luna.fast_jump), 0.5f)
        });

        functions.Add(new List<FunctionCall> {
            //new FunctionCall(new Action(Luna.complete)),
            new FunctionCall(new Action(Luna.dori_dori)),
        });

        functions.Add(new List<FunctionCall> {
            //new FunctionCall(new Action(Luna.complete)),
            new FunctionCall(new Action<float>(Luna.fast_jump), 0f),
            new FunctionCall(new Action<float>(Luna.fast_jump), 0.5f)
        });

        functions.Add(new List<FunctionCall> {
            //new FunctionCall(new Action(Luna.complete)),
            new FunctionCall(new Action(Luna.disappear_left_move))
        });

    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Luna.coroutines.Count);
        }
        if(Input.GetMouseButtonDown(0) && Luna.coroutines.Count == 0)
        {
            if(!Dialogue.isTalking && text_index < 10)
            {

                if (text_index == 9)
                {
                    foreach (var func in functions[5])
                    {
                        func.Invoke();
                    }
                }
                else if (name1[text_index] == "루나")
                {
                    foreach (var func in functions[text_index / 2])
                    {
                        func.Invoke();
                    }
                }
                
                coroutine = Dialogue.Typing(textfield_name, textfield_text, name1[text_index], talk1[text_index], 0.05f);
                StartCoroutine(coroutine);
                text_index++;
            }
            else if(Dialogue.isTalking && name1[text_index - 1] != "나")
            {
                StopCoroutine(coroutine);
                StartCoroutine(Dialogue.Typing_All(textfield_name, textfield_text, name1[text_index - 1], talk1[text_index - 1]));

                // Luna.setFinal();
            }
        }

    }
}
