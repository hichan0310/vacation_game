using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace GameFrontEnd.StoryScript
{
    public class StoryUnit
    {
        public string name { get; }
        public string dialogue { get; }
        public List<FunctionCall> funcs { get; }

        public StoryUnit(string name, string dialogue, List<FunctionCall> funcs)
        {
            this.name = name;
            this.dialogue = dialogue;
            this.funcs = funcs;
        }
    }
    
    public class Story
    {
        public static Character MainCharacter{get;set;}
        public static Character Luna{get;set;}
        public static Character Luna_fire{get;set;}
        public static Character Astra{get;set;}
        public static Character Helios{get;set;}

        private string name;
        private int dialogue_num;
        StringBuilder dialogue_textlist = new StringBuilder();
        private string dialogue_text;
        private int action_index;
        private int action_num;
        private int actionparam_num;
        private string[] values;
        public Story(string filePath, DialogueActionManager manager)
        {
            MainCharacter = manager.MainCharacter;
            Luna = manager.Luna;
            Luna_fire = manager.Luna_fire;
            Astra = manager.Astra;
            Helios = manager.Helios;
            
            MainCharacter.setSize(manager.canvasSize);
            Luna.setSize(manager.canvasSize);
            Luna_fire.setSize(manager.canvasSize);
            Astra.setSize(manager.canvasSize);
            Helios.setSize(manager.canvasSize);
            
            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("euc-kr")))
            {
                string headerLine = sr.ReadLine();
                
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
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
                        object targetObject = GetPropertyValueByName($"{objectName}");
                        actionparam_num = int.Parse(values[action_index + 2]);
                        Type type2 = targetObject.GetType();
                        MethodInfo method = type2.GetMethod(methodName);
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

        private object GetPropertyValueByName(string propertyName)
        {
            Type storyType = typeof(Story);
            PropertyInfo propertyInfo = storyType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);
            return propertyInfo.GetValue(null);  
        }
        
        public List<StoryUnit> units { get; private set; } = new List<StoryUnit>();
    }
}