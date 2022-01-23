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

    // Variables
    GameObject computerAlien;
    GameObject computerSonne;
    GameObject computerPlanet;
    GameObject computerMiniAlien;
    GameObject computerMiniSonne;
    GameObject computerMiniPlanet;

    float warteTimer = 5f;

    public void ObjektWahl(string name)
    {
        objektWurdeGewaehlt = true;
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
            if(objektWurdeGewaehlt)
            {

            }
        }
    }
}
