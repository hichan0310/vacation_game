using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    public static KeyCode MoveUp { get; private set; } = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveUp"));
    public static KeyCode MoveDown { get; private set; } = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveDown"));
    public static KeyCode MoveLeft { get; private set; } = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft"));
    public static KeyCode MoveRight { get; private set; } = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight"));
    public static KeyCode Jump { get; private set; } = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump"));
    public static KeyCode Skill { get; private set; } = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill"));
    public static KeyCode Ultimate { get; private set; } = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ultimate"));

    
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

    }
}