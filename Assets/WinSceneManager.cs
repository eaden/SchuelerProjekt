using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneManager : MonoBehaviour
{
    float zaehler = 0f;
    bool ersterTeil = false;
    bool zweiterTeil = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        zaehler += Time.deltaTime;
        if(zaehler > 2 && !ersterTeil)
        {
            AudioManager.Instance.Play1("deineReiseIstJetztVorbei");
            ersterTeil = true;
        }
        if (zaehler > 6 &&!zweiterTeil)
        {
            AudioManager.Instance.Play1("tschueeess");
            zweiterTeil = true;
        }
    }
}
