using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZSAbflugscript : MonoBehaviour
{
    float timer = 0;
    bool changeSzene = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SwitchLoop2();
        AudioManager.Instance.Play2("fff");
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * 10);
        if(transform.position.x > 10f)
        {
            if(!changeSzene)
            {
                AudioManager.Instance.Stop2();
                AudioManager.Instance.SwitchLoop2();
                changeSzene = true;
                AudioManager.Instance.Play1("achtungImmerAusweichen");
                SceneManager.LoadScene("Level1");
            }
        }
        /*
        if (gameObject.transform.localScale.x < 10f)
        {
            timer = (Time.deltaTime);
            gameObject.transform.localScale += new Vector3(timer, timer, timer);
            //gameObject.transform.Translate(Vector3.right * Time.deltaTime/2);
            if (gameObject.transform.localScale.x > 5f)
            {
                gameObject.transform.Translate(Vector3.right * Time.deltaTime*2);
            }
        }
        else
        {
            
        }
        */
    }
}
