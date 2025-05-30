using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls controls;
    private float _valueX;

    public float ValueX { get => _valueX;  } // para usar esta variable publica

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += StartMove; // queda asociado al StartMove
        controls.Player.Move.canceled += StopMove;
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

    private void OnDisable()
    {
        controls.Player.Move.performed -= StartMove; // queda asociado al StartMove
        controls.Player.Move.canceled -= StopMove;
        controls.Player.Disable();
    }
}
