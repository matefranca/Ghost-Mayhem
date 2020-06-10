using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //Script para gerenciar a música do Jogo.

    //Criamos uma instância do Gerenciador de Música.
    private static MusicManager instance;

    //Método chamado antes do inicio do jogo.
    private void Awake()
    {
        //Checando se já existe alguma instância do Music Manager.
        if (instance == null)
        {
            //Se não existir nenhuma instância, essa vira a primeira instância.
            instance = this;
            //Função que faz com que essa instância não se destrua ao carregar uma próxima cena.
            DontDestroyOnLoad(instance);
        }
        else
        {
            //Destrói a instância caso exista outra para a música não tocar duas vezes.
            Destroy(gameObject);
        }
    }
}
