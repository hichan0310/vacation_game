using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }
    
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

    // 나중에 키 설정 변경 가능하게 만들 예정
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Jump = Input.GetKeyDown(KeyCode.Space);
        Attack = Input.GetMouseButtonDown(0);
    }
}