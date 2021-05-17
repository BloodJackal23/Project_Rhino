using UnityEngine;

public class PlatformActivationTrigger : ActivationTrigger
{
    [SerializeField] private MovingPlatformGroup[] platformGroups;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTargetTag("Player", collision))
        {
            foreach (MovingPlatformGroup group in platformGroups)
                group.StartPlatformLoop();
        }
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTargetTag("Player", collision))
        {
            foreach (MovingPlatformGroup group in platformGroups)
                group.StopPlatformLoop();
        }
        base.OnTriggerExit2D(collision);
    }
}
