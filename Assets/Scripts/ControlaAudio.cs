using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{

    public static AudioSource instancia;
    private AudioSource adsrc;

    void Awake()
    {
        adsrc = GetComponent<AudioSource>();
        instancia = adsrc;
    }

}
