using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //COMPONENTS
    private Rigidbody2D m_rigidbody2D;                  //Prefijar nombres con m_(hace el codigo mas facil de lerr y distinguir entre campos y variables locales
    private GatherInput m_gatherinput;                  // LLamar a GetComponent una solo vez al inicio del start y guardar la referencia es una gran practica de rendimiento
    private Transform m_transform;                      //GetComponent es costoso, y encima guardar todos los componentes necesarios dentro del start para tu obj este completamente preparado para funcionar evita repetir esa busqueda innecesariamente
    private Animator m_animator;

    //VALUES
    [SerializeField] private float speed;
    private int direction = 1;
    private int idSpeed;
    [SerializeField] private float jumpForce;

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
        Jump();
    }   


    private void Move()
    {
        Flip(); //metodo para comprobar si estoy girado o no
        m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.ValueX, m_rigidbody2D.linearVelocityY);

    }

    private void Flip()
    {
        if(m_gatherinput.ValueX * direction < 0) // esta condicion verifica si el jugador esta intentando moverse en al dirrecion actual -1 izq, 1 der, 0 sin moverse
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x,1,1); // efecto visual de voltear el personaje
            direction *= -1;
        }
    }
    private void Jump() //!Anotacion : tengo q cambiar la gravedad desde el editor en rigibody2D para que el salto sea mas rapido o lo que yo necesite
    {
        if (m_gatherinput.IsJumping) // verifico si el jugador ha presionado el boton de salto
        {
            m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.ValueX, jumpForce); // defino la vel horizontal del personaje en el salto , y el jumpForce es lo que hace que se mueva en el eje Y (fuerza de salto
        }
        m_gatherinput.IsJumping = false; // despues de ejecutar el salto se reinicia la variable IsJumping a false para evitar que el personaje siga saltando continuamente
    }
}
