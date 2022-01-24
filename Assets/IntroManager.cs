using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public static IntroManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.levelFortschritt == 0)
            {
                GameObject.Find("Button_Weitermachen").SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
