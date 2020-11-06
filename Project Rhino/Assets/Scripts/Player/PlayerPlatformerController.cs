using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPlatformerController : PhysicsObject
{
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float jumpTakeOffSpeed = 7f;
    bool canJump = true;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    [SerializeField] CinemachineVirtualCamera virtualCam;

    public delegate void OnInteractionAvailable();
    public OnInteractionAvailable interactionDelegate;

    void Awake()
    {
        if(!spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if(!animator)
        {
            animator = GetComponent<Animator>();
        }
        gameManager = GameManager.instance;
        SetCinemachineCam();
    }

    protected override void Update()
    {
        PauseGame();
        base.Update();
        if(!gameManager.gamePaused)
        {
            interactionDelegate?.Invoke();
        }
    }

    private void OnDestroy()
    {
        interactionDelegate = null;
    }

    protected override void ComputeVelocity()
    {
        Vector2 moveInput = Vector2.zero;
        moveInput.x = GetHorInput(true);
        moveInput.y = GetVerInput(true);
        if (moveInput.y > 0)
        {
            if (grounded && canJump)
            {
                velocity.y = jumpTakeOffSpeed;
                canJump = false;
            }
        }
        else
        {
            if (velocity.y > 0)
            {
                velocity.y *= .5f;
            }
            else if(grounded)
            {
                canJump = true;
            }
        }

        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetBool("isJumping", !grounded);
        animator.SetFloat("isRunning", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = moveInput * maxSpeed;
    }

    float GetHorInput(bool _raw)
    {
        if(_raw)
        {
            return Input.GetAxisRaw("Horizontal");
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    float GetVerInput(bool _raw)
    {
        if (_raw)
        {
            return Input.GetAxisRaw("Vertical");
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }

    void PauseGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameManager.PauseGameToggle();
        }
    }

    void SetCinemachineCam()
    {
        virtualCam = GameObject.FindGameObjectWithTag("CM PlayerCam").GetComponent<CinemachineVirtualCamera>();
        virtualCam.m_Follow = transform;
    }
}
