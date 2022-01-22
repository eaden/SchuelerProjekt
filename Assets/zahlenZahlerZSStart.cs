using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zahlenZahlerZSStart : MonoBehaviour
{
    public Sprite[] spriteSammlung = new Sprite[10];
    private SpriteRenderer zehnerZaehlerRenderer;
    private SpriteRenderer einerZaehlerRenderer;
    private int aktuelleZahl = 12;
    private float sekundenZaehler = 1f;
    GameObject RaketeOhneFeuer = null;
    GameObject RaketeMitFeuer = null;
    void Start()
    {
        einerZaehlerRenderer = GetComponent<SpriteRenderer>();
        zehnerZaehlerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        RaketeOhneFeuer = GameObject.Find("RaketeOhneFeuer");
        RaketeMitFeuer = GameObject.Find("RaketeMitFeuer");
        RaketeMitFeuer.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
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
            // Rakete mit Feuer aktivieren + Feuersound abspielen
            RaketeOhneFeuer.SetActive(false);
            RaketeMitFeuer.SetActive(true);
        }
    }
}
