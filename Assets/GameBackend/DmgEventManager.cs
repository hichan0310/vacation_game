using GameBackend.Events;
using UnityEngine;

namespace GameBackend
{
    public class DmgEventManager : MonoBehaviour
    {
        public static DmgEventManager Instance { get; private set; }

        public GameObject dmgDisplay;
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this; // Singleton 초기화
            }
            else
            {
                Destroy(gameObject); // 중복된 매니저 제거
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void TriggerDmgTakeEvent(DmgTakeEvent dmgEvent)
        {
            //Debug.Log($"Event Triggered: ({dmgEvent.name}, {dmgEvent.realDmg})");
            // 이벤트 처리 로직
            
            GameObject text = Instantiate(dmgDisplay);
            text.GetComponent<DamageDisplay>().ShowDamage(dmgEvent);
        }
    }
}