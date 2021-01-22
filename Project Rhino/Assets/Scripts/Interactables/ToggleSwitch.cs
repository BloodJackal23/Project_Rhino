using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    bool isOn = false;
    public delegate void OnTurnedOn();
    public OnTurnedOn turnedOnDelegate;

    public delegate void OnTurnedOff();
    public OnTurnedOff turnedOffDelegate;

    [SerializeField] Animator m_Animator;

    private void Start()
    {
        if(!m_Animator)
        {
            m_Animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().onInteraction += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().onInteraction = null;
        }
    }

    void Interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            isOn = !isOn;
            m_Animator.SetBool("isOn", isOn);
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
