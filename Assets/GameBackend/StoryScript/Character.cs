﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace GameBackend.StoryScript
{
    public class Character:MonoBehaviour
    {
        private SpriteRenderer renderer;
        private float fadeDuration = 1f;
        private float y;

        private Vector3 finalPosition;
        private float finalAlpha = 1f;
        private Vector3 finalScale;
        private float finalRotation;
        
        private List<IEnumerator> coroutines = new();
        
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

        public void complete()
        {
            setPosition(finalPosition);
            SetAlpha(finalAlpha);
            setScale(finalScale);
            setRotation(finalRotation);
            StopAllStoredCoroutines();
            setPosition(finalPosition);
            SetAlpha(finalAlpha);
            setScale(finalScale);
            setRotation(finalRotation);
        }

        private IEnumerator completeCoroutine()
        {
            yield return null;
        }
        
        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            finalRotation = transform.eulerAngles.z;
            finalScale = transform.localScale;
            y = transform.position.y;
            
            
            setPosition(new Vector3(0, -20, 0));
            finalPosition = transform.position;
        }
        
        public void SetAlpha(float alpha)
        {
            // 알파값을 0~1 범위로 제한
            alpha = Mathf.Clamp01(alpha);

            Color c = renderer.color;
            c.a = alpha;
            renderer.color = c;
            finalAlpha = alpha;
        }

        public void setPosition(float x, float y)
        {
            this.transform.position = new Vector3(x, y, 0);
            finalPosition = transform.position;
        }

        public void setPosition(Vector3 position)
        {
            transform.position = position;
            finalPosition = transform.position;
        }

        public void setRotation(float degree)
        {
            this.transform.eulerAngles = new Vector3(0, 0, degree);
            finalRotation = transform.eulerAngles.z;
        }

        public void setScale(Vector3 scale)
        {
            transform.localScale = scale;
            finalScale = transform.localScale;
        }

        public void appear_right_move()
        {
            var tmp = transform.position;
            tmp.x = 15;
            tmp.y = y;
            transform.position = tmp;
            var cor = movex(-9, 0.7f);
            StartAndStoreCoroutine(cor);
            coroutines.Add(cor);
            finalPosition = tmp+new Vector3(6, 0, 0);
        }
        public void disappear_right_move()
        {
            var tmp = transform.position;
            tmp.x = 18;
            StartAndStoreCoroutine(movex(18-transform.position.x, 0.7f));
            finalPosition = tmp;
        }

        public void appear_left_move()
        {
            var tmp = transform.position;
            tmp.x = -15;
            tmp.y = y;
            transform.position = tmp;
            StartAndStoreCoroutine(movex(9, 0.7f));
            finalPosition = tmp+new Vector3(-6, 0, 0);
        }
        
        public void disappear_left_move()
        {
            var tmp = transform.position;
            tmp.x = -18;
            StartAndStoreCoroutine(movex(-18-transform.position.x, 0.7f));
            finalPosition = tmp;
        }

        public void little_jump()
        {
            StartAndStoreCoroutine(littleJump());
        }

        public void fall_down()
        {
            StartAndStoreCoroutine(fallDown());
            finalPosition=transform.position+new Vector3(0, -10f, 0);
        }

        public void move_x(float deltax, float time)
        {
            StartAndStoreCoroutine(movex(deltax, time));
            finalPosition = transform.position+new Vector3(deltax, 0, 0);
        }
        
        private IEnumerator movex(float deltax, float time)
        {
            yield return transform.DOMoveX(transform.position.x + deltax, time)
                .SetEase(Ease.Unset)
                .WaitForCompletion();
        }

        private IEnumerator littleJump()
        {
            yield return transform.DOMoveY(transform.position.y + 0.8f, 0.4f)
                .SetEase(Ease.OutCubic)
                .WaitForCompletion();
            yield return transform.DOMoveY(transform.position.y - 0.8f, 0.4f)
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