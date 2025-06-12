using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D m_rigibody2D;               //Si los componentes lo tienen el hijo no el gameobj principal no es necesario ponerles m_ ya que asi diferenciamos que es el hijo.
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        m_rigibody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>(); // se pone Inchildren por que lo tiene el hijo y optimiza la busqueda del componente.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
       gameManager = GameManager.instance; //llamar el gamemanger
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger con: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Player detectado!");
            spriteRenderer.enabled = false; // apagar la img
            m_rigibody2D.simulated = false; // en ves de destroy al obj usamos apagar el collider, ya que con destroy quedan restos de basuras
            gameManager.AddDiamond(); // Suma un diamond.
        }
    }
}
