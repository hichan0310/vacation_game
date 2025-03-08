using System;
using System.Collections.Generic;

namespace GameBackend.StoryScript
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
        public Character MainCharacter=DialogueManager.Instance.MainCharacter;
        public Character Luna=DialogueManager.Instance.Luna;
        public Character Luna_fire=DialogueManager.Instance.Luna_fire;
        public Character Astra=DialogueManager.Instance.Astra;
        public Character Helios=DialogueManager.Instance.Helios;

        // protected void addUnit(string name, string dialogue, params FunctionCall[] actions)
        // {
        //     // FunctionCall 리스트 생성
        //     List<FunctionCall> functionCalls = new List<FunctionCall>();
        //
        //     // 넘어온 Action들을 순회하며 FunctionCall 객체로 변환
        //     foreach (var action in actions)
        //     {
        //         // 필요하다면 new Action(action) 형태로 감싸거나
        //         // 바로 action을 넘길 수도 있음
        //         functionCalls.Add(action);
        //     }
        //
        //     // StoryUnit에 추가
        //     this.units.Add(new StoryUnit(
        //         name,
        //         dialogue,
        //         functionCalls
        //     ));
        // }
        
        public List<StoryUnit> units { get; private set; } = new List<StoryUnit>();
    }
}