using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player _player;
    Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = InputController.Instance.GetPlayerVelocity(_player.PlayerIndex) * _player.CurrentSpeed;
    }
}
