using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace GameBackend.StoryScript
{
    public class Character:MonoBehaviour
    {
        private float y;
        private void Awake()
        {
            y = transform.position.y;
            Invoke("fall_down", 2);
            Invoke("little_jump", 1);
            Invoke("appear_right", 4);
            Invoke("appear_left", 6);
        }

        public void appear_right()
        {
            var tmp = transform.position;
            tmp.x = 13;
            tmp.y = y;
            transform.position = tmp;
            StartCoroutine(movex(-7, 0.7f));
        }

        public void appear_left()
        {
            var tmp = transform.position;
            tmp.x = -13;
            tmp.y = y;
            transform.position = tmp;
            StartCoroutine(movex(7, 0.7f));
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
    }
}