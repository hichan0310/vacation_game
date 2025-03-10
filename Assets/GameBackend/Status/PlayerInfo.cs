using GameBackend.Skills;
using GameBackend.Skills.NormalSkill;
using GameBackend.Skills.SpecialSkill;

namespace GameBackend.Status
{
    public interface IPlayerInfo
    {
        public PlayerStatus getPlayerStatus();
        public string name { get; }
        public string explaination { get; }
        public ISkill normalSkill { get; }
        public ISkill specialSkill { get; }
    }

    public class CommonPlayerInfo : IPlayerInfo
    {
        public string name { get; }
        public string explaination { get; }

        public ISkill normalSkill => new TestSkill();

        public ISkill specialSkill => new TestSpecialSkill();

        public CommonPlayerInfo()
        {
            this.name = "name";
            this.explaination = "empty";
        }

        public PlayerStatus getPlayerStatus()
        {
            return new PlayerStatus(10000, 1000, 1000);
        }
    }

    public class TestPlayerInfo1: IPlayerInfo
    {
        public PlayerStatus getPlayerStatus()
        {
            return new PlayerStatus(10000, 1000, 1000);
        }

        public string name { get; }
        public string explaination { get; }
        public ISkill normalSkill => new TestSkill();
        public ISkill specialSkill => new TestSpecialSkill();
        
        /*
         * e로 근처 적에게 피해를 주고 넉백
         * 일반공격 하면 강한 범위 공격(아야토 6돌)
         * 최대 3번 발동
         * 쿨타임 5초
         * 
         * q 쓰면 7초동안 시간 정지하고 모든 공격으로 적에게 디버프 스택을 쌓을 수 있다
         * 시간 정지가 끝나면 디버프 스택에 비례한 피해를 추가로 가하고
         * 스택에 비례한 강도의 화상 효과 부여
         */
    }
}