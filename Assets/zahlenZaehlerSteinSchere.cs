using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zahlenZaehlerSteinSchere : MonoBehaviour
{
    public Sprite[] spriteSammlung = new Sprite[10];
    private SpriteRenderer einerZaehlerRenderer;
    private int aktuelleZahl = 0;
    private float sekundenZaehler = 1f;
    void Start()
    {
        einerZaehlerRenderer = GetComponent<SpriteRenderer>();
        einerZaehlerRenderer.sprite = spriteSammlung[aktuelleZahl];
    }

    public void PunktGemacht()
    {
        aktuelleZahl++;
        einerZaehlerRenderer.sprite = spriteSammlung[aktuelleZahl]; 
        if(gameObject.CompareTag("Player"))
        {
            if(aktuelleZahl > 3)
            {
                AudioManager.Instance.Play1("yeahGeschafft");
                SceneManager.LoadScene("ZSAbflug");
            }
            else
            {
                AudioManager.Instance.Play2("JA");
            }
        }
        else
        {
            if (aktuelleZahl > 3)
            {
                // Verloren
                AudioManager.Instance.Play1("explosionEtwasLaengerUndLeise");
                SteinSchereManager.Instance.spielVerloren = true;
            }
            else
            {
                AudioManager.Instance.Play2("schade");
            }
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
