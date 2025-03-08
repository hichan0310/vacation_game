using System;
using System.Collections.Generic;

namespace GameBackend.StoryScript
{
    public class TestStory1:Story
    {
        public TestStory1()
        {
            this.units.Add(new StoryUnit(
                "나",
                "안녕, 루나",
                new List<FunctionCall>
                {
                new(new Action(MainCharacter.appear_right_move)),
                new(new Action(Luna.appear_left_move))
                }
            ));
            
            this.units.Add(new StoryUnit(
                "루나",
                "오늘도 이 꽃을 보러 왔구나.",
                new List<FunctionCall>
                {
                    new(new Action(Luna.little_jump)),
                    new(new Action<float, float>(Luna.move_x), 1f, 0.1f)
                }
            ));
            
            this.units.Add(new StoryUnit(
                "나",
                "이 꽃을 보고 있으면 마음이 편안해져. 루나는 뭐하러 나온거야?",
                new List<FunctionCall>
                {
                    new(new Action(MainCharacter.little_jump))
                }
            ));

            this.units.Add(new StoryUnit(
                "루나",
                "산책하러 나왔어. 오늘은 바람도 선선하고, 하늘도 맑고, 산책하기 딱 좋은 날씨거든.",
                new List<FunctionCall>
                {
                    new(new Action<float>(Luna.fast_jump), 0f),
                    new (new Action<float>(Luna.fast_jump), 0.5f)
                }
            ));

            this.units.Add(new StoryUnit(
                "나",
                "정말 좋은 날씨네. 계속 방에만 있느라 눈치채지 못했어.",
                new List<FunctionCall>
                {
                    new(new Action(MainCharacter.little_jump))
                }
            ));

            this.units.Add(new StoryUnit(
                "루나",
                "아직도 코드가 제대로 작동하지 않는거야?",
                new List<FunctionCall>
                {
                    new(new Action(Luna.dori_dori))
                }
            ));

            this.units.Add(new StoryUnit(
                "나",
                "coroutine 작동 방식이 너무 어려워서 어떻게 해결해야 할지 모르겠어.",
                new List<FunctionCall>
                {
                    new(new Action(MainCharacter.little_jump))
                }
            ));

            this.units.Add(new StoryUnit(
                "루나",
                "나랑 같이 잠깐 산책하러 가자. 맑은 공기를 마시면 도움이 될거야!",
                new List<FunctionCall>
                {
                    new(new Action<float>(Luna.fast_jump), 0f),
                    new (new Action<float>(Luna.fast_jump), 0.5f)
                }
            ));
            
            this.units.Add(new StoryUnit(
                "",
                "",
                new List<FunctionCall>
                {
                    new(new Action(MainCharacter.disappear_right_move)),
                    new (new Action(Luna.disappear_left_move))
                }
            ));
            this.units.Add(new StoryUnit(
                "",
                "",
                new List<FunctionCall>
                {
                }
            ));
        }
    }
}