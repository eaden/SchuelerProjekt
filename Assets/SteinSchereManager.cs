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
    int gewaehltesObjektPlayer = 0;

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
    RotateScript rscript;
    MoveIntoDirection mscript;
    BoxCollider2D clickAlien;
    CircleCollider2D clickSonne;
    CircleCollider2D clickPlanet;

    bool shuffle = false;
    bool shuffleAbgeschlossen = false;
    int shuffleCounter = 0;
    float shuffleTimer = 0.7f;
    float shuffleTimerLimit = 0.7f;

    int computerAuswahl = 0;
    GameObject computerAuswahlObjekt;
    GameObject playerAuswahlObjekt;
    bool computerSneakpeak = false;

    bool clickablesAktiv = false;
    float warteTimer = 5f;
    bool auswahlPhase;

    void SetzeClickableAktiv()
    {
        clickAlien.enabled = true;
        clickSonne.enabled = true;
        clickPlanet.enabled = true;
        clickablesAktiv = true;
    }

   void SachenWeg()
    {
        // Rotation

    }

    void RotateSwitch()
    {
        rscript.istAmRotieren = !rscript.istAmRotieren;
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
        switch (name)
        {
            case "ClickAlien":
                playerAuswahlObjekt = clickAlien.gameObject;
                gewaehltesObjektPlayer = 0;
                break;
            case "ClickSonne":
                playerAuswahlObjekt = clickSonne.gameObject;
                gewaehltesObjektPlayer = 1;
                break;
            case "ClickPlanet":
                playerAuswahlObjekt = clickPlanet.gameObject;
                gewaehltesObjektPlayer = 2;
                break;
            default:
                Debug.Log("Something wrong");
                break;
        }
    }

    void ComputerShuffle()
    {
        int childs = derComputer.transform.childCount;
        if (shuffleCounter < 6)
        {
            derComputer.transform.GetChild((shuffleCounter + 2) % childs).gameObject.SetActive(false);
            derComputer.transform.GetChild((shuffleCounter) % childs).gameObject.SetActive(true);
        }
        else
        {
            derComputer.transform.GetChild((shuffleCounter + 2) % childs).gameObject.SetActive(false);
            derComputer.transform.GetChild(computerAuswahl % childs).gameObject.SetActive(true);
        }
            
        //derComputer.transform.GetChild((shuffleCounter + (childs - 1)) % childs).gameObject.SetActive(false);
        //derComputer.transform.GetChild(shuffleCounter % childs).gameObject.SetActive(true);
        shuffleCounter++;
        if (shuffleCounter > 6)
        {
            shuffle = false;
            shuffleCounter = 0;
            shuffleAbgeschlossen = true;
        }
    }

    void ComputerSneakPeakAus()
    {
        for (int i = 0; i < computerSpriteMask.transform.childCount; i++)
        {
            computerSpriteMask.transform.GetChild(i).gameObject.SetActive(false);
        }
        computerAugen.SetActive(false);
    }

    void ComputerWaehlt()
    {
        //computerAuswahl = Random.Range(0, 3);
        computerAuswahl = 0;
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
                computerAuswahlObjekt = computerAlien;
                // Alien
                break;
            case 1:
                computerAuswahlObjekt = computerSonne;
                // Sonne
                break;
            case 2:
                computerAuswahlObjekt = computerPlanet;
                // Planet
                break;
            default:
                Debug.Log("Hier ist was falsch gegangen");
                break;
        }
    }

    int SchauWerGewinnt()
    {
        if(gewaehltesObjektPlayer == computerAuswahl)
        {
            Debug.Log("Draw");
            return 0;
        }
        else if(computerAuswahl== ((gewaehltesObjektPlayer + 1)%3))
        {
            Debug.Log("Wir gewinnen");
            return 1;
        }
        else
        {
            Debug.Log("Computer gewinnt");
            return -1;
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
        rscript = clickAlien.transform.parent.GetComponent<RotateScript>();
        mscript = GetComponent<MoveIntoDirection>();

        computerSpriteMask = GameObject.Find("ComputerSpriteMask");
        computerAugen = GameObject.Find("ComputerAugen");
        computerAugen.SetActive(false);
        auswahlPhase = true;

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
            if (auswahlPhase)
            {
                if (!clickablesAktiv)
                    SetzeClickableAktiv();
                if (objektWurdeGewaehlt)
                {
                    auswahlPhase = false;
                    objektWurdeGewaehlt = false;
                    SetzeClickableInaktiv();
                    ComputerSneakPeakAus();
                    RotateSwitch();
                    shuffle = true;
                }
            }
            if (shuffle)
            {
                shuffleTimer -= Time.deltaTime;
                if (shuffleTimer < 0)
                {
                    ComputerShuffle();
                    shuffleTimer = shuffleTimerLimit;
                }
            }
            if (shuffleAbgeschlossen)
            {
                shuffleAbgeschlossen = false;
                int wer = SchauWerGewinnt();

                if (wer < 0)
                {
                    mscript.LetsMove(computerAuswahlObjekt, playerAuswahlObjekt);
                }
                else if (wer > 0)
                {
                    mscript.LetsMove(playerAuswahlObjekt, computerAuswahlObjekt);
                }
            }
        }

    }
}
