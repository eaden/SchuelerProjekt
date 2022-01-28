using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zahlenZahlerZSStart : MonoBehaviour
{
    public Sprite[] spriteSammlung = new Sprite[10];
    private SpriteRenderer zehnerZaehlerRenderer;
    private SpriteRenderer einerZaehlerRenderer;
    private int aktuelleZahl = 12;
    private float sekundenZaehler = 1f;
    GameObject RaketeOhneFeuer = null;
    GameObject RaketeMitFeuer = null;
    bool feuerAktiviert = false;
    bool zuendungAktiviert = false;
    bool sorgeAktiviert = false;
    bool ausmachenAktiviert = false;
    float ausmachenTimer = 3f;
    bool szenenWechsel = false;
    int zweiteZahl = 0;
    bool anfangsPhase = true;
    bool hintergrundMusikAnmachen = false;



    // phasen
    bool feuerPhase = false;
    bool fehltPhase = false;
    void Start()
    {
        einerZaehlerRenderer = GetComponent<SpriteRenderer>();
        zehnerZaehlerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        RaketeOhneFeuer = GameObject.Find("RaketeOhneFeuer");
        RaketeMitFeuer = GameObject.Find("RaketeMitFeuer");
        RaketeMitFeuer.SetActive(false);
        AudioManager.Instance.SetLoop3(false);

    }



    // Update is called once per frame
    void Update()
    {
        if(anfangsPhase && !AudioManager.Instance.Source1StillPlaying())
        {
            if(!hintergrundMusikAnmachen)
            {
                AudioManager.Instance.Play3("gesangVerzerrt");
                hintergrundMusikAnmachen = true;
                anfangsPhase = false;
            }
        }
        if (aktuelleZahl > 0)
        {
            // runterzaehlen
            sekundenZaehler -= Time.deltaTime;
            if (sekundenZaehler < 0)
            {
                sekundenZaehler = 1f;
                aktuelleZahl--;
            }
            if (aktuelleZahl > 9)
            {
                zehnerZaehlerRenderer.enabled = true;
                zehnerZaehlerRenderer.sprite = spriteSammlung[1];
            }
            else
            {
                zehnerZaehlerRenderer.enabled = false;
            }
            einerZaehlerRenderer.sprite = spriteSammlung[aktuelleZahl % 10];
        }
        else
        {
            if(!feuerAktiviert)
            {
                // Rakete mit Feuer aktivieren + Feuersound abspielen
                RaketeOhneFeuer.SetActive(false);
                RaketeMitFeuer.SetActive(true);
                AudioManager.Instance.SetLoop2(true);
                AudioManager.Instance.Play2("raketenZuendung");
                feuerAktiviert = true;
                feuerPhase = true;
            }
            sekundenZaehler -= Time.deltaTime;
            if (sekundenZaehler < 0)
            {
                sekundenZaehler = 1f;
                zweiteZahl++;
            }
            if(zweiteZahl == 2)
            {
                if(!zuendungAktiviert)
                {
                    AudioManager.Instance.Play1("OKAYDieRaketezuendet");
                    zuendungAktiviert = true;
                }               
            }
            if(feuerPhase)
            {
                if(zweiteZahl == 5)
                {
                    if (!sorgeAktiviert)
                    {
                        AudioManager.Instance.Play1("aberEtwasFehltNoch");
                        feuerPhase = false;
                        fehltPhase = true;
                        sorgeAktiviert = true;
                    }    
                }
            }
            if(fehltPhase)
            {
                if(!AudioManager.Instance.Source1StillPlaying())
                {
                    if(!ausmachenAktiviert)
                    {
                        ausmachenAktiviert = true;
                    }

                }
            }
            if(ausmachenAktiviert)
            {
                ausmachenTimer -= Time.deltaTime;
                if(ausmachenTimer < 0)
                {
                    if (szenenWechsel && FadeInOut.Instance.ganzSchwarz)
                    {
                        AudioManager.Instance.SetLoop2(false);
                        AudioManager.Instance.SetLoop3(true);
                        AudioManager.Instance.Stop2();
                        SceneManager.LoadScene("SteinSchereScene");
                        szenenWechsel = true;
                    }
                    if (!szenenWechsel)
                    {
                        FadeInOut.Instance.FadeBlackIn(2f);
                        szenenWechsel = true;
                    }
                }
            }
        }
    }
}
