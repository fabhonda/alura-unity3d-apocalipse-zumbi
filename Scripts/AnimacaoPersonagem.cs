using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator anmtor;

    void Awake()
    {
        anmtor = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        anmtor.SetBool("Atacando", estado);
    }

    public void Movimentar(float valor_mov)
    {
        anmtor.SetFloat("Movendo", valor_mov);
    }

    public void Morrer()
    {
        anmtor.SetTrigger("Morrer");
    }
}
