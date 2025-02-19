using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    public bool MoveUp { get; private set; }
    public bool MoveDown { get; private set; }
    public bool MoveLeft { get; private set; }
    public bool MoveRight { get; private set; }
    public bool Jump { get; private set; }
    public bool Skill { get; private set; }
    public bool Ultimate { get; private set; }
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
        MoveUp = Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveUp")));
        MoveDown = Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveDown")));
        MoveLeft = Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft")));
        MoveRight = Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight")));
        Jump = Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump")));
        Skill = Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill")));
        Ultimate = Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ultimate")));
        Attack = Input.GetMouseButtonDown(0);
    }
}