using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zahlenZaehler : MonoBehaviour
{
    ClickSceneManager csm;
    public Sprite[] spriteSammlung = new Sprite[10];
    private SpriteRenderer zehnerZaehlerRenderer;
    private SpriteRenderer einerZaehlerRenderer;
    private int aktuelleZahl;
    private int startZahl = 15;
    private float sekundenZaehler = 1f;
    private bool geschafft = false;
    void Start()
    {
        aktuelleZahl = startZahl;
        csm = ClickSceneManager.Instance;
        einerZaehlerRenderer = GetComponent<SpriteRenderer>();
        zehnerZaehlerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Reset()
    {
        aktuelleZahl = startZahl;
    }
    public void Geschafft()
    {
        geschafft = true;
    }



    // Update is called once per frame
    void Update()
    {
        if(!geschafft)
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
                // bei 0 muss was passieren
            }
        }
    }

}
