using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameFrontEnd.StoryScript
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
    public class DialogueActionManager : MonoBehaviour
    {
        

        public string storyFilePath
        {
            set
            {
                foreach (StoryUnit unit in new Story(value, this).units)
                {
                    this.name1.Add(unit.name);
                    this.talk1.Add(unit.dialogue);
                    this.functions.Add(unit.funcs);
                }
            }
        }


        private int text_index;
        private IEnumerator coroutine;
        private List<string> name1 = new();

        private List<string> talk1 = new();

        private TMP_Text textfield_name;
        private TMP_Text textfield_text;
    
        private List<Character> characters;
        public Character MainCharacter;
        public Character Luna;
        public Character Luna_fire;
        public Character Astra;
        public Character Helios;

        public List<List<FunctionCall>> functions = new();

        private bool motionEnd()
        {
            foreach (var character in characters)
            {
                if (character.coroutines.Count!=0) return false;
            }
            return true;
        }

        public Vector2 canvasSize => new(Screen.width, Screen.height);

        private void Awake()
        {
            this.finished = false;
            
            characters = new List<Character>()
            {
                MainCharacter,
                Luna,
                Luna_fire,
                Astra,
                Helios,
            };
        
            text_index = 0;
            textfield_name = GameObject.Find("CharName").GetComponent<TMP_Text>();
            textfield_text = GameObject.Find("dialogue").GetComponent<TMP_Text>();
        }
        
        public bool finished { get; set; }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(Luna.coroutines.Count);
            }

            if (Input.GetMouseButtonDown(0) && motionEnd())
            {
                if (text_index >= name1.Count)
                {
                    this.finished = true;
                    return;
                }
                if (!Dialogue.isTalking)
                {
                    foreach (var func in functions[text_index])
                    {
                        func.Invoke();
                    }

                    coroutine = Dialogue.Typing(textfield_name, textfield_text, name1[text_index], talk1[text_index],
                        0.05f);
                    StartCoroutine(coroutine);
                    text_index++;
                }
                else if (Dialogue.isTalking)
                {
                    StopCoroutine(coroutine);
                    StartCoroutine(Dialogue.Typing_All(textfield_name, textfield_text, name1[text_index - 1],
                        talk1[text_index - 1]));

                    // Luna.setFinal();
                }
            }
        }
    }
}