using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace GameFrontEnd.StoryScript
{
    public class Character:MonoBehaviour
    {
        private SpriteRenderer renderer;
        private float fadeDuration = 1f;
        private float y;

        // private Vector3 finalPosition;
        // private float finalAlpha = 1f;
        // private Vector3 finalScale;
        // private float finalRotation;
        
        public List<IEnumerator> coroutines = new();
        
        public void StartAndStoreCoroutine(IEnumerator coroutine)
        {
            coroutines.Add(coroutine);
            StartCoroutine(CoroutineWrapper(coroutine));
        }

        private IEnumerator CoroutineWrapper(IEnumerator coroutine)
        {
            // 실제 코루틴 실행
            yield return StartCoroutine(coroutine);
            // 코루틴이 끝나면 리스트에서 제거
            coroutines.Remove(coroutine);
        }
        
        public void StopAllStoredCoroutines()
        {
            foreach (IEnumerator coroutine in coroutines)
            {
                StopCoroutine(coroutine);
            }
            coroutines.Clear();
        }


        // public void setFinal()
        // {
        //     StopAllStoredCoroutines();
        //     transform.position = finalPosition;
        //     transform.eulerAngles=new Vector3(0, 0, finalRotation);
        //     transform.localScale=finalScale;
        //     SetAlpha(finalAlpha);
        //     Debug.Log($"fucking work({finalPosition})");
        // }

        private IEnumerator completeCoroutine()
        {
            yield return null;
        }
        
        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            // finalRotation = transform.eulerAngles.z;
            // finalScale = transform.localScale;
            y = transform.position.y;
            setPosition(new Vector3(0, -20, 0));
            // finalPosition = transform.position;
        }
        
        public void SetAlpha(float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            Color c = renderer.color;
            c.a = alpha;
            renderer.color = c;
            // finalAlpha = alpha;
        }
        public void setPosition(Vector3 position)
        {
            transform.position = position;
            // finalPosition = transform.position;
        }

        public void setRotation(float degree)
        {
            this.transform.eulerAngles = new Vector3(0, 0, degree);
            // finalRotation = transform.eulerAngles.z;
        }

        public void setScale(Vector3 scale)
        {
            transform.localScale = scale;
            // finalScale = transform.localScale;
        }
        public void appear_right_move(float[] param_list)
        {
            var tmp = transform.position;
            tmp.x = 15;
            tmp.y = y;
            transform.position = tmp;
            StartAndStoreCoroutine(movex(-9, 0.7f));
            // finalPosition = tmp+new Vector3(-9, 0, 0);
        }

        public void disappear_right_move(float[] param_list)
        {
            var tmp = transform.position;
            tmp.x = 18;
            StartAndStoreCoroutine(movex(18-transform.position.x, 0.7f));
            // finalPosition = tmp;
        }

        public void appear_left_move(float[] param_list)
        {
            var tmp = transform.position;
            tmp.x = -15;
            tmp.y = y;
            transform.position = tmp;
            StartAndStoreCoroutine(movex(9, 0.7f));
            // finalPosition = tmp+new Vector3(9, 0, 0);
        }
        
        public void disappear_left_move(float[] param_list)
        {
            var tmp = transform.position;
            tmp.x = -18;
            StartAndStoreCoroutine(movex(-18-transform.position.x, 0.7f));
            // finalPosition = tmp;
        }

        public void little_jump(float[] param_list)
        {
            StartAndStoreCoroutine(littleJump());
        }

        public void fast_jump(float[] param_list)
        {
            float time = param_list[0];
            StartAndStoreCoroutine(fastJump(time));
        }

        public void dori_dori(float[] param_list)
        {
            StartAndStoreCoroutine(doridori());
        }

        public void fall_down(float[] param_list)
        {
            StartAndStoreCoroutine(fallDown());
            // finalPosition=transform.position+new Vector3(0, -10f, 0);
        }

        public void move_x(float[] param_list)
        {
            float deltax = param_list[0];
            float time = param_list[1];
            StartAndStoreCoroutine(movex(deltax, time));
            // finalPosition = transform.position+new Vector3(deltax, 0, 0);
        }
        
        private IEnumerator movex(float deltax, float time)
        {
            yield return transform.DOMoveX(transform.position.x + deltax, time)
                .SetEase(Ease.Unset)
                .WaitForCompletion();
        }

        private IEnumerator littleJump()
        {
            yield return transform.DOMoveY(transform.position.y + 0.4f, 0.15f)
                .SetEase(Ease.OutCubic)
                .WaitForCompletion();
            yield return transform.DOMoveY(transform.position.y - 0.4f, 0.15f)
                .SetEase(Ease.InCubic)
                .WaitForCompletion();
        }

        private IEnumerator fastJump(float time)
        {
            yield return new WaitForSeconds(time);
            yield return transform.DOMoveY(transform.position.y + 1.2f, 0.2f)
                .SetEase(Ease.OutCubic)
                .WaitForCompletion();
            yield return transform.DOMoveY(transform.position.y - 1.2f, 0.2f)
                .SetEase(Ease.InCubic)
                .WaitForCompletion();
        }

        private IEnumerator fallDown()
        {
            yield return transform.DORotate(new Vector3(0, 0, -10), 0.5f)
                .SetEase(Ease.Unset) 
                .WaitForCompletion();
            
            yield return transform.DORotate(new Vector3(0, 0, 10), 0.5f)
                .SetEase(Ease.Unset) 
                .WaitForCompletion();
            
            yield return transform.DOMoveY(transform.position.y - 10, 0.5f)
                .SetEase(Ease.Unset)
                .WaitForCompletion();
            
            yield return transform.DORotate(Vector3.zero, 0.01f)
                .SetEase(Ease.Unset)
                .WaitForCompletion();
        }

        private IEnumerator doridori()
        {
            yield return transform.DORotate(new Vector3(0, 0, -5), 0.3f)
                .SetEase(Ease.Unset) 
                .WaitForCompletion();
            
            yield return transform.DORotate(new Vector3(0, 0, 5), 0.6f)
                .SetEase(Ease.Unset) 
                .WaitForCompletion();
            
            yield return transform.DORotate(Vector3.zero, 0.3f)
                .SetEase(Ease.Unset)
                .WaitForCompletion();
        }
        
        public void StartFadeOut()
        {
            StartAndStoreCoroutine(FadeOutRoutine(fadeDuration));
        }
        public void StartFadeIn()
        {
            StartAndStoreCoroutine(FadeInRoutine(fadeDuration));
        }

        private IEnumerator FadeOutRoutine(float time)
        {
            Color color = renderer.color;
            float startAlpha = color.a;
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(startAlpha, 0f, elapsedTime / time);
                renderer.color = color;
                yield return null;
            }

            color.a = 0f;
            renderer.color = color;
        }

        
        private IEnumerator FadeInRoutine(float time)
        {
            Color color = renderer.color;
            float startAlpha = 0f; // 처음엔 완전히 투명
            float elapsedTime = 0f;

            color.a = startAlpha;
            renderer.color = color;
            gameObject.SetActive(true); // 먼저 활성화

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(startAlpha, 1f, elapsedTime / time);
                renderer.color = color;
                yield return null;
            }

            color.a = 1f;
            renderer.color = color;
        }
    }
}