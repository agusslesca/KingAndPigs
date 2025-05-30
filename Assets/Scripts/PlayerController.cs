using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherinput;
    private Transform m_transform;
    [SerializeField] private float speed;
    
    void Start()
    {
        m_gatherinput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();

    }

    
    void FixedUpdate()
    {
        m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.ValueX, m_rigidbody2D.linearVelocityY);
    }
}
