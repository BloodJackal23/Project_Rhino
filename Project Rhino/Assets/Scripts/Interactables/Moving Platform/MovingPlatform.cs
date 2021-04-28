using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.IsTouchingLayers(GetComponent<Collider2D>(), targetMask)) 
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
