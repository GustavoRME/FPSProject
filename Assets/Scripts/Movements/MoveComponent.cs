using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    [SerializeField] private WeaponAnimatorComponent _weaponAnimator = null;

    private CharacterController _controller;    

    public Vector2 Axis { get; set; }
    public bool IsWalking { get => Axis.magnitude > 0; }

    private void Start() => _controller = GetComponent<CharacterController>();

    private void Update()
    {
        //Move as the player is looking
        Vector3 move = transform.right * Axis.x + transform.forward * Axis.y;

        //Stay in the ground
        move.y = 0;

        //Make movement with speed
        _controller.Move(move * _speed * Time.deltaTime);        

        //Set animation movement 
        _weaponAnimator.MoveAnimation(Axis.magnitude);
    }        
}
