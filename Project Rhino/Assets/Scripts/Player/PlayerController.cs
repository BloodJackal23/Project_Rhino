using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;
    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] Animator animator;
    Vector2 moveInput;
    bool jump = false;
    bool canJump = true;
    // Start is called before the first frame update
    void Start()
    {
        SetCinemachineCam();
        if(!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = GetInput();
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
}
