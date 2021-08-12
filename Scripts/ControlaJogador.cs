using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{

    public LayerMask mascara_chao;
    public GameObject texto_fim;
    public GameObject botao;
    public ControlaInterface script_interface;
    private MovimentoJogador script_mvj;
    private AnimacaoPersonagem script_anmpers;
    public Status script_status;
    public AudioClip som_dano;
    private Vector3 direcao;

    // Start is called before the first frame update
    void Start()
    {
        script_mvj = GetComponent<MovimentoJogador>();
        script_anmpers = GetComponent<AnimacaoPersonagem>();
        script_status = GetComponent<Status>();
    }

    void Update()
    {
        //Direções do personagem em cada eixo
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);

        //Animações do personagem
        script_anmpers.Movimentar(direcao.magnitude);

    }

    void FixedUpdate()
    {
        //Movimentação do personagem
        script_mvj.Movimentar(direcao, script_status.velocidade);
        script_mvj.RotacaoJogador(mascara_chao);
    }

    public void tomarDano(int dano)
    {
        script_status.vida -= 30;
        script_interface.atualizarVida();
        ControlaAudio.instancia.PlayOneShot(som_dano);
        if (script_status.vida <= 0) Morrer();
    }

    public void Morrer()
    {
        //Controla a "velocidade" do jogo
        script_interface.GameOver();
    }

    public void CurarVida(int qtd)
    {
        script_status.vida += qtd;
        if(script_status.vida > script_status.vida_inicial)
        {
            script_status.vida = script_status.vida_inicial;
        }
        script_interface.atualizarVida();
    }
}

//transform.Translate(direcao * velocidade * Time.deltaTime);
//Debug.DrawRay(raio.origin,raio.direction*100, Color.red);


