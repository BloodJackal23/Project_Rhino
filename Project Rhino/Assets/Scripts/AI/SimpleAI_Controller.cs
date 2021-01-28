using UnityEngine;

public class SimpleAI_Controller : MonoBehaviour
{
    #region Members and Children
    [Header("Members and Children")]
    [SerializeField] protected CharacterController2D m_CharacterController;
    [Space]
    #endregion

    #region Move Direction
    [Header("Move Direction")]
    public bool startWithRightDirection = true;
    public bool isMovingRight { get; private set; }
    protected enum MoveDirection { Right, Left}
    protected Vector2 currentMoveDirection;
    [Space]
    #endregion

    #region Wall Checks
    [Header("Wall Checks")]
    [SerializeField] protected float wallCheckThreshold = .5f;
    public LayerMask targetWallMask;
    [Space]
    #endregion

    #region Gap Checks
    [Header("Gap Checks")]
    [SerializeField] protected Transform rightGapCheck;
    [SerializeField] protected Transform leftGapCheck;
    [SerializeField] protected float gapThreshold = .5f; //Anything beyond that is considered a gap
    protected Transform activeGapCheck;
    protected LayerMask groundMask;
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        groundMask = m_CharacterController.GetGroundMask();
        isMovingRight = startWithRightDirection;
        InitNewDirection(isMovingRight);
    }

    protected virtual void FixedUpdate()
    {
        CheckForGaps();
        CheckForWalls();
        m_CharacterController.Move(currentMoveDirection.x * m_CharacterController.GetSpeed() * Time.fixedDeltaTime, false, false);
    }

    private void InitNewDirection(bool _isMovingRIght)
    {
        SetActiveGapCheck(_isMovingRIght);
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

    private void SwitchDirection()
    {
        isMovingRight = !isMovingRight;
        InitNewDirection(isMovingRight);
    }

    private void SetActiveGapCheck(bool _isMovingRIght)
    {
        if(_isMovingRIght)
        {
            activeGapCheck = rightGapCheck;
        }
        else
        {
            activeGapCheck = leftGapCheck;
        }
    }

    private void CheckForGaps()
    {
        RaycastHit2D hit = Physics2D.Raycast(activeGapCheck.position, Vector2.down, gapThreshold, groundMask);
        if(hit.collider == null)
        {
            SwitchDirection();
        }
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
