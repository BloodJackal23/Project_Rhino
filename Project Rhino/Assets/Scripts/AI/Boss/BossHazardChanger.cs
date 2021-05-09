using UnityEngine;

public class BossHazardChanger : MonoBehaviour
{
    [SerializeField] HazardData laserHazardData, redBombHazardData, blueBombHazardData;
    [SerializeField] private Vector2 dimensions = new Vector2(5f, 20f);
    [SerializeField] private LayerMask targetMask;

    private void FixedUpdate()
    {
        Collider2D bossCollider = Physics2D.OverlapBox(transform.position, dimensions, 0, targetMask);
        if(bossCollider)
        {
            bossCollider.GetComponent<BossAI_DecisionMaker>().SetNewHazardData(laserHazardData, redBombHazardData, blueBombHazardData);
            Destroy(gameObject);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, dimensions);
    }
}
