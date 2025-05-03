using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private Image imageFade;
    [SerializeField] private float duration;

    [Header("Dialogue NPC")]
    [SerializeField] private Image imageNPC;
    [SerializeField] private TMP_Text textNPC;
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
        float startAlpha = imageFade.color.a;
        float time = 0f;

        while (time < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            imageFade.color = new Color(imageFade.color.r, imageFade.color.g, imageFade.color.b, newAlpha);
            time += Time.deltaTime;
            yield return null;
        }
        imageFade.color = new Color(imageFade.color.r, imageFade.color.g, imageFade.color.b, targetAlpha);
    }
    private IEnumerator FadeOutIn()
    {
        yield return StartCoroutine(FadeTo(1f)); 
        yield return new WaitForSeconds(0.2f);    
        yield return StartCoroutine(FadeTo(0f)); 
    }
    public void DialogueNPC(string Dialogue)
    {
        imageNPC.gameObject.SetActive(true);
        textNPC.text = Dialogue;
    }
    public void HideDialogue()
    {
        imageNPC.gameObject.SetActive(false);
    }
}
