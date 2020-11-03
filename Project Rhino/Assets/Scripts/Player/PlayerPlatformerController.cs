using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPlatformerController : PhysicsObject
{
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float jumpTakeOffSpeed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ComputeVelocity()
    {
        base.ComputeVelocity();
        Vector2 move = Vector2.zero;
        move.x = GetHorInput();

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0)
            {
                velocity.y *= .5f;
            }
        }
        targetVelocity = move * maxSpeed;
    }

    float GetHorInput()
    {
        float input = Input.GetAxisRaw("Horizontal");
        return input;
    }
}
