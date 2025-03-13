using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace GameFrontEnd.StoryScript
{
    public class TestStory1:Story
    {
        public string filePath = "Assets/GameBackend/CSV/dialogue.csv";
        private string name;
        private int dialogue_num;
        StringBuilder dialogue_textlist = new StringBuilder();
        private string dialogue_text;
        private int action_index;
        private int action_num;
        private int actionparam_num;
        public TestStory1()
        {
            
            object GetObjectByName(string objectName)
            {
                Type dialogueManagerType = typeof(DialogueActionManager);
                object dialogueManagerInstance = dialogueManagerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
                FieldInfo fieldInfo = dialogueManagerType.GetField(objectName, BindingFlags.Public | BindingFlags.Instance);
                return fieldInfo.GetValue(dialogueManagerInstance);
            }

            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("euc-kr")))
            {
                string headerLine = sr.ReadLine();
                
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = new string[0];
                    values = line.Split(',');
                    name = (values[0] == "주인공") ? (PlayerPrefs.HasKey("hero") ? PlayerPrefs.GetString("hero") : "임시") : values[0];
                    dialogue_num = int.Parse(values[1]);
                    dialogue_textlist = new StringBuilder();
                    for (int i = 0; i < dialogue_num; i++)
                    {
                        dialogue_textlist.Append(values[i + 2].Replace("|||", "\"").Replace("||", "'").Replace("|", ","));
                        if (i < dialogue_num - 1)
                        {
                            dialogue_textlist.Append("\n");
                        }
                    }

                    dialogue_text = dialogue_textlist.ToString();
                    action_index = 2 + dialogue_num;
                    action_num = int.Parse(values[action_index]);
                    List<FunctionCall> functionCalls = new List<FunctionCall>();

                    for(int i = 0; i < action_num; i++)
                    {
                        string objectName = values[action_index + 1].Split(".")[0];
                        string methodName = values[action_index + 1].Split(".")[1];
                        object targetObject = GetObjectByName(objectName);
                        actionparam_num = int.Parse(values[action_index + 2]);
                        Type type = targetObject.GetType();
                        MethodInfo method = type.GetMethod(methodName);
                        List<float> paramList = new List<float>();
                        for (int j = 0; j < actionparam_num; j++)
                        {
                            paramList.Add(float.Parse(values[action_index + 3 + j]));
                        }
                        float[] param = paramList.ToArray();
                        functionCalls.Add(new FunctionCall(
                            Delegate.CreateDelegate(typeof(Action<float[]>), targetObject, method),
                            param
                        ));
                        action_index = action_index + actionparam_num + 2;
                    }

                    this.units.Add(new StoryUnit(
                        name,
                        dialogue_text,
                        functionCalls
                    ));
                }
            }
        }
    }
}