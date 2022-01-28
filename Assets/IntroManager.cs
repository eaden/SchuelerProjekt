using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public static IntroManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public bool ersteSzene = false;
    public bool teilnehmendeSzene = false;
    public bool weitermachen = false;
    bool futurEinsSpielen = false;
    bool fadeStart1 = false;
    void Start()
    {

        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.levelFortschritt == 0)
            {
                GameObject.Find("Button_Weitermachen").SetActive(false);
            }
        }
        AudioManager.Instance.Stop3();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FadeInOut.Instance.ganzDurchsichtig)
        {
            if(!futurEinsSpielen)
            {
                AudioManager.Instance.Play1("FuturEins");
                futurEinsSpielen = true;
            }
        }
        if(ersteSzene)
        {
            if (!fadeStart1)
            {
                FadeInOut.Instance.FadeBlackIn(2f);
                fadeStart1 = true;
            }
            if (!AudioManager.Instance.Source1StillPlaying() && FadeInOut.Instance.ganzSchwarz)
            {
                SceneManager.LoadScene("ZSStart");
            }
        }
        if(teilnehmendeSzene)
        {
            if (!fadeStart1)
            {
                FadeInOut.Instance.FadeBlackIn(2f);
                fadeStart1 = true;
            }
            if (FadeInOut.Instance.ganzSchwarz)
            {
                SceneManager.LoadScene("Teilnehmende");
            }
        }
        if (weitermachen)
        {
            if(!fadeStart1)
            {
                FadeInOut.Instance.FadeBlackIn(2f);
                fadeStart1 = true;
            }
            if(!AudioManager.Instance.Source1StillPlaying() && FadeInOut.Instance.ganzSchwarz)
            {
                int i = GameManager.Instance.levelFortschritt;
                switch (i)
                {
                    case 1:
                        SceneManager.LoadScene("SteinSchereScene");
                        break;
                    case 2:
                        SceneManager.LoadScene("Level1");
                        break;
                    case 3:
                        SceneManager.LoadScene("AnklickenSpiel");
                        break;
                    default:
                        Debug.Log("Hier war was falsch");
                        break;
                }
            }

        }
    }
}
