using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StompHitbox : MonoBehaviour
{
    #region Delegates
    public delegate void OnStomp();
    public OnStomp onStomp;
    #endregion

    #region Members and Children
    [Header("Members and Children")]
    private Collider2D m_Collider;
    [Space]
    #endregion

    #region Attributes
    [Header("Attributes")]
    [SerializeField] private bool affectParent = true;
    [SerializeField] private LayerMask whatCanStompMe;
    [SerializeField] private float playerBounceForce = 300f;
    #endregion

    private void Start()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_Collider.IsTouchingLayers(whatCanStompMe)) 
        {
            if(collision.gameObject.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                CharacterController2D characterController = collision.GetComponent<CharacterController2D>();
                characterController.AddJumpForceInstant(playerBounceForce);
                onStomp?.Invoke();
            }
            transform.parent.gameObject.SetActive(!affectParent);
        }
    }
}
