using UnityEngine;

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