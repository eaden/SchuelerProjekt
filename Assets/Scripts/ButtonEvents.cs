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
            int i = GameManager.Instance.levelFortschritt;
            switch (i)
            {
                case 1:
                    SceneManager.LoadScene("SteinSchereScene");
                    break;
                case 2:
                    SceneManager.LoadScene("ZSStart");
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
    public void Teilnehmende_Changelevel()
    {
        SceneManager.LoadScene("Teilnehmende");
    }
    
    public void ChangeSceneToFirstLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
