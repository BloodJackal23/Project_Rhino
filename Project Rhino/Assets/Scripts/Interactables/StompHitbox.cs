using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StompHitbox : MonoBehaviour
{
    private Collider2D m_Collider;

    [SerializeField] private bool affectParent = true;
    [SerializeField] private LayerMask whatCanStompMe;

    [SerializeField] private float playerBounceForce = 300f;

    private void Start()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_Collider.IsTouchingLayers(whatCanStompMe)) 
        {
            transform.parent.gameObject.SetActive(!affectParent);
            if(collision.gameObject.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                CharacterController2D characterController = collision.GetComponent<CharacterController2D>();
                characterController.AddJumpForceInstant(playerBounceForce);
            }
        }
    }
}
