using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class StartsceneUItext : MonoBehaviour
{   
    public GameObject UItext;
    public GameObject SettingUI;
    public GameObject backbg;
    public GameObject keysetbg;
    public GameObject fullbg;
    public GameObject windowbg;
    public Slider mastersound;
    public Slider musicsound;
    public Slider effectsound;
    public TMP_Text mastersoundtext;
    public TMP_Text musicsoundtext;
    public TMP_Text effectsoundtext;
    bool key = false; // DoTween 최적화를 위한 실행확인 트리거거
    bool screenmode = true;
    bool isSetting = false; // 세팅창을 켜두었는가가
    
    void Start()
    {

    }

    void Update()
    {   
        if(isSetting == false) 
        {
            UItext.SetActive(true);
            SettingUI.SetActive(false);
            if(MouseOnUI.isMouseOver) 
            { // 두트윈을 이용한 텍스트 크기 변화 (마우스에 닿은 UI)
                if(MouseOnUI.gameObj.name != "cont" || MouseOnUI.allow == true)
                {
                    if(MouseOnUI.gameObj != null) MouseOnUI.gameObj.transform.DOScale(1.2f, 0.5f);
                    key = true;
                }
            }
            else if(key == true) 
            {
                if(MouseOnUI.gameObj != null) MouseOnUI.gameObj.transform.DOScale(1f, 0.5f);
                key = false;
            }
            if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "start" && Input.GetMouseButtonDown(0)) 
            { // 새 게임
                Loading.loadScene("ProlScene");
            }
            else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "cont" && Input.GetMouseButtonDown(0) && MouseOnUI.allow == true) 
            { // 계속하기
                Debug.Log("cont");
            }
            else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "setting" && Input.GetMouseButtonDown(0)) 
            { // 설정 메뉴
                MouseOnUI.gameObj.transform.DOScale(1f, 0.5f);
                key = false;
                isSetting = true;
                SettingUI.SetActive(true);
                UItext.SetActive(false);
            }
            else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "quit" && Input.GetMouseButtonDown(0)) 
            { // 게임 종료
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit(); 
            #endif
            }
        }
        else
        {
            mastersoundtext.text = (mastersound.value * 10).ToString("F0");
            musicsoundtext.text = (musicsound.value * 10).ToString("F0");
            effectsoundtext.text = (effectsound.value * 10).ToString("F0");
            if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "back") 
            { 
                Color color = backbg.GetComponent<Image>().color;
                color.a = 0.823f;
                backbg.GetComponent<Image>().color = color;
                if(Input.GetMouseButtonDown(0)) 
                {
                    isSetting = false;
                    UItext.SetActive(true);
                    SettingUI.SetActive(false);
                }
            }
            else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "keysetting") 
            { 
                Color color = keysetbg.GetComponent<Image>().color;
                color.a = 0.823f;
                keysetbg.GetComponent<Image>().color = color;
                if(Input.GetMouseButtonDown(0)) 
                {
                    // 키 세팅 만들자
                }
            }
            else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "full") 
            { 
                Color color = fullbg.GetComponent<Image>().color;
                color.a = 0.823f;
                fullbg.GetComponent<Image>().color = color;
                if(Input.GetMouseButtonDown(0)) 
                {
                    screenmode = true;
                    Color color2 = windowbg.GetComponent<Image>().color;
                    color2.a = 0.47f;
                    windowbg.GetComponent<Image>().color = color2;
                }
            }
            else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "window") 
            { 
                Color color = windowbg.GetComponent<Image>().color;
                color.a = 0.823f;
                windowbg.GetComponent<Image>().color = color;
                if(Input.GetMouseButtonDown(0)) 
                {
                    screenmode = false;
                    Color color2 = fullbg.GetComponent<Image>().color;
                    color2.a = 0.47f;
                    fullbg.GetComponent<Image>().color = color2;
                }
            }
            else
            {
                Color color = backbg.GetComponent<Image>().color;
                color.a = 0.47f;
                backbg.GetComponent<Image>().color = color;
                Color color2 = keysetbg.GetComponent<Image>().color;
                color2.a = 0.47f;
                keysetbg.GetComponent<Image>().color = color2;
                if(screenmode == true)
                {
                    Color color3 = fullbg.GetComponent<Image>().color;
                    color3.a = 0.823f;
                    fullbg.GetComponent<Image>().color = color3;
                    Color color4 = windowbg.GetComponent<Image>().color;
                    color4.a = 0.47f;
                    windowbg.GetComponent<Image>().color = color4;
                }
                else
                {
                    Color color3 = fullbg.GetComponent<Image>().color;
                    color3.a = 0.47f;
                    fullbg.GetComponent<Image>().color = color3;
                    Color color4 = windowbg.GetComponent<Image>().color;
                    color4.a = 0.823f;
                    windowbg.GetComponent<Image>().color = color4;
                }
            }
        }

    }

}
