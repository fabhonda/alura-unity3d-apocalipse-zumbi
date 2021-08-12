using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{

    private Rigidbody rgb;

    void Awake()
    {
        rgb = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        rgb.MovePosition(rgb.position + Time.deltaTime * velocidade * direcao.normalized);
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion rotacao_nova = Quaternion.LookRotation(direcao);
        rgb.MoveRotation(rotacao_nova);
    }

    public void Morrer()
    {
        rgb.constraints = RigidbodyConstraints.None;
        rgb.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
}
