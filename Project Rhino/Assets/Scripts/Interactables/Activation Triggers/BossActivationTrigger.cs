using UnityEngine;

public class BossActivationTrigger : ActivationTrigger
{
    [SerializeField] private BossAI_Controller bossCtrl;
    [SerializeField] private bool followPlayer = true;
    [SerializeField] HazardData laserHazardData, redBombHazardData;
    [SerializeField] private LayerMask targetMask;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!bossCtrl)
                bossCtrl = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI_Controller>();
            bossCtrl.gameObject.SetActive(true);
            if (followPlayer)
            {
                bossCtrl.targetTransform = collision.transform;
                bossCtrl.BossDM.runDM = true;
                bossCtrl.BossDM.SetNewHazardData(laserHazardData, redBombHazardData);
            }
            else
            {
                bossCtrl.targetTransform = null;
                bossCtrl.BossDM.runDM = false;
            }

            Debug.Log("Player in the field");
            base.OnTriggerEnter2D(collision);
        }
    }

    private void OnValidate()
    {
        if (!followPlayer)
        {
            laserHazardData.ClearAllData();
            redBombHazardData.ClearAllData();
        }
    }
}
