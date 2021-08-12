using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{

    private ControlaJogador script_jogador;
    public Slider sldr_vida;
    public GameObject painel_gameover;
    public Text texto_sobrev;
    public Text texto_maxsobrev;
    public Text texto_kills;
    private float maxtempo=0;
    private int kills=0;
    // Start is called before the first frame update
    void Start()
    {
        painel_gameover.SetActive(false);
        script_jogador = GameObject.FindWithTag("Player").GetComponent<ControlaJogador>();
        sldr_vida.maxValue = script_jogador.script_status.vida;
        atualizarVida();
        Time.timeScale = 1;
        maxtempo = PlayerPrefs.GetFloat("PontuacaoMaxima");
        texto_kills.text = string.Format("x {0}", kills);
    }

    public void atualizarVida()
    {
        sldr_vida.value = script_jogador.script_status.vida;
    }

    public void contarKills()
    {
        kills++;
        texto_kills.text = string.Format("x {0}", kills);
    }

    public void GameOver()
    {
        painel_gameover.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        texto_sobrev.text = "Você sobreviveu por " + minutos + " minutos e " + segundos + " segundos";
        AjustarPontuacao(minutos, segundos);
    }

    void AjustarPontuacao(int minutos, int segundos)
    {
        if(Time.timeSinceLevelLoad > maxtempo)
        {
            maxtempo = Time.timeSinceLevelLoad;
            texto_maxsobrev.text = string.Format("Seu melhor tempo é {0}min e {1}seg", minutos, segundos);
            PlayerPrefs.SetFloat("PontuacaoMaxima",maxtempo);
        }
        if(texto_maxsobrev.text == "")
        {
            minutos = (int)(maxtempo / 60);
            segundos = (int)(maxtempo % 60);
            texto_maxsobrev.text = string.Format("Seu melhor tempo é {0}min e {1}seg", minutos, segundos);
        }
    }

    public void reiniciarfase()
    {
        SceneManager.LoadScene("game");
    }
}
