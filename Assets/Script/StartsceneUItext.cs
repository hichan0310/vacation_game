using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartsceneUItext : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    GameObject gameObj;
    bool key = false; // DoTween 최적화를 위한 실행확인 트리거거
    bool allow = true; // 이어하기 할 게임 데이터가 있는가
    bool isMouseOver = false; // 마우스를 UI위에 올려놨는지 확인
    bool isSetting = false; // 세팅창을 켜두었는가가
    
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

    private void Update()
    {   
        if(isSetting == false) {
            if(isMouseOver && allow == true) { // 두트윈을 이용한 텍스트 크기 변화 (마우스에 닿은 UI)
                if(gameObj != null) gameObj.transform.DOScale(1.2f, 0.5f);
                key = true;
            }
            else if(key == true) {
                if(gameObj != null) gameObj.transform.DOScale(1f, 0.5f);
                key = false;
            }
            if(isMouseOver && gameObj.name == "start" && Input.GetMouseButtonDown(0)) { // 새 게임
                Loading.loadScene("ProlScene");
            }
            else if(isMouseOver && gameObj.name == "cont" && Input.GetMouseButtonDown(0) && allow == true) { // 계속하기
                Debug.Log("cont");
            }
            else if(isMouseOver && gameObj.name == "setting" && Input.GetMouseButtonDown(0)) { // 설정 메뉴
                isSetting = true;
            }
            else if(isMouseOver && gameObj.name == "quit" && Input.GetMouseButtonDown(0)) { // 게임 종료
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit(); 
            #endif
            }
        }

    }

}
