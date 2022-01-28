using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Weitermachen_Changelevel()
    {
        if(GameManager.Instance!=null && !AudioManager.Instance.Source1StillPlaying())
        {
            AudioManager.Instance.Play1("vielSpassBeiDerNaechstenRunde");
            IntroManager.Instance.weitermachen = true;
        }

    }
    public void Teilnehmende_Changelevel()
    {
        if (!IntroManager.Instance.weitermachen)
            IntroManager.Instance.teilnehmendeSzene = true;
    }
    
    public void ChangeSceneToFirstLevel()
    {
        if (!IntroManager.Instance.weitermachen)
        {
            IntroManager.Instance.ersteSzene = true;
            GameManager.Instance.levelFortschritt = 0;
            AudioManager.Instance.Play1("lassUnsZumMarsReisen");
        }
            
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
