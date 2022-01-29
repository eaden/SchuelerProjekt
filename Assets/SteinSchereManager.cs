using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteinSchereManager : MonoBehaviour
{
    public static SteinSchereManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public bool objektWurdeGewaehlt = false;
    public bool warteTimerLaeuft = true;
    public bool moveAnimationFertig = false;
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
    zahlenZaehlerSteinSchere zaehlerSpieler; 
    zahlenZaehlerSteinSchere zaehlerComputer;

    bool anfangsPhase = false;
    bool shuffle = false;
    bool shuffleAbgeschlossen = false;
    int shuffleCounter = 0;
    float shuffleTimer = 0.7f;
    float shuffleTimerLimit = 0.7f;

    int computerAuswahl = 0;
    GameObject computerAuswahlObjekt;
    GameObject playerAuswahlObjekt;
    bool computerSneakpeak = false;
    int aktuellerGewinner = 0;
    

    bool clickablesAktiv = false;
    float warteTimer = 6f;
    bool auswahlPhase;
    public bool spielVerloren = false;
    float verlorenTimer = 5f;

    public bool spielGewonnen = false;

    bool fadeStart1 = false;
    bool fadeStart2 = false;
    bool szenenWechsel = false;
    bool computerAugenSound = false;
    float computerAugenSoundTimer = 1f;
    float computerAugenSoundTimerLimit = 1f;

    // music
    bool gluecksspiel = false;

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
                AudioManager.Instance.Play2("ssp_alien");
                break;
            case "ClickSonne":
                playerAuswahlObjekt = clickSonne.gameObject;
                gewaehltesObjektPlayer = 1;
                AudioManager.Instance.Play2("ssp_sonne");
                break;
            case "ClickPlanet":
                playerAuswahlObjekt = clickPlanet.gameObject;
                gewaehltesObjektPlayer = 2;
                AudioManager.Instance.Play2("ssp_planet");
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
            derComputer.transform.GetChild(shuffleCounter % childs).gameObject.SetActive(true);
        }
        else
        {
            if (shuffleCounter == 6)
                derComputer.transform.GetChild((shuffleCounter + 2) % childs).gameObject.SetActive(false);
            //derComputer.transform.GetChild(computerAuswahl % childs).gameObject.SetActive(true);
            else
                if(shuffleCounter == 7)
                    computerAuswahlObjekt.SetActive(true);
            // auf 8 ist nichts, da ist einfach wait
        }

        //derComputer.transform.GetChild((shuffleCounter + (childs - 1)) % childs).gameObject.SetActive(false);
        //derComputer.transform.GetChild(shuffleCounter % childs).gameObject.SetActive(true);
        shuffleCounter++;
        if (shuffleCounter > 8)
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
        computerAugenSound = false;
    }

    void ComputerAuswahlAus()
    {
        computerAuswahlObjekt.SetActive(false);
    }

    void ComputerWaehlt()
    {
        computerAuswahl = Random.Range(0, 3);
        if (Random.Range(1, 6) > 1)
        {
            computerSneakpeak = true;
            // Change den richtigen anzuzeigen
            int showRightOne = Random.Range(1, 11);
            if(showRightOne > 4)
            {
                computerSpriteMask.transform.GetChild(computerAuswahl).gameObject.SetActive(true);
            }
            else
            {
                // darauf achten, dass wenn die Augen roten sind, auch immer das falsche angezeigt wird
                computerSpriteMask.transform.GetChild((computerAuswahl+ Random.Range(1,3)) % 3).gameObject.SetActive(true);
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
            AudioManager.Instance.Play2("nochmal");
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
        GameManager.Instance.levelFortschritt = 1;

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
 
        zaehlerSpieler = GameObject.Find("SpielerZaehler").GetComponent<zahlenZaehlerSteinSchere>();
        zaehlerComputer = GameObject.Find("GegnerZaehler").GetComponent<zahlenZaehlerSteinSchere>();

        computerSpriteMask = GameObject.Find("ComputerSpriteMask");
        computerAugen = GameObject.Find("ComputerAugen");
        computerAugen.SetActive(false);
        anfangsPhase = true;
        auswahlPhase = true;
        AudioManager.Instance.Play1("computerProgrammieren");
        // Computerauswahl
        ComputerWaehlt();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spielGewonnen)
        {
            if(!fadeStart2)
            {
                FadeInOut.Instance.FadeBlackIn(2f);
                fadeStart2 = true;
            }
            if(FadeInOut.Instance.ganzSchwarz && !szenenWechsel)
            {
                SceneManager.LoadScene("ZSAbflug");
                szenenWechsel = true;
            }
        }
        if(spielVerloren)
        {
            verlorenTimer -= Time.deltaTime;
            if(verlorenTimer < 2.5f)
            {
                if(!fadeStart1)
                {
                    FadeInOut.Instance.FadeBlackIn(2f);
                    fadeStart1 = true;
                }
            }
            if(verlorenTimer < 0)
                SceneManager.LoadScene("IntroScene");
        }
        // Wartezeit am Anfang während Ansprache
        if(warteTimer > 0)
        {
            warteTimer -= Time.deltaTime;
            if(warteTimer < 3)
            {
                if (gluecksspiel && !AudioManager.Instance.Source1StillPlaying())
                {
                    warteTimer = -1f;
                    AudioManager.Instance.SetLoop3(true);
                    AudioManager.Instance.Play3("gesangSummen");
                }

                if (!gluecksspiel)
                {
                    AudioManager.Instance.Play1("richtigesGluecksspiel");
                    gluecksspiel = true;
                }
            }
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
                aktuellerGewinner = SchauWerGewinnt();

                if (aktuellerGewinner < 0)
                {
                    mscript.LetsMove(computerAuswahlObjekt, playerAuswahlObjekt);
                }
                else if (aktuellerGewinner > 0)
                {
                    mscript.LetsMove(playerAuswahlObjekt, computerAuswahlObjekt);
                }
                else
                {
                    // Draw
                    moveAnimationFertig = true;
                }
            }
            if(moveAnimationFertig)
            {
                moveAnimationFertig = false;
                if (aktuellerGewinner < 0)
                {
                    zaehlerComputer.PunktGemacht();
                }
                else if (aktuellerGewinner > 0)
                {
                    zaehlerSpieler.PunktGemacht();
                }
                RotateSwitch();
                ComputerAuswahlAus();
                auswahlPhase = true;
                ComputerWaehlt();

            }
            if(computerAugen.activeInHierarchy && !computerAugenSound && AudioManager.Instance.Source2StillPlaying())
            {
                computerAugenSoundTimer -= Time.deltaTime;
                if(computerAugenSoundTimer < 0)
                {
                    AudioManager.Instance.Play1("ssp_ohoh");
                    computerAugenSoundTimer = computerAugenSoundTimerLimit;
                    computerAugenSound = true;
                }
                
            }
        }

    }
}
