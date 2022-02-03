using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    float zaehler = 0f;
    bool ersterTeil = false;
    bool zweiterTeil = false;
    bool fadeStart1 = false;
    bool szenenWechsel = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Stop3();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeStart1 && FadeInOut.Instance.ganzSchwarz)
        {
            if(!szenenWechsel)
            {
                SceneManager.LoadScene("Teilnehmende");
                szenenWechsel = true;
            }
        }
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
        if (zaehler > 11 && !fadeStart1)
        {
            FadeInOut.Instance.FadeBlackIn(2f);
            fadeStart1 = true;
        }
    }
}
