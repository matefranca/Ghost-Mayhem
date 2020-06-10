using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Script para gerenciar os inimigos caindo.

    //Variáveis para a máxima e mínima velocidade que o objeto pode ter.
    [SerializeField] float _maxSpeed;
    [SerializeField] float _minSpeed;
    
    //Variável para a velocidade que o objeto vai ter.
    float _speed;

    //Referência para o script do Jogador.
    Player _playerScript;

    //Dano que o objeto vai causar.
    [SerializeField] int _damage;

    //Particula de explosão para ser instanciada.
    [SerializeField] GameObject _explosion;
    
    //Método chamado no inicio do Jogo.
    void Start()
    {
        //Seta a velocidade do objeto para ser um valor aleatório entre
        //a velocidade máxima e mínima que definimos.
        _speed = Random.Range(_minSpeed, _maxSpeed);

        //Busca o script do Jogador no objeto com a Tag Player.
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    //Metodo chamado uma vez por frame.
    void Update()
    {
        //Função para mover o objeto para baixo a uma velocidade constante.
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
    }

    //Metodo que ativa quando o objeto colide com outro objeto que possui um colisor
    //Armazena as informações do colisor.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checa se colidiu com o Jogador.
        if (collision.tag == "Player")
        {
            //Chama o método de tomar dano dentro do script do Jogador e passa o dano desse objeto.
            _playerScript.TakeDamage(_damage);

            //Faz surgir a explosão na posição da colisão e sem rotação.
            Instantiate(_explosion, transform.position, Quaternion.identity);

            //Destrói o ojbeto.
            Destroy(gameObject);
        }

        //Checa se colidiu com o chão.
        if (collision.tag == "Ground")
        {
            //Faz surgir a explosão na posição da colisão e sem rotação.
            Instantiate(_explosion, transform.position, Quaternion.identity);

            //Destrói o ojbeto.
            Destroy(gameObject);
        }
    }
}
