using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    public GameObject zumbi;
    public float geracaozumbis_time = 1;
    public LayerMask layer_zumbi;
    private float cont_time = 0;
    private float distanciaGeradores = 3;
    private float distanciaJogadorGeracao = 20;
    private GameObject jogador;
    private int qtdZumbiMax = 2;
    private int qtdZumbiAtual;
    private float tempoDificuldade = 30;
    private float contadorDificuldade;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        contadorDificuldade = tempoDificuldade;
        for(int i=0; i < qtdZumbiMax; i++)
        {
            StartCoroutine(NovoZumbi());
        }
    }

    void Update()
    {
        bool dist = Vector3.Distance(transform.position, jogador.transform.position) > distanciaJogadorGeracao;
        if (dist && qtdZumbiAtual < qtdZumbiMax)
        {
            cont_time += Time.deltaTime;
            if (cont_time >= geracaozumbis_time)
            {
                StartCoroutine(NovoZumbi());
                cont_time = 0;
            }
        }
        
        if(Time.timeSinceLevelLoad > contadorDificuldade)
        {
            qtdZumbiMax++;
            contadorDificuldade = Time.timeSinceLevelLoad + tempoDificuldade;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,distanciaGeradores);
    }

    IEnumerator NovoZumbi()
    {
        Vector3 posCriacao = AleatorizarPos();
        Collider[] colisores = Physics.OverlapSphere(posCriacao, 1, layer_zumbi);
        while (colisores.Length > 0)
        {
            posCriacao = AleatorizarPos();
            colisores = Physics.OverlapSphere(posCriacao, 1, layer_zumbi);
            yield return null;
        }
        ControlaInimigo zombie = Instantiate(zumbi, posCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zombie.script_gerador = this;
        qtdZumbiAtual++;

    }

    Vector3 AleatorizarPos()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaGeradores;
        posicao += transform.position;
        posicao.y = 0;
        return posicao;
    }

    public void DiminuirZumbis()
    {
        qtdZumbiAtual--;
    }
}
