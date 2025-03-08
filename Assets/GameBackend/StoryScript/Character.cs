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
        }

        public void appear_right()
        {
            
        }

        public void appear_left()
        {
            
        }

        public void little_jump()
        {
            StartCoroutine(littleJump());
        }

        public void fall_down()
        {
            StartCoroutine(fallDown());
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
        }
    }
}