using UnityEngine;

public class SimpleAI_Controller : MonoBehaviour
{
    [SerializeField] private CharacterController2D m_CharacterController;

    private enum MoveDirection { Right, Left}
    private Vector2 currentMoveDirection;
    [SerializeField] private bool isMovingRight = true;

    public LayerMask targetWallMask;
    [SerializeField] private float wallCheckThreshold = .5f;

    [SerializeField] private Transform rightGapCheck;
    [SerializeField] private Transform leftGapCheck;
    [SerializeField] private float gapThreshold = .5f; //Anything beyond that is considered a gap
    private Transform activeGapCheck;
    private LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        if(!m_CharacterController)
        {
            m_CharacterController = GetComponent<CharacterController2D>();
        }
        groundMask = m_CharacterController.GetGroundMask();
        InitNewDirection(isMovingRight);
    }

    private void FixedUpdate()
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
