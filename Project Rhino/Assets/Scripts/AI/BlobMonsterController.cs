using UnityEngine;

public partial class BlobMonsterController : SimpleAI_Controller
{
    #region Members and Children
    [Header("Members and Children")]
    [SerializeField] private StompHitbox stompHitbox;
    [SerializeField] private PlayerHazard damageBox;
    [SerializeField] protected bool startWithRightDirection = true;
    [Space]
    #endregion

    #region Gap Checks
    [Header("Gap Checks")]
    [SerializeField] protected Transform rightGapCheck;
    [SerializeField] protected Transform leftGapCheck;
    [SerializeField] protected float gapThreshold = .5f; //Anything beyond that is considered a gap
    protected Transform activeGapCheck;
    protected LayerMask groundMask;
    [Space]
    #endregion

    #region Particle Effects
    [Header("Effects")]
    [SerializeField] private GameObject deathExplosionPrefab;
    #endregion

    #region Sound Effects
    [SerializeField] private GameObject hitAudioPrefab;
    [Space]
    #endregion

    #region Hazard Data
    [SerializeField] HazardData hazardData;
    #endregion

    private void OnEnable()
    {
        stompHitbox.onStomp += PlayHitAudio;
        stompHitbox.onStomp += SpawnDeathParticles;
    }

    private void Start()
    {
        groundMask = m_CharacterController.GetGroundMask();
        isMovingRight = startWithRightDirection;
        InitNewDirection(isMovingRight);
        damageBox.SetHazardData(hazardData);
    }

    protected override void FixedUpdate()
    {
        CheckForGaps();
        base.FixedUpdate();
    }

    private void OnDisable()
    {
        stompHitbox.onStomp = null;
    }

    private void SetActiveGapCheck(bool _isMovingRIght)
    {
        if (_isMovingRIght)
        {
            activeGapCheck = rightGapCheck;
        }
        else
        {
            activeGapCheck = leftGapCheck;
        }
    }

    protected override void InitNewDirection(bool _isMovingRIght)
    {
        SetActiveGapCheck(_isMovingRIght);
        base.InitNewDirection(_isMovingRIght);
    }

    private void CheckForGaps()
    {
        RaycastHit2D hit = Physics2D.Raycast(activeGapCheck.position, Vector2.down, gapThreshold, groundMask);
        if (hit.collider == null)
        {
            SwitchDirection();
        }
    }

    private void PlayHitAudio()
    {
        AudioSource hitAudio = Instantiate(hitAudioPrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        hitAudio.Play();
        Destroy(hitAudio.gameObject, .5f);
    }

    private void SpawnDeathParticles()
    {
        Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
    }
}
