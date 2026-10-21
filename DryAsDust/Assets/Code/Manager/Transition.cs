using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    private float m_introLenght = 1f;
    private float m_outroLenght = 1f;

    private Action m_OnIntroTransitionComplete;
    private Action m_OnOutroTransitionComplete;
    [SerializeField] CanvasGroup m_BlackPanel;
    private Coroutine m_currentRoutine;
    [SerializeField] private Image m_clickCatcher;
    [SerializeField] private Color m_clickCatcherFinalColor;

    public void StartTransition(Action intro = null, Action outro = null)
    {
        m_OnIntroTransitionComplete = intro;
        m_OnOutroTransitionComplete = outro;


    }


    public void FadeIn(float duration)
    {
        StartFade(1f, duration);
    }
    public void FadeOut(float duration)
    {
        StartFade(0f, duration);
    }


    private void StartFade(float targetAlpha, float duration)
    {
        if (m_currentRoutine != null)
        {
            StopCoroutine(m_currentRoutine);
        }
        m_currentRoutine = StartCoroutine(FadeRoutine(targetAlpha, duration));
    }

    private IEnumerator FadeRoutine(float targetAlpha, float fadeDuration)
    {

        float startAlpha = m_BlackPanel.alpha;
        float elapse = 0;

        while (elapse < fadeDuration)
        {
            elapse += Time.deltaTime;


            m_BlackPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapse / fadeDuration);
            yield return null;

        }

    }


    private IEnumerator FadeTransition(float duration, string sceneName)
    {
        FadeIn(duration);
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneName);
        FadeOut(duration);
    }

}



