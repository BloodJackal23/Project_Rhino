using UnityEngine;

public class BossAI_DecisionMaker : MonoBehaviour
{
    [Header("Members")]
    [SerializeField] private BossAI_ProximitySensor m_sensor;
    [Space]

    [Header("Boss Weapons")]
    [SerializeField] private LaserBeam m_laserBeam;

    public enum CombatAction { None, FireBombs, FireLaser}
    public CombatAction currentCombatAction = CombatAction.None;

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

    private void OnTargetDetected()
    {
        FireLaserBeam();
        //SelectRandomCombatAction();
    }

    private void OnTargetLost()
    {
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
        m_laserBeam.StartLaserBeam();
    }
}
