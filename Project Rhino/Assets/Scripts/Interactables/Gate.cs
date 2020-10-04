using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Collider2D collider;
    [SerializeField] ToggleSwitch activatingSwitch;
    bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!animator)
        {
            animator = GetComponent<Animator>();
        }

        if(!collider)
        {
            collider = GetComponent<Collider2D>();
        }

        activatingSwitch.turnedOnDelegate += ToggleGate;
        activatingSwitch.turnedOffDelegate += ToggleGate;
    }

    public void ToggleGate()
    {
        isOpened = !isOpened;
        collider.isTrigger = isOpened;
        animator.SetBool("opened", isOpened);
    }
}
