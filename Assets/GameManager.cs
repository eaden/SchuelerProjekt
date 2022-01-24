 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int levelFortschritt = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
            
        DontDestroyOnLoad(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // Audiotest
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.Play("oui");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.Instance.Play2("gesang1");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.Stop1();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.Stop2();
        }
        */
    }
}
