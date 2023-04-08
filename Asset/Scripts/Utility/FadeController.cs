using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;

    private void Awake()
    {
        instance = this; 
    }

    public void ImageFadeOut(GameObject[] targetObjects)
    {
        foreach(var Img in targetObjects)
        {
            StartCoroutine(FadeOutAlpha(Img));
        }
    }

    public void ImageFadeIn(GameObject[] targetObjects)
    {
        foreach (var Img in targetObjects)
        {
            StartCoroutine(FadeInAlpha(Img));
        }
    }

    private IEnumerator FadeOutAlpha(GameObject t_obj)
    {
        float fadeCount = 1;

        Image image = t_obj.GetComponent<Image>();

        for (int i = 0; i < 100; i++)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(255, 255, 255, fadeCount);
        }

        t_obj.SetActive(false);
    }

    private IEnumerator FadeInAlpha(GameObject t_obj)
    {
        float fadeCount = 0;

        Image image = t_obj.GetComponent<Image>();

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(255, 255, 255, fadeCount);
        }
    }
}
