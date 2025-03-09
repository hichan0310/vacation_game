using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Dialogue : MonoBehaviour
{
    public static bool isTalking = false;

    public static IEnumerator Typing(TMP_Text textfield_name, TMP_Text textfield_text, string name, string talk, float speed) 
    { 
        isTalking = true;
        if (talk.Length == 0)
        {
            textfield_name.text = "";
            textfield_text.text = "";
        }
        else
        {
            for (int i = 0; i < talk.Length; i++) 
            { 
                textfield_name.text = name.Substring(0, name.Length); 
                textfield_text.text = talk.Substring(0, i + 1); 
                yield return new WaitForSeconds(speed);
            }
        }
        yield return null;
        isTalking = false;
        yield break;
    } 

    public static IEnumerator Typing_All(TMP_Text textfield_name, TMP_Text textfield_text, string name, string talk) 
    { 
        textfield_name.text = name; 
        textfield_text.text = talk; 
        yield return null;
        isTalking = false;
        yield break;
    }
}
