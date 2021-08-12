using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBala : MonoBehaviour
{

    private Rigidbody rgb;
    public float velocidade = 20;
    public AudioClip som_mortezumbi;
    private int dano=1;

    void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rgb.MovePosition(rgb.position + transform.forward * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider colisor)
    {
        if (colisor.CompareTag("Inimigo"))
        {
            colisor.GetComponent<ControlaInimigo>().tomarDano(dano);
        }
        Destroy(gameObject);

    }
}
