using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorkAroundFadeInOut : MonoBehaviour
{
    Image im;
    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var tempColor = im.color;
        tempColor.a = FadeInOut.Instance.alphaAktuell;
        im.color = tempColor;
        
    }
}
