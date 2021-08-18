using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefes : MonoBehaviour
{
    private float tempoGer=0;
    public float tempoEntGer=60;
    public GameObject chefe;

    private void Start()
    {
        tempoGer = tempoEntGer;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoGer)
        {
            Instantiate(chefe, transform.position, Quaternion.identity);
            tempoGer = Time.timeSinceLevelLoad + tempoEntGer;
        }
    }
}
