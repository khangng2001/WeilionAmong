using Fusion;
using UnityEngine;

public class MovementController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;

    private InputHandler _inputHandler;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<InputHandler>();
    }

    //private void FixedUpdate()
    //{
    //    Move(_inputHandler.PlayerMovement);
    //}

    //private void Move(Vector2 direction)
    //{
    //    Vector2 moveDir = direction.normalized * _moveSpeed;
    //    _rigidbody2D.velocity = moveDir;

    //}

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Vector2 dir = data.MovementInput.normalized;
            _rigidbody2D.position += dir * _moveSpeed * Runner.DeltaTime;
        }
    }
}