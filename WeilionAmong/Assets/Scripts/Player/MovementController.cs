using Fusion;
using UnityEngine;

public class MovementController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Vector2 dir = data.MovementInput.normalized;
            //_rigidbody2D.position += dir * _moveSpeed * Runner.DeltaTime;
            //_rigidbody2D.velocity = dir * _moveSpeed * Runner.DeltaTime;
            //_rigidbody2D.MovePosition(_rigidbody2D.position + dir * _moveSpeed * Runner.DeltaTime);
            //transform.position += new Vector3(dir.x, dir.y, 0) * _moveSpeed * Runner.DeltaTime;
            transform.Translate(dir * _moveSpeed * Runner.DeltaTime);
        }
    }
}