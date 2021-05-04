using UnityEngine;

public class BossAI_DecisionMaker : MonoBehaviour
{
    [Header("Members")]
    [SerializeField] private BossAI_ProximitySensor m_sensor;
    [Space]

    [Header("Boss Weapons")]
    [SerializeField] private LaserBeam m_laserBeam;
    [Space]

    [Header("Attributes")]
    [SerializeField] private float actionCooldownTime = 2f;

    private bool targetInRange, takingAction, actionCooldown;
    private float actionCooldownTimer = 0;
    public enum CombatAction { None, FireBombs, FireLaser}
    public CombatAction currentCombatAction = CombatAction.None;

    private void Start()
    {
        m_laserBeam.onLaserEnd += OnLaserBeamEnd;
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
        if (actionCooldown)
            WaitForActionCooldown();
        else
        {
            if(targetInRange && !takingAction)
                FireLaserBeam();
        }
    }

    private void OnTargetDetected()
    {
        targetInRange = true;
        FireLaserBeam();
        //SelectRandomCombatAction();
    }

    private void OnTargetLost()
    {
        targetInRange = false;
        currentCombatAction = CombatAction.None;
    }

    private void SelectRandomCombatAction()
    {
        int rand = Random.Range(0, 2);

        switch(rand)
        {
            case 0:
                currentCombatAction = CombatAction.FireBombs;
                break;
            case 1:
                FireLaserBeam();
                break;
        }
    }

    private void FireLaserBeam()
    {
        currentCombatAction = CombatAction.FireLaser;
        takingAction = true;
        m_laserBeam.StartLaserBeam();
    }

    private void OnLaserBeamEnd()
    {
        actionCooldown = true;
        takingAction = false;
        currentCombatAction = CombatAction.None;
    }

    private void WaitForActionCooldown()
    {
        if(actionCooldownTimer < actionCooldownTime)
            actionCooldownTimer += Time.deltaTime;
        else
        {
            actionCooldownTimer = 0;
            currentCombatAction = CombatAction.None;
            actionCooldown = false;
        }
    }
}
