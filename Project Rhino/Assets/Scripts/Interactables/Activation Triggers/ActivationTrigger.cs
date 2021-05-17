using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActivationTrigger : MonoBehaviour
{
    [SerializeField] protected bool destroyOnActivation = true;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnActivation)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {

    }

    protected bool IsTargetTag(string _tag, Collider2D _collider)
    {
        if (_collider.gameObject.tag == _tag)
            return true;
        return false;
    }
}
