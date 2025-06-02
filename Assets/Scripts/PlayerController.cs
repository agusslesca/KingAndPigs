using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //COMPONENTS
    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherinput;
    private Transform m_transform;
    private Animator m_animator;

    //VALUES
    [SerializeField] private float speed;
    private int direction = 1;
    private int idSpeed;

    void Start()
    {
        m_gatherinput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        idSpeed = Animator.StringToHash("Speed"); // convertir el el string de speed en numero asi no consume tanto ya que si no tiene q entrar el trigger y leer uno por uno las letras.
    }

    private void Update()
    {
        SetAnimatorValues();
        
    }

    private void SetAnimatorValues()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocityX));       //Mathf.abs te devuele un numero siempre positivo
    }

    void FixedUpdate()
    {
        Move();
    }   

    private void Move()
    {
        Flip(); //metodo para comprobar si estoy girado o no
        m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.ValueX, m_rigidbody2D.linearVelocityY);

    }

    private void Flip()
    {
        if(m_gatherinput.ValueX * direction < 0)
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x,1,1);
            direction *= -1;
        }
    }
}
