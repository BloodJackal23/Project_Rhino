using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] CharacterController2D m_CharacterController;
    [SerializeField] Animator m_Animator;
    Vector2 moveInput = Vector2.zero;
    bool jump = false;
    bool canJump = true;

    [SerializeField] AudioSource jumpAudio;
    [SerializeField] private AudioSource deathAudio;

    public delegate void OnInteractionAvailable();
    public OnInteractionAvailable onInteraction;

    public delegate void OnDeath();
    public OnDeath onDeath;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        SetCinemachineCam();
        if (!m_Animator)
        {
            m_Animator = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        onDeath += PlayDeathAudio;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gamePaused)
        {
            moveInput = new Vector2(GetHorInput(), GetVerInput());
            onInteraction?.Invoke();
        }
        bool isGrounded = m_CharacterController.IsGrounded();
        m_Animator.SetFloat("isRunning", Mathf.Abs(moveInput.x));
        if(moveInput.y > 0)
        {
            if(isGrounded && canJump)
            {
                jump = true;
                canJump = false;
                if(jumpAudio)
                {
                    jumpAudio.Play();
                }
            }
        }
        else
        {
            jump = false;
            if (isGrounded)
            {
                canJump = true;
            }
        }
        
        m_Animator.SetBool("isJumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        m_CharacterController.Move(moveInput.x * m_CharacterController.GetSpeed() * Time.fixedDeltaTime, false, jump);
    }

    private void OnDestroy()
    {
        onDeath -= PlayDeathAudio;
    }

    void SetCinemachineCam()
    {
        virtualCam = GameObject.FindGameObjectWithTag("CM PlayerCam").GetComponent<CinemachineVirtualCamera>();
        virtualCam.m_Follow = transform;
    }

    float GetHorInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    float GetVerInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    private void PlayDeathAudio()
    {
        deathAudio.Play();
    }
}
