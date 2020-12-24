using UnityEngine;

public class UI_Panel : MonoBehaviour
{
    [SerializeField] protected Animator m_Animator;

    virtual protected void OnValidate()
    {
        if (!m_Animator) m_Animator = GetComponent<Animator>();
    }
}
