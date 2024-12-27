using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  
using DG.Tweening;
public class Loading : MonoBehaviour
{

    public CanvasGroup Fade_img;
    public GameObject loading;
    float fadeDuration = 1.5f; 
    public static string nextScene;

    public Image progressBar;

    private void Start()
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(()=>{

        })
        .OnComplete(()=>{
            Fade_img.blocksRaycasts = false;
        });
        StartCoroutine(LoadScene());
    }

    public static void loadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadScene");
    }


    IEnumerator LoadScene()
    {
        progressBar.fillAmount = 0.0f;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while(!op.isDone)
        {
            yield return null;
            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount=Mathf.Lerp(progressBar.fillAmount,1f,timer);
                if(progressBar.fillAmount == 1.0f)
                {
                    yield return new WaitForSeconds(0.5f);
                    Fade_img.DOFade(1, fadeDuration)
                    .OnStart(()=>{
                        Fade_img.blocksRaycasts = true;
                    })
                    .OnComplete(()=>{

                     });
                    yield return new WaitForSeconds(1.5f);
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }
}