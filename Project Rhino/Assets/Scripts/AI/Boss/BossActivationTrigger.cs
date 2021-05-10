using UnityEngine;

public class BossActivationTrigger : MonoBehaviour
{
    [SerializeField] private bool destroyOnActivation = true, followPlayer = true;
    [SerializeField] HazardData laserHazardData, redBombHazardData, blueBombHazardData;
    [SerializeField] private LayerMask targetMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            BossAI_Controller bossCtrl = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI_Controller>();
            if (followPlayer)
            {
                bossCtrl.targetTransform = collision.transform;
                bossCtrl.BossDM.runDM = true;
                bossCtrl.BossDM.SetNewHazardData(laserHazardData, redBombHazardData, blueBombHazardData);
            }
            else
            {
                bossCtrl.targetTransform = null;
                bossCtrl.BossDM.runDM = false;
            }     

            Debug.Log("Player in the field");
            if(destroyOnActivation)
                Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (!followPlayer)
        {
            laserHazardData.ClearAllData();
            redBombHazardData.ClearAllData();
            blueBombHazardData.ClearAllData();
        }
    }
}
