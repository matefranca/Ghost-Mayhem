using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Script para gerenciar as funções do Jogador.

    //Painel de derrota que aparece quando o jogador morre.
    [SerializeField] GameObject _losePanel;

    //Exibição da vida do Jogador.
    [SerializeField] Text _healthDisplay;

    //Exibição de tempo do Jogo.
    [SerializeField] Text _timeDisplay;

    //Velocidade do Jogador.
    [SerializeField] float _speed;

    //Variável para controle do Corpo do Jogador.
    Rigidbody2D _rb;

    //Referência ao Animator do Jogador.
    Animator _anim;

    //Referência ao controle de Áudio no Jogador.
    AudioSource _source;

    //Variável para quantificar o input do Jogador.
    float _input;

    //Variável para a quantidade de vida do Jogador.
    [SerializeField] float _health;

    //Tempo inicial de duração do Dash.
    [SerializeField] float _startDashTime;

    //Variável para contar o tempo que o Dash está ocorrendo.
    private float _dashTime;

    //Acréscimo de velocidade no Dash.
    [SerializeField] float _extraSpeed;

    //Variável para checar se o Jogador está no Dash.
    private bool _isDashing;

    //Método chamado no início do jogo.
    void Start()
    {

        //Preenchendo todas as variáveis com os Componentes presentes no objeto Jogador.
        _source = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        //Inicializando os textos de vida e de tempo.
        _healthDisplay.text = _health.ToString();
        _timeDisplay.text = Time.timeSinceLevelLoad.ToString("F1");
    }

    //Método chamado todo frame do jogo.
    void Update()
    {
        //Definindo o texto de tempo para ser o tempo decorrido desde o início do level.
        _timeDisplay.text = Time.timeSinceLevelLoad.ToString("F1");

        //Variável preenchida com o input do jogador.
        //Vai ter o valor 1 se a tecla da seta para a direita ou o d for clicado
        //Valor -1 se a tecla da seta para a esquerda ou a for clicado.
        _input = Input.GetAxisRaw("Horizontal");

        //Checando se o jogador está ou não clicando as teclas de andar.
        if (_input != 0)
        {
            //Seta a variável dentro do Animator para que habilitar a animação de correr.
            _anim.SetBool("isRunning", true);
        }
        else
        {
            //Seta a variável dentro do Animator para que desabilitar a animação de correr.
            //Habilita a animação de parado.
            _anim.SetBool("isRunning", false);
        }

        //Checando se o Jogador clicou para ir para a direita.
        if (_input > 0)
        {
            //Vira o personagem para a direita.
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //Checando se o Jogador clicou para ir para a esquerda.
        else if (_input < 0)
        {
            //Vira o personagem para a esquerda.
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //CHecando se o Jogador apertou a barra de espaço e se o Jogador não está dando Dash.
        if (Input.GetKeyDown(KeyCode.Space) && _isDashing == false)
        {
            //Amenta a velocidade do Jogador.
            _speed += _extraSpeed;

            //Seta a booleana para verdadeiro para não poder dar Dash duplo.
            _isDashing = true;

            //Reseta o temporizador do Dash.
            _dashTime = _startDashTime;
        }

        //Checando se o contador do Dash chegou a 0 enquanto estiver no Dash.
        if (_dashTime <= 0 && _isDashing == true)
        {
            //Seta a booleana para falso para poder dar o Dash novamente.
            _isDashing = false;

            //Seta a velocidade para a velocidade normal.
            _speed -= _extraSpeed;
        }

        //Checa se o contador ainda não chegou a 0.
        else
        {
            //Reduz o contador.
            _dashTime -= Time.deltaTime;
        }
    }

    //Método que é chamado em períodos fixos de tempo
    //Utilizado para física na Unity.
    void FixedUpdate()
    {
        //Definindo a velocidade do Jogador de acordo com o Input do mesmo.
        _rb.velocity = new Vector2(_input * _speed, _rb.velocity.y);
    }

    //Método para quando o Jogador toma dano.
    public void TakeDamage (int damageAmount)
    {
        //Toca o áudio de dano.
        _source.Play();

        //Diminui a vida do jogador pela quantidade de dano que é passada por quem chama o método.
        _health -= damageAmount;

        //Atualiza o display na interface do Jogador com a nova vida.
        _healthDisplay.text = _health.ToString();

        //Checa se a vida do Jogador chegou a 0, se ele morreu.
        //Faz a checagem de menor ou igual a 0 porque as vezes o dano pode fazer a vida ficar abaixo de 0.
        if (_health <= 0)
        {
            //Seta a vida para 0.
            _health = 0;

            //Ativa painel de derrota para o Jogador reiniciar o jogo.
            _losePanel.SetActive(true);

            //Destroi o Jogador.
            Destroy(gameObject);
        }
    }
}
