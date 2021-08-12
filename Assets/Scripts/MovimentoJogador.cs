using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public void RotacaoJogador(LayerMask mascara_chao)
    {
        //Direção de onde o mouse do jogador está localizado
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);


        //Rotaciona o jogador para aonde o mouse está apontado
        RaycastHit impacto;
        if (Physics.Raycast(raio, out impacto, 100, mascara_chao))
        {
            Vector3 posicao_mira = impacto.point - transform.position;
            posicao_mira.y = transform.position.y;
            Rotacionar(posicao_mira);
        }
    }
}
