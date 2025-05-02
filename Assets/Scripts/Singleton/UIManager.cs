using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float duration;
    public static UIManager Instance { get;private set;}
    private void Awake()
    {
        if (Instance != null&&Instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void StartFadeIn()
    {
        StartCoroutine(FadeTo(0f));
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeTo(1f));
    }
    public void EfectFade()
    {
        StartCoroutine(FadeOutIn());
    }
    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = image.color.a;
        float time = 0f;

        while (time < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            time += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
    }
    private IEnumerator FadeOutIn()
    {
        yield return StartCoroutine(FadeTo(1f)); 
        yield return new WaitForSeconds(0.2f);    
        yield return StartCoroutine(FadeTo(0f)); 
    }
}
