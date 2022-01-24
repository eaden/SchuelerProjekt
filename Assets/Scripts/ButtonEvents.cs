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

    public void Weitermachen_ChangeToLastScene()
    {
        SceneManager.LoadScene("Level1");
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
