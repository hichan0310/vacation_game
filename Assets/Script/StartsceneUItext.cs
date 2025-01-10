using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
public class StartsceneUItext : MonoBehaviour
{   
    public GameObject UItext;
    public GameObject SettingUI;
    public GameObject backbg;
    public GameObject keysetbg;
    public GameObject keysetUI;
    public GameObject fullbg;
    public GameObject windowbg;
    public GameObject windowed;
    public GameObject setresolution;
    public GameObject textset;
    public Image arrow1;
    public Image arrow2;
    public Slider mastersound;
    public Slider musicsound;
    public Slider effectsound;
    public TMP_Text mastersoundtext;
    public TMP_Text musicsoundtext;
    public TMP_Text effectsoundtext;
    public TMP_Text resolutiontext;
    public TMP_Text moveuptext;
    public TMP_Text movedowntext;
    public TMP_Text movelefttext;
    public TMP_Text moverighttext;
    public TMP_Text jumptext;
    private Dictionary<string, KeyCode> keysetting = new Dictionary<string, KeyCode>();
    string[] keylist = {"MoveUp", "MoveDown", "MoveLeft", "MoveRight", "Jump"};
    string iswaitkeyset = "";
    string keytemp = "";
    KeyCode valuetemp;
    int screenmode;
    int HW;
    int height;
    int width;
    int fullheight;
    int fullwidth;
    int[] heightarray = {360, 540, 576, 720, 768, 900, 1080, 1152, 1440, 1620};
    int[] widtharray = {640, 960, 1024, 1280, 1366, 1600, 1920, 2048, 2560, 2880};
    bool key = false; // DoTween 최적화를 위한 실행확인 트리거거
    bool isSetting = false; // 세팅창을 켜두었는가
    bool isKeysetting = false;

