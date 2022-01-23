using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteinSchereManager : MonoBehaviour
{
    public static SteinSchereManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public bool objektWurdeGewaehlt = false;
    public bool warteTimerLaeuft = true;
    string gewaehltesObjekt = "";

    // Variables
    GameObject computerAlien;
    GameObject computerSonne;
    GameObject computerPlanet;
    GameObject computerMiniAlien;
    GameObject computerMiniSonne;
    GameObject computerMiniPlanet;
    GameObject computerSpriteMask;
    BoxCollider2D clickAlien;
    CircleCollider2D clickSonne;
    CircleCollider2D clickPlanet;

    int computerAuswahl = 0;
    bool computerSneakpeak = false;

    bool clickablesAktiv = false;
    float warteTimer = 5f;

    void SetzeClickableAktiv()
    {
        clickAlien.enabled = true;
        clickSonne.enabled = true;
        clickPlanet.enabled = true;
        clickablesAktiv = true;
    }

    void SetzeClickableInaktiv()
    {
        clickAlien.enabled = false;
        clickSonne.enabled = false;
        clickPlanet.enabled = false;
        clickablesAktiv = false;
    }

    public void ObjektWahl(string name)
    {
        objektWurdeGewaehlt = true;
        gewaehltesObjekt = name;
    }

    void ComputerWaehlt()
    {
        computerAuswahl = Random.Range(0, 3);
        if (Random.Range(1, 6) > 1)
        {
            computerSpriteMask.transform.GetChild(computerAuswahl).gameObject.SetActive(true);
            computerSneakpeak = true;
        }
        switch (computerAuswahl)
        {
            case 0:
                // Alien
                break;
            case 1:
                // Sonne
                break;
            case 2:
                // Planet
                break;
            default:
                Debug.Log("Hier ist was falsch gegangen");
                break;
        }

    }

    void Start()
    {
        computerAlien = GameObject.Find("ComputerAlien");
        computerSonne = GameObject.Find("ComputerSonne"); ;
        computerPlanet = GameObject.Find("ComputerPlanet"); ;
        computerMiniAlien = GameObject.Find("ComputerMiniAlien");
        computerMiniSonne = GameObject.Find("ComputerMiniSonne");
        computerMiniPlanet = GameObject.Find("ComputerMiniPlanet");
        computerAlien.SetActive(false);
        computerSonne.SetActive(false);
        computerPlanet.SetActive(false);
        computerMiniAlien.SetActive(false);
        computerMiniSonne.SetActive(false);
        computerMiniPlanet.SetActive(false);
        clickAlien = GameObject.Find("ClickAlien").GetComponent<BoxCollider2D>();
        clickSonne = GameObject.Find("ClickSonne").GetComponent<CircleCollider2D>();
        clickPlanet = GameObject.Find("ClickPlanet").GetComponent<CircleCollider2D>();
        computerSpriteMask = GameObject.Find("ComputerSpriteMask"); 

        // Computerauswahl
        ComputerWaehlt();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Wartezeit am Anfang während Ansprache
        if(warteTimer > 0)
        {
            warteTimer -= Time.deltaTime;
        }
        else
        {
            if (!clickablesAktiv)
                SetzeClickableAktiv();
            if(objektWurdeGewaehlt)
            {
                SetzeClickableInaktiv();
                // Szene animieren
            }
        }
    }
}
