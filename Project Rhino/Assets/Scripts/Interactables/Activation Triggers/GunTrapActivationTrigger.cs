using UnityEngine;

public class GunTrapActivationTrigger : ActivationTrigger
{
    [SerializeField] private GunTrap[] gunTraps;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsTargetTag("Player", collision))
        {
            foreach (GunTrap gunTrap in gunTraps)
            {
                gunTrap.SetActive(true);
            }
        }
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTargetTag("Player", collision))
        {
            foreach (GunTrap gunTrap in gunTraps)
            {
                gunTrap.SetActive(false);
            }
        }
    }
}
