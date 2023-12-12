using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public CanvasGroup Fade_img;
    public GameObject Loading;
    public Text Loading_text;
    float fadeDuration = 2f;
    private float percentage;
    public static MySceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            else
            {
                return instance;
            }
        }
    }

    private static MySceneManager instance;

    void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
            .OnStart(() =>
            {
                Loading.SetActive(false);
            })
            .OnComplete(() =>
            {
                Fade_img.blocksRaycasts = false;
            });

        percentage = 0;
    }

    public void ChangeScene(string sceneName)
    {
        Fade_img.DOFade(1, fadeDuration)
            .OnStart(() =>
            {
                Fade_img.blocksRaycasts = true;
            })
            .OnComplete(() =>
            {
                StartCoroutine("LoadScene", sceneName);
            });
    }
    IEnumerator LoadScene(string sceneName)
    {
        Loading.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        float past_time = 0;
        percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;
            if (percentage >= 90f)
            {
                percentage = Mathf.Lerp(percentage, 100f, past_time);
                if (percentage == 100f)
                {
                    async.allowSceneActivation = true;
                }
            }
            else if (percentage < 90f)
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90f)
                {
                    past_time = 0f;
                }
            }
            Loading_text.text = percentage.ToString("0") + "%";
        }
    }
}

