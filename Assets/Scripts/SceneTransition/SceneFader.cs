using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup loadingScreen;
    public AnimationCurve curve;
    public static SceneFader instance;
    private float speed = 0.7f;
    [SerializeField] private GameObject fader;

    private void Start()
    {
        instance = this;
        fader.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fader.TryGetComponent<Image>(out Image faderImg);
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime * speed;
            float a = curve.Evaluate(t);
            faderImg.color = new Color(faderImg.color.r, faderImg.color.g, faderImg.color.b, a);
            yield return 0;
        }
    }

    public IEnumerator Fadeout()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            float a = curve.Evaluate(t);
            loadingScreen.alpha = a;
            yield return 0;
        }
    }
}
