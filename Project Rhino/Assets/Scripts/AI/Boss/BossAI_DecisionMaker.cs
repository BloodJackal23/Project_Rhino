using UnityEngine;

public class BossAI_DecisionMaker : MonoBehaviour
{
    [Header("Members")]
    [SerializeField] private BossAI_ProximitySensor m_sensor;
    [Space]

    [Header("Boss Weapons")]
    [SerializeField] private LaserBeam m_laserBeam;
    [SerializeField] private BombBurstGun m_bombBurstGun;
    [Space]

    [Header("Attributes")]
    [SerializeField] private float actionCooldownTime = 2f;
    [Space]

    [Header("Debugging")]
    [SerializeField] private bool runDM = true;
    private bool targetInRange, takingAction, waitForCooldown = true;
    private float actionCooldownTimer = 0;
    public enum CombatAction { None, FireBombs, FireLaser}
    public CombatAction currentCombatAction = CombatAction.None;

    private void Start()
    {
        m_laserBeam.onLaserEnd += OnCombatActionEnd;
        m_bombBurstGun.onBurstEnd += OnCombatActionEnd;
    }

    private void OnEnable()
    {
        if (!m_sensor)
            m_sensor = GetComponentInChildren<BossAI_ProximitySensor>();
        m_sensor.onTargetDetected += OnTargetDetected;
        m_sensor.onTargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        m_sensor.onTargetDetected -= OnTargetDetected;
        m_sensor.onTargetLost -= OnTargetLost;
    }

    private void Update()
    {
        if(runDM)
        {
            if (waitForCooldown)
                WaitForActionCooldown();
            else
            {
                if (targetInRange && !takingAction)
                {
                    takingAction = true;
                    SelectRandomCombatAction();
                }
            }
        }
    }

    private void OnTargetDetected()
    {
        targetInRange = true;
    }

    private void OnTargetLost()
    {
        targetInRange = false;
    }

    private void SelectRandomCombatAction()
    {
        int rand = Random.Range(0, 2);
        switch(rand)
        {
            case 0:
                FireBombsBurst();
                break;
            case 1:
                FireLaserBeam();
                break;
        }
    }

    private void FireLaserBeam()
    {
        currentCombatAction = CombatAction.FireLaser;
        m_laserBeam.StartLaserBeam();
    }

    private void FireBombsBurst()
    {
        currentCombatAction = CombatAction.FireBombs;
        m_bombBurstGun.FireBurst();
    }

    private void OnCombatActionEnd()
    {
        waitForCooldown = true;
        actionCooldownTimer = 0;
        takingAction = false;
        currentCombatAction = CombatAction.None;
    }

    private void WaitForActionCooldown()
    {
        if(actionCooldownTimer < actionCooldownTime)
            actionCooldownTimer += Time.deltaTime;
        else
            waitForCooldown = false;
    }
}
