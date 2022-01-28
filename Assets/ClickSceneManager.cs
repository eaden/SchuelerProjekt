using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickSceneManager : MonoBehaviour
{
    public static ClickSceneManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Variables
    GameObject szene1;
    GameObject szene2;
    GameObject szene3;
    GameObject aktuelleSzene;
    int aktuelleSzeneIndex = 1;
    List<GameObject> moeglicheZiele = new List<GameObject>();
    int ausgewaehltesZiel;
    GameObject anzeigeFenster;
    zahlenZaehler zaehlerScript;

    bool anfangsPhase = true;
    float anfangsTimer = 2f;

    bool spielGewonnen = false;
    bool gewonnenUebergang = false;

    public bool spielVerloren = false;
    bool verlorenSetzen = false;
    bool beendet = false;
    float verlorenTimer = 5f;

    bool fadeStart = false;
    bool fadeStart2 = false;

    public void NextClickObject()
    {
        aktuelleSzeneIndex++;
        switch (aktuelleSzeneIndex)
        {
            case 2:
                AudioManager.Instance.Play1("BINGNaechsteRunde");
                SzeneAktivSetzen(szene2);
                zaehlerScript.Reset();
                break;
            case 3:
                AudioManager.Instance.Play1("BINGRichtig");
                SzeneAktivSetzen(szene3);
                zaehlerScript.Reset();
                break;
            case 4:
                AudioManager.Instance.Play1("wowAllesFertig");
                zaehlerScript.Geschafft();
                spielGewonnen = true;
                Debug.Log("Geschafft");
                break;
            default:
                Debug.Log("Hier ist was falsch gegangen");
                break;
        }
    }
    void SzeneAktivSetzen(GameObject szene)
    {
        // alte Szene inaktiv setzen
        aktuelleSzene.SetActive(false);
        moeglicheZiele.Clear();
        // Anzeigefenster aendern
        //Debug.Log(anzeigeFenster.transform.childCount);
        //Debug.Log(aktuelleSzeneIndex);
        for (int i = 0; i < anzeigeFenster.transform.childCount; i++)
        {
            if ((aktuelleSzeneIndex - 1) == i)
                anzeigeFenster.transform.GetChild(i).gameObject.SetActive(true);
            else
                anzeigeFenster.transform.GetChild(i).gameObject.SetActive(false);
            
        }
        aktuelleSzene = szene;
        szene.SetActive(true);
        GameObject suchObjekte = null;
        for (int i = 0; i < szene.transform.childCount; i++)
        {
            if (szene.transform.GetChild(i).gameObject.name == "GesuchteObjekte")
                suchObjekte = szene.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < suchObjekte.transform.childCount; i++)
        {
            GameObject aktuellesKind = suchObjekte.transform.GetChild(i).gameObject;
            if (aktuellesKind.CompareTag("SearchedItem"))
                moeglicheZiele.Add(aktuellesKind);
        }
        ausgewaehltesZiel = Random.Range(0, moeglicheZiele.Count);
        for (int i = 0; i < moeglicheZiele.Count; i++)
        {
            if (i == ausgewaehltesZiel)
                continue;
            else
                moeglicheZiele[i].SetActive(false);
        }
    }

    void Start()
    {
        GameManager.Instance.levelFortschritt = 3;

        //Methode, um die richtigen Objekte für die nächste Versteckszene zu laden
        //Methode um neues Objekt random zu setzen
        anzeigeFenster = GameObject.Find("anzeigeFenster");
        zaehlerScript = GameObject.Find("zaehlerObjekt").GetComponent<zahlenZaehler>();
        szene1 = GameObject.Find("SuchSzene1");
        szene2 = GameObject.Find("SuchSzene2");
        szene3 = GameObject.Find("SuchSzene3");
        szene1.SetActive(true);
        szene2.SetActive(false);
        szene3.SetActive(false);
        aktuelleSzene = szene1;
        SzeneAktivSetzen(szene1);

        AudioManager.Instance.Play1("dieRichtigenTeileObenImKasten");
        AudioManager.Instance.Stop3();
        AudioManager.Instance.SetLoop3(true);
        AudioManager.Instance.Play3("gesangEtwasLaenger");
    }

    void Update()
    {
        if(spielGewonnen)
        {
            if(!fadeStart)
            {
                FadeInOut.Instance.FadeBlackIn(2f);
                fadeStart = true;
            }
            if(!AudioManager.Instance.Source1StillPlaying() && FadeInOut.Instance.ganzSchwarz)
            {
                if(!gewonnenUebergang)
                {
                    AudioManager.Instance.Stop3();
                    AudioManager.Instance.SetLoop3(false);
                    SceneManager.LoadScene("WinScene");
                    gewonnenUebergang = true;
                }
            }
        }
        if(spielVerloren)
        {
            if(!verlorenSetzen)
            {
                for (int i = 0; i < moeglicheZiele.Count; i++)
                    moeglicheZiele[i].GetComponent<BoxCollider2D>().enabled = false;
                verlorenSetzen = true;
            }
            verlorenTimer -= Time.deltaTime;
            if(verlorenTimer < 2.5f)
            {
                if(!fadeStart2)
                {
                    FadeInOut.Instance.FadeBlackIn(2f);
                    fadeStart2 = true;
                }
            }
            if(verlorenTimer < 0)
            {
                if(!beendet)
                {
                    SceneManager.LoadScene("IntroScene");
                    beendet = true;
                }
            }
        }
        if(anfangsPhase)
        {
            if(!AudioManager.Instance.Source1StillPlaying())
            {
                anfangsTimer -= Time.deltaTime;
                if(anfangsTimer < 0)
                {
                    AudioManager.Instance.Play2("schnellBeeilDich");
                    anfangsPhase = false;
                }
            }
        }

    }
}
