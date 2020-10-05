using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] CharacterController2D characterController;
    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] Animator animator;
    Vector2 moveInput;
    bool jump = false;
    bool canJump = true;

    public delegate void OnInteractionAvailable();
    public OnInteractionAvailable interactionDelegate;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        SetCinemachineCam();
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        if(!gameManager.gamePaused)
        {
            moveInput = GetInput();
            interactionDelegate?.Invoke();
        }
        bool isGrounded = characterController.IsGrounded();
        animator.SetFloat("isRunning", Mathf.Abs(moveInput.x));
        if(moveInput.y > 0)
        {
            if(isGrounded && canJump)
            {
                jump = true;
                canJump = false;
            }
        }
        else
        {
            canJump = true;
        }
        animator.SetBool("isJumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        characterController.Move(moveInput.x * characterController.GetSpeed() * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    void SetCinemachineCam()
    {
        virtualCam = GameObject.FindGameObjectWithTag("CM PlayerCam").GetComponent<CinemachineVirtualCamera>();
        virtualCam.m_Follow = transform;
    }

    Vector2 GetInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        return input;
    }

    void PauseGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameManager.PauseGameToggle();
        }
    }
}
