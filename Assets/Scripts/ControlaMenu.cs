using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{

    public GameObject botao_sair;

    private void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        botao_sair.SetActive(true);
        #endif
    }
    public void jogar()
    {
        StartCoroutine(mudarCena("game"));
    }

    IEnumerator mudarCena(string name)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
    }

    public void sair()
    {
        StartCoroutine(sairjogo());
    }

    IEnumerator sairjogo()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
