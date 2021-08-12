using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int cura = 15;
    private int duracao = 5;

    private void Start()
    {
        Destroy(gameObject, duracao);
    }

    void OnTriggerEnter(Collider colisor)
    {
        if (colisor.tag == "Player")
        {
            colisor.GetComponent<ControlaJogador>().CurarVida(cura);
            Destroy(gameObject);
        }
    }
}
