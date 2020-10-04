using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    bool isOn = false;
    public delegate void OnTurnedOn();
    public OnTurnedOn turnedOnDelegate;

    public delegate void OnTurnedOff();
    public OnTurnedOff turnedOffDelegate;

    [SerializeField] Animator animator;

    private void Start()
    {
        if(!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().interactionDelegate += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().interactionDelegate = null;
        }
    }

    void Interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            isOn = !isOn;
            animator.SetBool("isOn", isOn);
            if(isOn)
            {
                turnedOnDelegate?.Invoke();
            }
            else
            {
                turnedOffDelegate?.Invoke();
            }
        }
    }
}
