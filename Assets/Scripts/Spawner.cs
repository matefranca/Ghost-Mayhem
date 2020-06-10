using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Script para gerenciar o surgimento dos inimigos.

    //Posições de onde vão surgir os inimigos.
    [SerializeField] Transform[] _spawnPoints;

    //Todos os objetos que vão ser criados para cair.
    [SerializeField] GameObject[] _hazards;

    //Tempo para o surgimento do próximo inimigo.
    private float _timeBtwSpawns;

    //Valor para qual o tempo do próximo surgimento vai resetar ao criar o inimigo.
    [SerializeField] float _startTimeBtwSpawns;

    //Valor minimo que o tempo para surgimento pode chegar.
    [SerializeField] float _minTimeBtwSpawns;

    //Valor que reduz o tempo para o próximo surgimento, até o valor mínimo.
    [SerializeField] float _decrease;

    //Referência ao objeto do jogador.
    private GameObject _player;

    //Metodo chamado antes do inicio do jogo.
    private void Awake()
    {
        //Referência ao objeto com a tag Player.
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    //Metodo chamado todo frame do jogo.
    void Update()
    {
        //Checando se ainda tem o jogador em cena.
        if (_player != null)
        {
            //Checando se o tempo para surgimento do inimigo é menor que 0.
            if (_timeBtwSpawns <= 0)
            {
                //Variável local de posição para escolher uma das posições que temos armazenado.
                Transform _randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                //Variável local de objeto para escolher um dos objetos de inimigos para ser criado.
                GameObject _randomHazard = _hazards[Random.Range(0, _hazards.Length)];

                //Cria o objeto que criamos localmente antes, na posição que também criamos localmente e sem nenhuma rotação.
                Instantiate(_randomHazard, _randomSpawnPoint.position, Quaternion.identity);

                //Checa se o tempo para o próximo surgimento é maior que o tempo mínimo determinado previamente.
                if (_startTimeBtwSpawns > _minTimeBtwSpawns)
                {
                    //Diminuindo o tempo do próximo surgimento em um valor específico definido anteriormente.
                    _startTimeBtwSpawns -= _decrease;
                }

                //Setamos o tempo para surgimento do inimigo de volta para o valor armazenado.
                _timeBtwSpawns = _startTimeBtwSpawns;
            }
            else
            {
                //Reduzindo o valor para cada tempo decorrido no jogo.
                _timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
