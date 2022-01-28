using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut Instance;
    private void Awake()
    {
        Instance = this;
    }

    //public float alpha = 1f;
    //public float v = 1f;

    public float alphaAktuell = 0f;

    SpriteRenderer sr;
    public bool ganzSchwarz = false;
    public bool ganzDurchsichtig = false;

    public void FadeBlackIn(float fadeTime)
    {
        StartCoroutine(FadeIn(fadeTime));
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut(2f));
        //StartCoroutine(FadeTo(0, 2f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = sr.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            alphaAktuell = Mathf.Lerp(alpha, aValue, t);
            Color newColor = new Color(1, 1, 1, alphaAktuell);
            sr.material.color = newColor;
            yield return null;
        }
    }

    IEnumerator FadeIn(float aTime)
    {

        float alpha = sr.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            alphaAktuell = Mathf.Lerp(alpha, 1, t);
            Color newColor = new Color(1, 1, 1, alphaAktuell);
            if (newColor.a > 0.95)
            {
                newColor.a = 1;
                ganzSchwarz = true;
            }
            sr.material.color = newColor;
            yield return null;
        }
    }

    IEnumerator FadeOut(float aTime)
    {
        float alpha = sr.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            alphaAktuell = Mathf.Lerp(alpha, 0, t);
            Color newColor = new Color(1, 1, 1, alphaAktuell);
            if (newColor.a < 0.05)
            {
                newColor.a = 0;
                ganzDurchsichtig = true;
            }  
            sr.material.color = newColor;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
