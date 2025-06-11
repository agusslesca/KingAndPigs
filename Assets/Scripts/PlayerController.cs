using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Components")]
   [SerializeField] private Transform m_transform;                      //GetComponent es costoso, y encima guardar todos los componentes necesarios dentro del start para tu obj este completamente preparado para funcionar evita repetir esa busqueda innecesariamente
    private Rigidbody2D m_rigidbody2D;                  //Prefijar nombres con m_(hace el codigo mas facil de lerr y distinguir entre campos y variables locales
    private GatherInput m_gatherinput;                  // LLamar a GetComponent una solo vez al inicio del start y guardar la referencia es una gran practica de rendimiento
    private Animator m_animator;

    // ANIMATOR IDS
    private int idIsGrounded;
    private int idSpeed;

    [Header("Move settings")]
    [SerializeField] private float speed;
    private int direction = 1;
    

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private int counterxtrajumps;
    [SerializeField] private bool canDoubleJump; // si puedo hacer doble salto


    [Header("Ground Settings")]
    [SerializeField] private Transform lFoot;
    [SerializeField] private Transform rFoot;
    RaycastHit2D lFootRay;
    RaycastHit2D rFootRay;
    [SerializeField] private bool isGrounded;                        
    [SerializeField] private float rayLenght;
    [SerializeField] private LayerMask groundLayer;

    [Header("Wall Settings")]
    [SerializeField] private float checklWallDistance;
    [SerializeField] private bool isWallDetected;

    private void Awake()
    {
        m_gatherinput = GetComponent<GatherInput>();
       // m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    //Componentes del player mismo se puede solicitar en el awake, pero componentes de otro
    //game obj se deberia solicitar en el start.
    void Start()
    {
       
        idSpeed = Animator.StringToHash("speed"); // convertir el el string de speed en numero asi no consume tanto ya que si no tiene q entrar el trigger y leer uno por uno las letras.
        idIsGrounded = Animator.StringToHash("isGrounded");
        lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
        rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
        counterxtrajumps = extraJumps;
    }

    private void Update()
    {
        SetAnimatorValues();
        
    }

    private void SetAnimatorValues()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocityX));       //Mathf.abs te devuele un numero siempre positivo
        m_animator.SetBool(idIsGrounded, isGrounded);
    }

    void FixedUpdate()
    {
        CheckCollision();
        Move();
        Jump();
    }

    private void CheckCollision() // verifica las colisiones si toca el ground o wall
    {
        HandleGround();
        HandleWall();
    }

    private void HandleWall()
    {
        isWallDetected = Physics2D.Raycast(m_transform.position, Vector2.right * direction, checklWallDistance, groundLayer); // detectar la pared
    }

    private void HandleGround()
    {
        lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLenght, groundLayer);
         rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLenght, groundLayer);

        if (lFootRay || rFootRay)
        {
            isGrounded = true;
            counterxtrajumps = extraJumps; // se recargan los saltos cuando toca el ground
            canDoubleJump = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Move()
    {
        Flip(); //metodo para comprobar si estoy girado o no
        m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.Value.x, m_rigidbody2D.linearVelocityY);

    }

    private void Flip()
    {
        if(m_gatherinput.Value.x * direction < 0) // esta condicion verifica si el jugador esta intentando moverse en al dirrecion actual -1 izq, 1 der, 0 sin moverse
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x,1,1); // efecto visual de voltear el personaje
            direction *= -1;
        }
    }
    private void Jump() //!Anotacion : tengo q cambiar la gravedad desde el editor en rigibody2D para que el salto sea mas rapido o lo que yo necesite
    {
        if (m_gatherinput.IsJumping) // verifico si el jugador ha presionado el boton de salto
        {
            if (isGrounded)
            {
            m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.Value.x, jumpForce); // defino la vel horizontal del personaje en el salto , y el jumpForce es lo que hace que se mueva en el eje Y (fuerza de salto
                canDoubleJump = true;
            }
            else if (counterxtrajumps > 0 && canDoubleJump)
            {
                m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.Value.x, jumpForce);
                counterxtrajumps--;

            }
        }
        m_gatherinput.IsJumping = false; // despues de ejecutar el salto se reinicia la variable IsJumping a false para evitar que el personaje siga saltando continuamente
    }

    private void OnDrawGizmos() // es un metodo que me permite dibujar y mostrar los Gizmos (ahora vamos a dibujar un rayo physicsraycast)
    {
        Gizmos.DrawLine(m_transform.position, new Vector2(m_transform.position.x + (checklWallDistance * direction), m_transform.position.y));
    }


}
