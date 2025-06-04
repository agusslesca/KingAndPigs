using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls controls;
    private float _valueX;

    public float ValueX { get => _valueX; } // para usar esta variable publica
    

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
        _valueX = context.ReadValue<float>(); // leo los valores en  X
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _valueX = 0; // cuando no se mueve el valor es 0 en x
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
