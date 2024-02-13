using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public Action ItemTaken { get; set; }
    public Action ItemPlaced { get; set; }

    private float speed = 10.0f;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ItemTaken?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ItemPlaced?.Invoke();
        }
    }

    public Vector2 GetPlayerVelocity()
    {
        float xVelocity = Input.GetAxis("Horizontal") * speed;
        float yVelocity = Input.GetAxis("Vertical") * speed;
        return new Vector2(xVelocity, yVelocity);
    }

    public void SetPlayerSpeed(float _speed)
    {
        this.speed = _speed;
    }
}
