using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] CanvasGroup m_BlackPanel;
    [SerializeField] float m_sceneTransitionTime = 2f;
    private Coroutine m_currentRoutine;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string sceneName)
    {


        StartCoroutine(FadeTransition(m_sceneTransitionTime,sceneName));
        
       

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


    private IEnumerator FadeTransition(float duration,string sceneName)
    {
        FadeIn(duration);
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneName);
        FadeOut(duration);
    }
}
