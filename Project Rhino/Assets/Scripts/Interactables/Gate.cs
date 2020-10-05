using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] Animator[] doorAnimators;
    [SerializeField] Collider2D collider;
    [SerializeField] ToggleSwitch activatingSwitch;
    [SerializeField] bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!collider)
        {
            collider = GetComponent<Collider2D>();
        }

        activatingSwitch.turnedOnDelegate += ToggleGate;
        activatingSwitch.turnedOffDelegate += ToggleGate;

        OperateGate();
    }

    void OperateGate()
    {
        collider.enabled = !isOpened;
        foreach (Animator animator in doorAnimators)
        {
            animator.SetBool("opened", isOpened);
        }
    }

    public void ToggleGate()
    {
        isOpened = !isOpened;
        OperateGate();
    }
}
