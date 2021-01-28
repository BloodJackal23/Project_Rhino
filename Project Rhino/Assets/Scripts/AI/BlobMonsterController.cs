using UnityEngine;

public partial class BlobMonsterController : SimpleAI_Controller
{
    #region Members and Children
    [Header("Members and Children")]
    [SerializeField] private StompHitbox stompHitbox;
    [SerializeField] private PlayerHazard damageBox;
    [Space]
    #endregion

    #region Sound Effects
    [Header("Sound Effects")]
    [SerializeField] private GameObject hitAudioPrefab;
    [Space]
    #endregion

    #region Hazard Data
    [SerializeField] HazardData hazardData;
    #endregion

    private void OnEnable()
    {
        stompHitbox.onStomp += PlayHitAudio;
    }

    protected override void Start()
    {
        base.Start();
        hazardData.SetupHazard(damageBox);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnDisable()
    {
        stompHitbox.onStomp = null;
    }

    private void PlayHitAudio()
    {
        AudioSource hitAudio = Instantiate(hitAudioPrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        hitAudio.Play();
        Destroy(hitAudio.gameObject, .5f);
    }
}
