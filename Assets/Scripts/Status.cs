using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int vida_inicial = 100;
    [HideInInspector]
    public int vida=100;
    public float velocidade=5;

    void Awake()
    {
        vida = vida_inicial;
    }
}
