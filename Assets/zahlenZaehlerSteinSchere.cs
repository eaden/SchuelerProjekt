using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zahlenZaehlerSteinSchere : MonoBehaviour
{
    public Sprite[] spriteSammlung = new Sprite[10];
    private SpriteRenderer einerZaehlerRenderer;
    private int aktuelleZahl = 0;
    private float sekundenZaehler = 1f;
    //GameObject RaketeOhneFeuer = null;
    //GameObject RaketeMitFeuer = null;
    void Start()
    {
        einerZaehlerRenderer = GetComponent<SpriteRenderer>();
        einerZaehlerRenderer.sprite = spriteSammlung[aktuelleZahl];
        //RaketeOhneFeuer = GameObject.Find("RaketeOhneFeuer");
        //RaketeMitFeuer = GameObject.Find("RaketeMitFeuer");
    }

    public void PunktGemacht()
    {
        aktuelleZahl++;
        einerZaehlerRenderer.sprite = spriteSammlung[aktuelleZahl];
        if (aktuelleZahl > 5 && gameObject.CompareTag("Player"))
        {
            // Gewonnen
        }
    }



    // Update is called once per frame
    void Update()
    {
        /*
        if (aktuelleZahl > 0)
        {
            einerZaehlerRenderer.sprite = spriteSammlung[aktuelleZahl % 10];
        }
        else
        {

        }
        */
    }
}
