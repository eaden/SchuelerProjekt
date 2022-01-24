using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    [HideInInspector]
    public AudioSource mySource1;

    [HideInInspector]
    public AudioSource mySource2;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(Instance);
        /*
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source2 = gameObject.AddComponent<AudioSource>();
            s.source2.clip = s.clip;
            s.source2.volume = s.volume;
        }
        */
        mySource1 = gameObject.AddComponent<AudioSource>();
        mySource2 = gameObject.AddComponent<AudioSource>();
    }

    public bool Source1StillPlaying()
    {
        return mySource1.isPlaying;
    }
    public bool Source2StillPlaying()
    {
        return mySource2.isPlaying;
    }
    public void Stop1()
    {
        if(mySource1.isPlaying)
        {
            mySource1.Stop();
        }
    }
    public void Stop2()
    {
        if (mySource2.isPlaying)
        {
            mySource2.Stop();
        }
    }

    public void Play1(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sounds with name" + name + "not found");
            return;
        }
        mySource1.clip = s.clip;
        mySource1.volume = s.volume;
        mySource1.Play();
    }
    public void Play2(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sounds with name" + name + "not found");
            return;
        }
        mySource2.clip = s.clip;
        mySource2.volume = s.volume;
        mySource2.Play();
    }
    /*
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sounds with name" + name + "not found");
            return;
        }
        s.source.Play();
    }
    public void Play2(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sounds with name" + name + "not found");
            return;
        }
        s.source2.Play();
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
