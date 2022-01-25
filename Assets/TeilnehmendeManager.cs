using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeilnehmendeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SetLoop3(true);
        AudioManager.Instance.Play3("Beatbox");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
