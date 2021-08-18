using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status script_stats;
    private AnimacaoPersonagem script_anim;
    private MovimentoPersonagem script_mov;
    public GameObject kit;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;
        agente = GetComponent<NavMeshAgent>();
        script_stats = GetComponent<Status>();
        script_anim = GetComponent<AnimacaoPersonagem>();
        script_mov = GetComponent<MovimentoPersonagem>();
        agente.speed = script_stats.velocidade;
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        script_anim.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath)
        {
            bool perto = agente.remainingDistance <= agente.stoppingDistance;
            if (perto)
            {
                script_anim.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                script_mov.Rotacionar(direcao);
            }
            else script_anim.Atacar(false);
        }

    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().tomarDano(dano);
    }

    public void tomarDano(int dano)
    {
        script_stats.vida -= dano;
        if (script_stats.vida <= 0) Morrer();
    }

    public void Morrer()
    {
        script_anim.Morrer();
        script_mov.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(kit, transform.position,Quaternion.identity);
        Destroy(gameObject, 2);
    }
}
