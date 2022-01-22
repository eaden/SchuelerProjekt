using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void NextClickObject()
    {
        aktuelleSzeneIndex++;
        switch (aktuelleSzeneIndex)
        {
            case 2:
                SzeneAktivSetzen(szene2);
                break;
            case 3:
                SzeneAktivSetzen(szene3);
                break;
            default:
                Debug.Log("Geschafft");
                break;
        }
    }
    void SzeneAktivSetzen(GameObject szene)
    {
        // alte Szene inaktiv setzen
        aktuelleSzene.SetActive(false);
        moeglicheZiele.Clear();
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

        //Methode, um die richtigen Objekte für die nächste Versteckszene zu laden
        //Methode um neues Objekt random zu setzen
        szene1 = GameObject.Find("SuchSzene1");
        szene2 = GameObject.Find("SuchSzene2");
        szene3 = GameObject.Find("SuchSzene3");
        szene1.SetActive(true);
        szene2.SetActive(false);
        szene3.SetActive(false);
        aktuelleSzene = szene1;
        SzeneAktivSetzen(szene1);
    }

    void Update()
    {
        
    }
}
