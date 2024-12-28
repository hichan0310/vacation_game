using UnityEngine.EventSystems;
using UnityEngine;

public class MouseOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool allow = true; // 이어하기 할 게임 데이터가 있는가
    public static bool isMouseOver = false; // 마우스를 UI위에 올려놨는지 확인
    public static GameObject gameObj;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "cont")
        {
            allow = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // 마우스가 UI위에 올려져있음
    {
        isMouseOver = true;
        gameObj = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData) // 마우스가 UI위에서 내려감
    {
        isMouseOver = false;
        gameObj = gameObject;
    }
}