    public enum ScreenMode
    {
        FullScreenWindow,
        Window
    }
    void Awake()
    {
        if (PlayerPrefs.HasKey("HW"))
        {
            HW = PlayerPrefs.GetInt("HW"); 
            height = heightarray[PlayerPrefs.GetInt("HW")]; 
            width = widtharray[PlayerPrefs.GetInt("HW")]; 
        }
        else
        {
            PlayerPrefs.SetInt("HW", 6);
            PlayerPrefs.Save();
            HW = 6;
            height = heightarray[6]; 
            width = widtharray[6]; 
        }


        if (PlayerPrefs.HasKey("ScreenMode"))
        {
            if (PlayerPrefs.GetInt("ScreenMode") == 0)
                Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
            else
                Screen.SetResolution(width, height, FullScreenMode.Windowed);
            screenmode = PlayerPrefs.GetInt("ScreenMode");
        }
        else
        {
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
            PlayerPrefs.SetInt("ScreenMode", 0);
            PlayerPrefs.Save();
            screenmode = 0;
        }

        if (PlayerPrefs.HasKey("Height") && PlayerPrefs.HasKey("Width"))
        {
            fullheight = PlayerPrefs.GetInt("Height"); 
            fullwidth = PlayerPrefs.GetInt("Width"); 
        }
        else
        {
            PlayerPrefs.SetInt("Height", Screen.height);
            PlayerPrefs.SetInt("Width", Screen.width);
            PlayerPrefs.Save();
            fullheight = PlayerPrefs.GetInt("Height"); 
            fullwidth = PlayerPrefs.GetInt("Width");  
        }

        if (PlayerPrefs.HasKey("Sound1"))
        {
            mastersound.value = PlayerPrefs.GetFloat("Sound1");
        }
        else
        {
            PlayerPrefs.SetFloat("Sound1", 1);
            PlayerPrefs.Save();
            mastersound.value = 1; 
        }

        if (PlayerPrefs.HasKey("Sound2"))
        {
            musicsound.value = PlayerPrefs.GetFloat("Sound2");
        }
        else
        {
            PlayerPrefs.SetFloat("Sound2", 1);
            PlayerPrefs.Save();
            musicsound.value = 1; 
        }

        if (PlayerPrefs.HasKey("Sound3"))
        {
            effectsound.value = PlayerPrefs.GetFloat("Sound3");
        }
        else
        {
            PlayerPrefs.SetFloat("Sound3", 1);
            PlayerPrefs.Save();
            effectsound.value = 1; 
        }

        if (PlayerPrefs.HasKey("MoveUp"))
        {
            foreach (string key in keylist)
            {
                keysetting[key] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(key));
            } 
        }
        else
        {
            keysetting["MoveUp"] = KeyCode.W;
            keysetting["MoveDown"] = KeyCode.S;
            keysetting["MoveLeft"] = KeyCode.A;
            keysetting["MoveRight"] = KeyCode.D;
            keysetting["Jump"] = KeyCode.Space;
            foreach (string key in keylist)
                PlayerPrefs.SetString(key, $"{keysetting[key]}");       
        }
    }
    
    void Start()
    {

    }

    private void OnGUI()
    {
        if (iswaitkeyset != "")
        {
            if (Event.current != null && Event.current.isKey)
            {
                KeyCode detectedKey = Event.current.keyCode;
                keytemp = FindKeyByValue(keysetting, detectedKey);
                if(keytemp == null)
                {
                    PlayerPrefs.SetString(iswaitkeyset, $"{detectedKey}");
                    keysetting[iswaitkeyset] = detectedKey;
                }
                else
                {
                    keysetting[iswaitkeyset] = detectedKey;
                    keysetting[keytemp] = valuetemp;
                    PlayerPrefs.SetString(iswaitkeyset, $"{detectedKey}");
                    PlayerPrefs.SetString(keytemp, $"{valuetemp}");
                }
                iswaitkeyset = "";
                moveuptext.text = $"{keysetting["MoveUp"]}";
                movedowntext.text = $"{keysetting["MoveDown"]}";
                movelefttext.text = $"{keysetting["MoveLeft"]}";
                moverighttext.text = $"{keysetting["MoveRight"]}";
                jumptext.text = $"{keysetting["Jump"]}";
            }
        }
    }

    private string FindKeyByValue(Dictionary<string, KeyCode> dictionary, KeyCode value)
    {
        foreach (var pair in dictionary)
        {
            if (pair.Value.Equals(value))
            {
                return pair.Key;
            }
        }
        return null;
    }

    void Update()
    {   
        if(isSetting == false) 
        {
            UItext.SetActive(true);
            SettingUI.SetActive(false);
            keysetUI.SetActive(false);
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
            if(isKeysetting)
            {
                mastersound.interactable = false;
                musicsound.interactable = false;
                effectsound.interactable = false;
                if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "moveuptmp" && iswaitkeyset == "") 
                { 
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        valuetemp = keysetting["MoveUp"];
                        moveuptext.text = "";
                        iswaitkeyset = "MoveUp";
                    }
                }
                else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "movedowntmp" && iswaitkeyset == "") 
                { 
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        valuetemp = keysetting["MoveDown"];
                        movedowntext.text = "";
                        iswaitkeyset = "MoveDown";
                    }
                }
                else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "movelefttmp" && iswaitkeyset == "") 
                { 
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        valuetemp = keysetting["MoveLeft"];
                        movelefttext.text = "";
                        iswaitkeyset = "MoveLeft";
                    }
                }
                else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "moverighttmp" && iswaitkeyset == "") 
                { 
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        valuetemp = keysetting["MoveRight"];
                        moverighttext.text = "";
                        iswaitkeyset = "MoveRight";
                    }
                }
                else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "jumptmp" && iswaitkeyset == "") 
                { 
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        valuetemp = keysetting["Jump"];
                        jumptext.text = "";
                        iswaitkeyset = "Jump";
                    }
                }
                if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "savetext" && iswaitkeyset == "")
                {
                    Color color = textset.GetComponent<Image>().color;
                    color.a = 0.823f;
                    textset.GetComponent<Image>().color = color;
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        isKeysetting = false;
                        keysetbg.SetActive(true);
                        keysetUI.SetActive(false);
                        PlayerPrefs.Save();
                    }
                }
                else
                {
                    Color color = textset.GetComponent<Image>().color;
                    color.a = 0.47f;
                    textset.GetComponent<Image>().color = color;
                }
            }
            else
            {
                mastersound.interactable = true;
                musicsound.interactable = true;
                effectsound.interactable = true;
                if(screenmode == 0)
                {
                    windowed.SetActive(false);
                }
                else
                {
                    windowed.SetActive(true);
                    resolutiontext.text = $"{widtharray[HW]} * {heightarray[HW]}";
                    if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "arrow1") 
                    { 
                        arrow1.color = new Color(1f, 1f, 1f);
                        if(Input.GetMouseButtonDown(0)) 
                        {
                            if(HW > 0)
                            {
                                HW -= 1;
                            }
                        }
                    }
                    else
                        arrow1.color = new Color(0.5f, 0.5f, 0.5f); 
                    if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "arrow2") 
                    { 
                        arrow2.color = new Color(1f, 1f, 1f);
                        if(Input.GetMouseButtonDown(0)) 
                        {
                            if(HW < 9)
                            {
                                HW += 1;
                            }
                        }
                    }
                    else
                        arrow2.color = new Color(0.5f, 0.5f, 0.5f); 
                    if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "applyresolution") 
                    { 
                        Color color = setresolution.GetComponent<Image>().color;
                        color.a = 0.8f;
                        setresolution.GetComponent<Image>().color = color;
                        if(Input.GetMouseButtonDown(0)) 
                        {
                            PlayerPrefs.SetInt("HW", HW);
                            width = widtharray[HW];
                            height = heightarray[HW];
                            Screen.SetResolution(width, height, FullScreenMode.Windowed);
                        }
                    }
                    else
                    {
                        Color color = setresolution.GetComponent<Image>().color;
                        color.a = 0.4f;
                        setresolution.GetComponent<Image>().color = color;
                    }
                }
                if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "back") 
                { 
                    Color color = backbg.GetComponent<Image>().color;
                    color.a = 0.823f;
                    backbg.GetComponent<Image>().color = color;
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        PlayerPrefs.SetFloat("Sound1", mastersound.value);
                        PlayerPrefs.SetFloat("Sound2", musicsound.value);
                        PlayerPrefs.SetFloat("Sound3", effectsound.value);
                        isSetting = false;
                        UItext.SetActive(true);
                        SettingUI.SetActive(false);
                        PlayerPrefs.Save();
                    }
                }
                else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "keysetting") 
                { 
                    Color color = keysetbg.GetComponent<Image>().color;
                    color.a = 0.823f;
                    keysetbg.GetComponent<Image>().color = color;
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        keysetbg.SetActive(false);
                        keysetUI.SetActive(true);
                        moveuptext.text = $"{keysetting["MoveUp"]}";
                        movedowntext.text = $"{keysetting["MoveDown"]}";
                        movelefttext.text = $"{keysetting["MoveLeft"]}";
                        moverighttext.text = $"{keysetting["MoveRight"]}";
                        jumptext.text = $"{keysetting["Jump"]}";
                        isKeysetting = true;
                    }
                }
                else if(MouseOnUI.isMouseOver && MouseOnUI.gameObj.name == "full") 
                { 
                    Color color = fullbg.GetComponent<Image>().color;
                    color.a = 0.823f;
                    fullbg.GetComponent<Image>().color = color;
                    if(Input.GetMouseButtonDown(0)) 
                    {
                        Screen.SetResolution(fullwidth, fullheight, FullScreenMode.FullScreenWindow);
                        PlayerPrefs.SetInt("ScreenMode", 0);
                        screenmode = 0;
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
                        Screen.SetResolution(width, height, FullScreenMode.Windowed);
                        PlayerPrefs.SetInt("ScreenMode", 1);
                        screenmode = 1;
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
                    if(screenmode == 0)
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

}
