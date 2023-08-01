using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    private static InputManager _instance;
    public static InputManager Instance => _instance;

    private PlayerInput _playerInput;

    private InputAction _mouseClick;
    private InputAction _mousePosition;

    private Vector2 _lastClickPos;

    public event Action<Vector2> OnClickEvent;

    public Vector2 GetClickWorldPos()
    {
        return _camera.ScreenToWorldPoint(_lastClickPos);
    }

    private void Awake()
    {
        _instance = this;
        
        _playerInput = GetComponent<PlayerInput>();

        _mouseClick = _playerInput.actions["MouseClick"];
        _mousePosition = _playerInput.actions["MousePosition"];

        _mouseClick.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext obj)
    {
        _lastClickPos = _mousePosition.ReadValue<Vector2>();
        OnClickEvent?.Invoke(_lastClickPos);
    }

    private void OnDestroy()
    {
        _mouseClick.performed -= OnClick;
    }
}
