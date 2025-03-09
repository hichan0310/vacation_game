using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;

namespace GameBackend.StoryScript
{
    public class TestStory1:Story
    {
        public string filePath = "Assets/GameBackend/CSV/dialogue.csv";
        private List<string> name_text = new();
        private List<string> dialogue_text = new();
        private List<string> firstaction = new();
        private List<string> secondaction = new();
        private List<float> firstaction_firstarg = new();
        private List<float> firstaction_secondarg = new();
        private List<float> secondaction_firstarg = new();
        private List<float> secondaction_secondarg = new();
        public TestStory1()
        {

            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("euc-kr")))
            {
                string headerLine = sr.ReadLine();
                
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    var matches = Regex.Matches(line, @"(?<=^|,)(\"".*?\"")|([^,]+)(?=,|$)");
                    List<string> values = new List<string>();

                    foreach (Match match in matches)
                    {
                        string value = match.Value.Trim('"');
                        values.Add(value);
                    }

                    Debug.Log($"{values[0]} + {values[1]} + {values[2]} + {values[3]} + {values[4]} + {values[5]}");
                    name_text.Add(values[0]);
                    dialogue_text.Add(values[1]);
                    firstaction.Add(values[2]);
                    firstaction_firstarg.Add(float.Parse(values[3]));
                    firstaction_secondarg.Add(float.Parse(values[4]));
                    secondaction.Add(values[5]);
                    secondaction_firstarg.Add(float.Parse(values[6]));
                    secondaction_secondarg.Add(float.Parse(values[7]));
                }
            }

            object GetObjectByName(string objectName)
            {
                Type dialogueManagerType = typeof(DialogueManager);
                object dialogueManagerInstance = dialogueManagerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
                FieldInfo fieldInfo = dialogueManagerType.GetField(objectName, BindingFlags.Public | BindingFlags.Instance);
                return fieldInfo.GetValue(dialogueManagerInstance);
            }

            for(int i = 0; i < 9; i++)
            {
                string objectName1 = firstaction[i].Split(".")[0];
                string methodName1 = firstaction[i].Split(".")[1];
                string objectName2 = secondaction[i].Split(".")[0];
                string methodName2 = secondaction[i].Split(".")[1];
                object targetObject1 = GetObjectByName(objectName1);
                object targetObject2 = GetObjectByName(objectName2);
                Type type1 = targetObject1.GetType();
                Type type2 = targetObject2.GetType();
                MethodInfo method1 = type1.GetMethod(methodName1);
                MethodInfo method2 = type2.GetMethod(methodName2);
                this.units.Add(new StoryUnit(
                    name_text[i],
                    dialogue_text[i],
                    new List<FunctionCall>
                    {
                        new(Delegate.CreateDelegate(typeof(Action<float, float>), targetObject1, method1), firstaction_firstarg[i], firstaction_secondarg[i]),
                        new(Delegate.CreateDelegate(typeof(Action<float, float>), targetObject2, method2), secondaction_firstarg[i], secondaction_secondarg[i])
                    }
                ));
            }

            // this.units.Add(new StoryUnit(
            //     "나",
            //     "안녕, 루나",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(MainCharacter.appear_right_move)),
            //         new(new Action<float, float>(Luna.appear_left_move))
            //     }
            // ));
            
            // this.units.Add(new StoryUnit(
            //     "루나",
            //     "오늘도 이 꽃을 보러 왔구나.",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(Luna.little_jump)),
            //         new(new Action<float, float>(Luna.move_x), 1f, 0.1f)
            //     }
            // ));
            
            // this.units.Add(new StoryUnit(
            //     "나",
            //     "이 꽃을 보고 있으면 마음이 편안해져. 루나는 뭐하러 나온거야?",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(MainCharacter.little_jump)),
            //         new(new Action<float, float>(Luna.none_move))
            //     }
            // ));

            // this.units.Add(new StoryUnit(
            //     "루나",
            //     "산책하러 나왔어. 오늘은 바람도 선선하고, 하늘도 맑고, 산책하기 딱 좋은 날씨거든.",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(Luna.fast_jump), 0f),
            //         new (new Action<float, float>(Luna.fast_jump), 0.5f)
            //     }
            // ));

            // this.units.Add(new StoryUnit(
            //     "나",
            //     "정말 좋은 날씨네. 계속 방에만 있느라 눈치채지 못했어.",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(MainCharacter.little_jump)),
            //         new(new Action<float, float>(Luna.none_move))
            //     }
            // ));

            // this.units.Add(new StoryUnit(
            //     "루나",
            //     "아직도 코드가 제대로 작동하지 않는거야?",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(Luna.dori_dori)),
            //         new(new Action<float, float>(Luna.none_move))
            //     }
            // ));

            // this.units.Add(new StoryUnit(
            //     "나",
            //     "coroutine 작동 방식이 너무 어려워서 어떻게 해결해야 할지 모르겠어.",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(MainCharacter.little_jump)),
            //         new(new Action<float, float>(Luna.none_move))
            //     }
            // ));

            // this.units.Add(new StoryUnit(
            //     "루나",
            //     "나랑 같이 잠깐 산책하러 가자. 맑은 공기를 마시면 도움이 될거야!",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(Luna.fast_jump), 0f),
            //         new(new Action<float, float>(Luna.fast_jump), 0.5f)
            //     }
            // ));
            
            // this.units.Add(new StoryUnit(
            //     " ",
            //     " ",
            //     new List<FunctionCall>
            //     {
            //         new(new Action<float, float>(MainCharacter.disappear_right_move)),
            //         new(new Action<float, float>(Luna.disappear_left_move))
            //     }
            // ));
        }
    }
}