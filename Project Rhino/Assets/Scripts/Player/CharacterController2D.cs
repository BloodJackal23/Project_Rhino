using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
	[SerializeField] float speed = 10;
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private float m_JumpSpeed = 2;								// The jump speed of the player as they're holding the jump button
	[SerializeField] private float m_JumpTime = .6f;                             //The maximum amount of time the player can stay airborne while holding the jump button (ABU)
	private float jumpTimer = 0;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	[SerializeField] private SpriteRenderer m_SpriteRenderer;					//The sprite renderer attached to the actor (ABU)

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		if(!m_SpriteRenderer)
        {
			m_SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public float GetSpeed()
    {
		return speed;
    }

	public LayerMask GetGroundMask()
    {
		return m_WhatIsGround;
    }

	public void Move(float _move, bool _crouch, bool _jump)
	{
		// If crouching, check to see if the character can stand up
		if (!_crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				_crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (_crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				_move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(_move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (_move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				FlipSprite(); //Changed from Flip() to FlipSprite (ABU)
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (_move < 0 && m_FacingRight)
			{
				// ... flip the player.
				FlipSprite(); //Changed from Flip() to FlipSprite (ABU)
			}
		}
		AddJumpForceCont(_jump); //This adds vertical movement (aka jumping) for as long as the player is holding the jump button (ABU)
	}

	public bool IsGrounded()
    {
		return m_Grounded;
    }

	public void AddJumpForceInstant(float _jumpForce)
    {
		// Add a vertical force to the player.
		//m_Grounded = false;
		m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
		m_Rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
	}

	private void AddJumpForceCont(bool _jump) //(ABU)
    {
		if (_jump) //Is the player jumping at the moment?
		{
			if (jumpTimer < m_JumpTime)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpSpeed);
				jumpTimer += Time.deltaTime;
			}
		}
		else
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
			jumpTimer = 0;
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void FlipSprite() //(ABU)
    {
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		//Flips the sprite renderer when the actor is facing the opposite direction
		m_SpriteRenderer.flipX = !m_FacingRight;
	}
}
