using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZSAbflugscript : MonoBehaviour
{
    bool changeSzene = false;
    void Start()
    {
        AudioManager.Instance.Stop1();
        AudioManager.Instance.Stop2();
        AudioManager.Instance.Stop3();
        AudioManager.Instance.SetLoop2(true);
        AudioManager.Instance.Play2("fff");
        
    }
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * 10);
        if(transform.position.x > 10f)
        {
            if(!changeSzene)
            {
                AudioManager.Instance.Stop2();
                AudioManager.Instance.SetLoop2(false);
                changeSzene = true;
                AudioManager.Instance.Play1("achtungImmerAusweichen");
                SceneManager.LoadScene("Level1");
            }
        }
    }
}
