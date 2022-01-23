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
    GameObject derComputer;
    GameObject computerAlien;
    GameObject computerSonne;
    GameObject computerPlanet;
    GameObject computerMiniAlien;
    GameObject computerMiniSonne;
    GameObject computerMiniPlanet;
    GameObject computerSpriteMask;
    GameObject computerAugen;
    BoxCollider2D clickAlien;
    CircleCollider2D clickSonne;
    CircleCollider2D clickPlanet;

    bool shuffle = false;
    int shuffleCounter = 0;
    float shuffleTimer = 0.7f;
    float shuffleTimerLimit = 0.7f;

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

    void ComputerShuffle()
    {
        int childs = derComputer.transform.childCount;
        derComputer.transform.GetChild((shuffleCounter+2) % childs).gameObject.SetActive(false);
        derComputer.transform.GetChild((shuffleCounter) % childs).gameObject.SetActive(true);
        //derComputer.transform.GetChild((shuffleCounter + (childs - 1)) % childs).gameObject.SetActive(false);
        //derComputer.transform.GetChild(shuffleCounter % childs).gameObject.SetActive(true);
        shuffleCounter++;
        if (shuffleCounter > 5)
        {
            shuffle = false;
            shuffleCounter = 0;
        }
    }

    void ComputerWaehlt()
    {
        computerAuswahl = Random.Range(0, 3);
        if (Random.Range(1, 6) > 1)
        {
            computerSneakpeak = true;
            // Change den richtigen anzuzeigen
            int showRightOne = Random.Range(1, 11);
            if(showRightOne > 2)
            {
                computerSpriteMask.transform.GetChild(computerAuswahl).gameObject.SetActive(true);
            }
            else
            {
                computerSpriteMask.transform.GetChild((computerAuswahl+ showRightOne) % 3).gameObject.SetActive(true);
                computerAugen.SetActive(true);
            }
                
            
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
        derComputer = GameObject.Find("DerComputer");
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
        computerAugen = GameObject.Find("ComputerAugen");
        computerAugen.SetActive(false);

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
                objektWurdeGewaehlt = false;
                SetzeClickableInaktiv();
                shuffle = true;
            }
            if(shuffle)
            {
                shuffleTimer -= Time.deltaTime;
                if(shuffleTimer < 0)
                {
                    ComputerShuffle();
                    shuffleTimer = shuffleTimerLimit;
                }
            }
        }
    }
}
