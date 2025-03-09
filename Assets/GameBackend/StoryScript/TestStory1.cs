using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace GameBackend.StoryScript
{
    public class TestStory1:Story
    {
        private List<string> list1 = new() {"나", "루나"};
        private List<string> list2 = new() {"안녕, 루나", "오늘도 이 꽃을 보러 왔구나."};
        private List<string> list3 = new() {"MainCharacter.appear_right_move", "Luna.little_jump"};
        private List<string> list4 = new() {"Luna.appear_left_move", "Luna.move_x"};
        private List<float> list3_1 = new() {0, 0};
        private List<float> list3_2 = new() {0, 0};
        private List<float> list4_1 = new() {0, 1};
        private List<float> list4_2 = new() {0, 0.1f};
        public TestStory1()
        {
            object GetObjectByName(string objectName)
            {
                Type dialogueManagerType = typeof(DialogueManager);
                object dialogueManagerInstance = dialogueManagerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
                FieldInfo fieldInfo = dialogueManagerType.GetField(objectName, BindingFlags.Public | BindingFlags.Instance);
                return fieldInfo.GetValue(dialogueManagerInstance);
            }

            for(int i = 0; i < 2; i++)
            {
                string objectName1 = list3[i].Split(".")[0];
                string methodName1 = list3[i].Split(".")[1];
                string objectName2 = list4[i].Split(".")[0];
                string methodName2 = list4[i].Split(".")[1];
                object targetObject1 = GetObjectByName(objectName1);
                object targetObject2 = GetObjectByName(objectName2);
                Type type1 = targetObject1.GetType();
                Type type2 = targetObject2.GetType();
                MethodInfo method1 = type1.GetMethod(methodName1);
                MethodInfo method2 = type2.GetMethod(methodName2);
                this.units.Add(new StoryUnit(
                    list1[i],
                    list2[i],
                    new List<FunctionCall>
                    {
                        new(Delegate.CreateDelegate(typeof(Action<float, float>), targetObject1, method1), list3_1[i], list3_2[i]),
                        new(Delegate.CreateDelegate(typeof(Action<float, float>), targetObject2, method2), list4_1[i], list4_2[i])
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