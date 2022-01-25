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

    public bool weitermachen = false;
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
        AudioManager.Instance.Play1("FuturEins");
    }

    // Update is called once per frame
    void Update()
    {
        if(weitermachen)
        {
            //AudioManager.Instance.Play1("FuturEins");
            if(!AudioManager.Instance.Source1StillPlaying())
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
