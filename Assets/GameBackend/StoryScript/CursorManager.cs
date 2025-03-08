using UnityEngine;
using UnityEngine.UI;

public class UICursorChanger : MonoBehaviour
{
    public RectTransform cursorUI; 

    void Awake()
    {
        cursorUI.gameObject.SetActive(false); 
        Cursor.visible = true; 
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition.y);
            Debug.Log(Screen.height / 3);
        }
        cursorUI.position = Input.mousePosition;
        if (Input.mousePosition.y <= Screen.height / 3)
        {
            if (!cursorUI.gameObject.activeSelf) 
            {
                cursorUI.gameObject.SetActive(true);
                Cursor.visible = false;
            }
        }
        else
        {
            if (cursorUI.gameObject.activeSelf)
            {
                cursorUI.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }
    }
}