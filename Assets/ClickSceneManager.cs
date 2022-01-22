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
    public GameObject[] clickObjectSprites = new GameObject[3];
    private int clickObjectIndex = 0;
    public void NextClickObject()
    {
        if(clickObjectIndex < clickObjectSprites.Length-1)
        {
            //Methode, um die richtigen Objekte für die nächste Versteckszene zu laden
            //Methode um neues Objekt random zu setzen
        }
    }

    void Start()
    {
        //Methode, um die richtigen Objekte für die nächste Versteckszene zu laden
        //Methode um neues Objekt random zu setzen
    }

    void Update()
    {
        
    }
}
