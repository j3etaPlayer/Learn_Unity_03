using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public static Ending Instance;

    [Header("Step 1")]
    public GameObject[] endingTexts;
    public float textDeley = 0.2f;

    [Header("step 2")]
    public GameObject stepTwoObj;
    public TextMeshProUGUI stepTwoText;
    public string context = "Made By P J M";

    [Header("Credit")]
    public GameObject credit;

    [Header("Fade Effect")]
    public FadeEffect fadeEffect;

    public AudioClip bgm;
    public AudioSource audioSource;
    public Animator animator;

    public bool endingStart = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameObject.SetActive(false);
    }
    public void PlayEndingScene()
    {
        gameObject.SetActive(true);
        audioSource.PlayOneShot(bgm);
        StartCoroutine(ShowTextAnimation());
    }
    private void Start()
    {
    }

    IEnumerator ShowTextAnimation()
    {
        foreach(var text in endingTexts)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(textDeley);
        }
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.OnFade(FadeState.FadeOut, StepTwo);
    }

    IEnumerator ShowMessegeAnimation()
    {
        stepTwoText.text = "";

        for (int i = 0; i < context.Length; i++)
        {
            stepTwoText.text += context[i];
            yield return new WaitForSeconds(textDeley);
        }
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.OnFade(FadeState.FadeOut, ShowEndingCredit);
    }
    void StepTwo()
    {
        stepTwoObj.SetActive(true);
        fadeEffect.gameObject.SetActive(false);
        StartCoroutine(ShowMessegeAnimation());
    }

    private void ShowEndingCredit()
    {
        fadeEffect.gameObject.SetActive(false);
        credit.SetActive(true);
        animator.CrossFade("ShowCredit", 0.1f);
    }

    public void CreditEnd()
    {
        LoadingUI.LoadScene("TitleScene");
    }

}
