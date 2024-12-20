using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartsceneUItext : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    GameObject gameObj;
    bool key = false;
    bool allow = true;
    bool isMouseOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "cont")
        {
            allow = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        gameObj = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        gameObj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(isMouseOver && allow == true) {
            gameObj.transform.DOScale(1.2f, 0.5f)
            .SetAutoKill(true);
            key = true;
        }
        else if(key == true) {
            gameObj.transform.DOScale(1f, 0.5f)
            .SetAutoKill(true);
            key = false;
        }
    }

}
