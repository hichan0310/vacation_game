using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace GameBackend.StoryScript
{
    public class Character:MonoBehaviour
    {
        private SpriteRenderer renderer;
        private float fadeDuration = 1f;
        private float y;
        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            y = transform.position.y;
            Invoke("fall_down", 2);
            Invoke("little_jump", 1);
            Invoke("appear_right_move", 4);
            Invoke("disappear_right_move", 5);
            Invoke("appear_left_move", 6);
            Invoke("disappear_left_move", 7);
            Invoke("appear_right_move", 8);
            Invoke("StartFadeOut", 9);
            Invoke("StartFadeIn", 11);
        }

        public void setPosition(float x, float y)
        {
            this.transform.position = new Vector3(x, y, 0);
        }

        public void appear_right_move()
        {
            var tmp = transform.position;
            tmp.x = 15;
            tmp.y = y;
            transform.position = tmp;
            StartCoroutine(movex(-9, 0.7f));
        }
        public void disappear_right_move()
        {
            StartCoroutine(movex(18-transform.position.x, 0.7f));
        }

        public void appear_left_move()
        {
            var tmp = transform.position;
            tmp.x = -15;
            tmp.y = y;
            transform.position = tmp;
            StartCoroutine(movex(9, 0.7f));
        }
        
        public void disappear_left_move()
        {
            StartCoroutine(movex(-18-transform.position.x, 0.7f));
        }

        public void little_jump()
        {
            StartCoroutine(littleJump());
        }

        public void fall_down()
        {
            StartCoroutine(fallDown());
        }

        public void move_x(float deltax, float time)
        {
            StartCoroutine(movex(deltax, time));
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
        
        public void StartFadeOut()
        {
            StartCoroutine(FadeOutRoutine(fadeDuration));
        }
        public void StartFadeIn()
        {
            StartCoroutine(FadeInRoutine(fadeDuration));
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