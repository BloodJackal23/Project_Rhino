using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] Animator[] doorAnimators;
    [SerializeField] Collider2D collider;
    [SerializeField] ToggleSwitch[] switches;
    [SerializeField] bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!collider)
        {
            collider = GetComponent<Collider2D>();
        }

        if(switches.Length > 0)
        {
            SubscribeToSwitches();
        }
        else
        {
            Debug.LogError(gameObject.name + " is not subscribed to any switches! This means that this interactable can't be controlled");
        }

        OperateGate();
    }

    private void SubscribeToSwitches()
    {
        foreach(ToggleSwitch _switch in switches)
        {
            _switch.turnedOnDelegate += ToggleGate;
            _switch.turnedOffDelegate += ToggleGate;
        }
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
