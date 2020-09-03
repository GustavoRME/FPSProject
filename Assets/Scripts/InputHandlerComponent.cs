using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandlerComponent : MonoBehaviour
{
    private InputActions _inputHandle;

    [Header("Movement")]
    [SerializeField] private UnityEvent<Vector2> _OnInputMove = null;
    [SerializeField] private UnityEvent<Vector2> _OnMouseAim = null;
    [SerializeField] private UnityEvent _OnInputJump = null;
    
    [Header("Weapons")]
    [SerializeField] private UnityEvent _OnInputFire = null;
    [SerializeField] private UnityEvent _OnInputReload = null;
    [SerializeField] private UnityEvent<float> _OnInputScroll = null;
    [SerializeField] private UnityEvent<float> _OnInputNumbers = null;

    [Header("Interaction")]
    [SerializeField] private UnityEvent _OnInteraction = null;
    [SerializeField] private UnityEvent _OnInputMenu = null;

    private void Awake()
    {
        _inputHandle = new InputActions();

        //From keyboard 
        _inputHandle.Player.Move.performed += ctx => _OnInputMove?.Invoke(ctx.ReadValue<Vector2>());
        _inputHandle.Player.Jump.started += ctx => _OnInputJump?.Invoke();
        _inputHandle.Player.Reload.started += ctx => _OnInputReload?.Invoke();
        _inputHandle.Player.Menu.started += ctx => _OnInputMenu?.Invoke();
        _inputHandle.Player.N1.performed += ctx => _OnInputNumbers?.Invoke(0f);
        _inputHandle.Player.N2.performed += ctx => _OnInputNumbers?.Invoke(1f);
        _inputHandle.Player.N3.performed += ctx => _OnInputNumbers?.Invoke(2f);

        //From mouse
        _inputHandle.Player.Fire.performed += ctx => _OnInputFire?.Invoke();
        _inputHandle.Player.Fire.canceled += ctx => _OnInputFire?.Invoke();
        _inputHandle.Player.Aim.performed += ctx => _OnMouseAim?.Invoke(ctx.ReadValue<Vector2>());
        _inputHandle.Player.Scroll.performed += ctx => _OnInputScroll?.Invoke(ctx.ReadValue<float>());
        _inputHandle.Player.Interaction.performed += ctx => _OnInteraction?.Invoke();
    }
    private void OnEnable() => _inputHandle.Enable();

    private void OnDisable() => _inputHandle.Disable();
        
}
