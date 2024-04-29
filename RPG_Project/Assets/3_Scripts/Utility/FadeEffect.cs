using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState { FadeIn = 0, FadeOut = 1, FadeInOut, FadeLoop };

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime;            // 페이드가 진행될 시간
    [SerializeField] private AnimationCurve fadeCurve;  // 변화를 곡선으로 정할수 있다
    public FadeState fadeState;

    public delegate void FadeOutCallBack();

    private Image image => GetComponent<Image>();

    private void Start()
    {
        // OnFade(fadeState);
    }

    public void OnFade(FadeState state, FadeOutCallBack callBack)
    {
        fadeState = state;
        switch (fadeState)
        {
            case FadeState.FadeIn:
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut:
            case FadeState.FadeLoop:
                StartCoroutine(FadeInOut());
                break;
        }

        IEnumerator Fade(float start, float end)
        {
            float timeValue = 0.0f;
            float percent = 0.0f;

            while(percent < 1)
            {
                timeValue += Time.deltaTime;        // 직관적으로 시간이 더해지는 변 수
                percent = timeValue / fadeTime;    // 0 ~ 1사이 값을 가지고, fade의 lerp시간을 결저하는 percent값

                Color color = image.color;
                //color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
                color.a = Mathf.Lerp(start, end, percent);

                image.color = color;
            
                yield return null;
            }
            callBack();

        }
        IEnumerator FadeInOut()
        {
            while (true)
            {
                yield return StartCoroutine(Fade(1, 0));
                yield return StartCoroutine(Fade(0, 1));


                if (fadeState == FadeState.FadeInOut)
                {
                    break;
                }

            }
        }
    }
}
