using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.Translate(new Vector2(rb.velocity.x, 0) * Time.deltaTime, transform);
    }
}
