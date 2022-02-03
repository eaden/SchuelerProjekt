using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeilnehmendeManager : MonoBehaviour
{
    public static TeilnehmendeManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public bool szeneVerlassen = false;
    bool szenenWechsel = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SetLoop3(true);
        AudioManager.Instance.Play3("Beatbox");
    }

    // Update is called once per frame
    void Update()
    {
        if(szeneVerlassen && FadeInOut.Instance.ganzSchwarz)
        {
            if(!szenenWechsel)
            {
                SceneManager.LoadScene("IntroScene");
                szenenWechsel = true;
            }
        }
    }
}
