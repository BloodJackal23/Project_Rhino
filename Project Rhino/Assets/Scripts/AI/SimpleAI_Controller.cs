using UnityEngine;

public class SimpleAI_Controller : MonoBehaviour
{
    #region Members and Children
    [Header("Members and Children")]
    [SerializeField] protected CharacterController2D m_CharacterController;
    #endregion

    #region Move Direction
    public bool isMovingRight { get; protected set; }
    protected Vector2 currentMoveDirection;
    [Space]
    #endregion

    #region Wall Checks
    [Header("Wall Checks")]
    [SerializeField] protected float wallCheckThreshold = .5f;
    [SerializeField] protected LayerMask targetWallMask;
    #endregion

    protected virtual void FixedUpdate()
    {
        CheckForWalls();
        m_CharacterController.Move(currentMoveDirection.x * m_CharacterController.GetSpeed() * Time.fixedDeltaTime, false, false);
    }

    protected virtual void InitNewDirection(bool _isMovingRIght)
    {
        SetCurrentMoveDirection(_isMovingRIght);
    }

    private void SetCurrentMoveDirection(bool _isMovingRIght)
    {
        if(_isMovingRIght)
        {
            currentMoveDirection = Vector2.right;
        }
        else
        {
            currentMoveDirection = Vector2.left;
        }
    }

    protected void SwitchDirection()
    {
        isMovingRight = !isMovingRight;
        InitNewDirection(isMovingRight);
    }

    private void CheckForWalls()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentMoveDirection, wallCheckThreshold, targetWallMask);
        if (hit.collider != null)
        {
            SwitchDirection();
        }
    }
}
