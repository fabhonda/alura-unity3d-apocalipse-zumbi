using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{

    public GameObject bala;
    public GameObject cano;
    public AudioClip som_tiro;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bala, cano.transform.position, cano.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(som_tiro);
        }
    }
}
