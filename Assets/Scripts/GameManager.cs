using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Script para gerenciamento geral do Jogo.

    //Método para iniciar o Jogo.
    public void PlayGame()
    {
        //Método que carrega a cena.
        SceneManager.LoadScene("Game");
    }
}
