using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    public GameObject jogador;
    public AudioClip som_mortezumbi;
    public GameObject KitMedico;
    private MovimentoPersonagem script_mov;
    private AnimacaoPersonagem script_anm;
    private Status script_status;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoPos=4;
    private float percKit = 0.1f;
    private ControlaInterface script_inteface;
    [HideInInspector]
    public GeradorZumbis script_gerador;


    void Start()
    {
        script_mov = GetComponent<MovimentoPersonagem>();
        script_anm = GetComponent<AnimacaoPersonagem>();
        script_status = GetComponent<Status>();
        jogador = GameObject.FindWithTag("Player");
        Randomizar();
        script_inteface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        //Rotaciona o inimigo de acordo com a direção do jogador
        script_mov.Rotacionar(direcao);
        script_anm.Movimentar(direcao.magnitude);
        if(distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5){
            //Move o inimigo para a direção que o jogador se encontra
            direcao = jogador.transform.position - transform.position;
            script_mov.Movimentar(direcao,script_status.velocidade);
            script_anm.Atacar(false);
        }

        else{
            direcao = jogador.transform.position - transform.position;
            script_anm.Atacar(true);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoPos + Random.Range(-1f,1f);
        }

        bool perto = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (!perto)
        {
            direcao = posicaoAleatoria - transform.position;
            script_mov.Movimentar(direcao, script_status.velocidade);
        }
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }

    void AtacaJogador()
    {
        //Condição de Game Over e sistema de vida
        int dano_aleatorio = Random.Range(20,30);
        jogador.GetComponent<ControlaJogador>().tomarDano(dano_aleatorio);
    }

    void Randomizar()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void tomarDano(int dano)
    {
        script_status.vida -= dano;
        if (script_status.vida <= 0) Morrer();
    }

    public void Morrer()
    {
        Destroy(gameObject,2);
        script_anm.Morrer();
        script_mov.Morrer();
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(som_mortezumbi);
        GerarKitMedico(percKit);
        script_inteface.contarKills();
        script_gerador.DiminuirZumbis();
    }

    public void GerarKitMedico(float perc)
    {
        if(Random.value <= perc)
        {
            Instantiate(KitMedico,transform.position, Quaternion.identity);
        }
    }
}
