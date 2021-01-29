using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Delegates
    public delegate void OnInteractionAvailable();
    public OnInteractionAvailable onInteraction;

    public delegate void OnDeath();
    public OnDeath onDeath;
    #endregion

    #region Members
    [Header("Members")]
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private CharacterController2D m_CharacterController;
    [SerializeField] private Animator m_Animator;
    [Space]
    #endregion

    #region Internal Variables
    private GameManager gameManager;
    private Vector2 moveInput = Vector2.zero;
    private bool isJumping = false;
    private bool canJump = true;
    #endregion

    #region Sound Effects
    [Header("Sound Effects")]
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource deathAudio;
    [Space]
    #endregion

    #region Particle Effects
    [Header("Particle Effects")]
    [SerializeField] private GameObject deathExplosionPrefab;
    #endregion

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
        onDeath += SpawnDeathParticles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gamePaused)
        {
            moveInput = new Vector2(GetHorInput(), GetVerInput());
            onInteraction?.Invoke();
        }
        m_Animator.SetFloat("isRunning", Mathf.Abs(moveInput.x));
        bool isGrounded = m_CharacterController.IsGrounded;
        m_Animator.SetBool("isGrounded", isGrounded);
        if (canJump)
        {
            PlayerJump(moveInput.y);
        }
        else
        {
            if(isGrounded && moveInput.y <= 0)
            {
                canJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        m_CharacterController.Move(moveInput.x * m_CharacterController.GetSpeed() * Time.fixedDeltaTime, false, isJumping);
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

    private void PlayerJump(float _jumpInput)
    {
        if(_jumpInput > 0)
        {
            canJump = false;
            jumpAudio.Play();
            StartCoroutine(RunJumpAction(m_CharacterController.MaxJumpTime));
        }
    }

    private IEnumerator RunJumpAction(float _jumpTime)
    {
        float timer = 0;
        isJumping = true;
        m_Animator.SetBool("isJumping", isJumping);
        while (timer < _jumpTime)
        {
            if(moveInput.y > 0)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            else
            {
                break;
            }
        }
        isJumping = false;
        m_Animator.SetBool("isJumping", isJumping);
    }

    private void PlayDeathAudio()
    {
        deathAudio.Play();
    }

    private void SpawnDeathParticles()
    {
        GameObject deathExplosion = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
    }
}
