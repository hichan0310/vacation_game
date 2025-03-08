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
    private string[] name1 = {"아로나", "아로나", "아로나", "아로나"};
    private string[] talk1 = {
        "좋은 아침이에요, 선생님!", 
        "요 며칠 샬레에 대한 소문도 많이 퍼져나간것 같고, 다른 학생들의 도움 요청 편지도 도착한 것도 있어요.", 
        "좋은 신호에요! 저희의 활약이 시작될 거란 얘기니까요!",
        "근데 왜 complete 한수는 작동을 안 하는 건가요?"
    }; 
    private float[] speed1 = {0.05f, 0.03f, 0.05f, 0.05f};
    
    
    
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
            new FunctionCall(new Action(Luna.fall_down))
        });
        
        functions.Add(new List<FunctionCall> {
            //new FunctionCall(new Action(Luna.complete)),
            new FunctionCall(new Action(Luna.appear_right_move)),
        });
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0) && Luna.coroutines.Count == 0)
        {
            if(!Dialogue.isTalking && text_index < 4)
            {
                foreach (var func in functions[text_index])
                {
                    func.Invoke();
                }
                
                coroutine = Dialogue.Typing(textfield_name, textfield_text, name1[text_index], talk1[text_index], speed1[text_index]);
                StartCoroutine(coroutine);
                text_index++;
            }
            else if(Dialogue.isTalking)
            {
                StopCoroutine(coroutine);
                StartCoroutine(Dialogue.Typing_All(textfield_name, textfield_text, name1[text_index - 1], talk1[text_index - 1]));

                // Luna.setFinal();
            }
        }

    }
}
