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
        if(GameManager.Instance!=null)
        {
            AudioManager.Instance.Play1("vielSpassBeiDerNaechstenRunde");
            IntroManager.Instance.weitermachen = true;
        }

    }
    public void Teilnehmende_Changelevel()
    {
        SceneManager.LoadScene("Teilnehmende");
    }
    
    public void ChangeSceneToFirstLevel()
    {
        AudioManager.Instance.Play1("lassUnsZumMarsReisen");
        SceneManager.LoadScene("ZSStart");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
