using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour           //SISTEMA ACTUALIZADO DE MOVIMIENTO PARA UN VECTOR2D ( UP , DOWN , LEFT , RIGHT)
{
    private Controls controls;
   [SerializeField] private Vector2 _value;

    public Vector2 Value { get => _value; } // para usar esta variable publica
    

    [SerializeField] private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += StartMove; // queda asociado al StartMove
        controls.Player.Move.canceled += StopMove;
        controls.Player.Jump.performed += StartJump;
        controls.Player.Jump.canceled += StopJump;
        controls.Player.Enable();

    }

    private void StartMove(InputAction.CallbackContext context) // cuando presiono la tecla
    {
        _value = context.ReadValue<Vector2>().normalized; // leo los valores en  X // el normalized va a siempre rendodiar para atras 1 o -1 dependiendo si es negativo
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _value = Vector2.zero; // cuando no se mueve el valor es 0 en x
    }

    public void StartJump (InputAction.CallbackContext context)
    {
        _isJumping = true;
    }

    public void StopJump (InputAction.CallbackContext context)
    {
        _isJumping = false;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= StartMove; // queda asociado al StartMove
        controls.Player.Move.canceled -= StopMove;
        controls.Player.Jump.performed -= StartJump;
        controls.Player.Jump.canceled -= StopJump;
        controls.Player.Disable();
    }
}
