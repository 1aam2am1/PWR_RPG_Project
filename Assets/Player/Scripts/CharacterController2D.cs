using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(1, 2)] [SerializeField] private float m_GroundSpeed = 1f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private Collider2D m_GroundCheckCollider;                  // A collider that will be checked for ground colision
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    const float k_GroundedMovment = .2f;                                        // How mach move down to check for ground colision
    const float k_CeilingMovment = .2f;                                         // How mach move up to check for celing colision;
    protected const float shellRadius = 0.01f;

    private bool m_Grounded;                                                    // Whether or not the player is grounded.

    protected ContactFilter2D contactFilter;

    private Rigidbody2D m_Rigidbody2D;

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

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        if (m_GroundCheckCollider == null)
            m_GroundCheckCollider = new Collider2D();

        if (m_CrouchDisableCollider == null)
            m_CrouchDisableCollider = new Collider2D();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        int count = m_GroundCheckCollider.Cast(Vector2.down, contactFilter, hitBuffer, k_GroundedMovment + shellRadius);

        for (int i = 0; i < count; i++)
        {
            if (hitBuffer[i].distance - shellRadius <= k_GroundedMovment)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
                break;
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch && m_wasCrouching)
        {
            int count = m_CrouchDisableCollider.Cast(Vector2.up, contactFilter, hitBuffer, k_CeilingMovment + shellRadius);
            // If the character has a ceiling preventing them from standing up, keep them crouching
            for (int i = 0; i < count; i++)
            {
                if (hitBuffer[i].distance - shellRadius <= k_CeilingMovment)
                {
                    crouch = true;
                    break;
                }
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
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

            //grunt niejest sliski
            //(move-velocity)*konst=>force
            if (m_Grounded)
            {
                m_Rigidbody2D.AddForce(new Vector2(move * m_GroundSpeed, 0f), ForceMode2D.Force);
            }
            else
            {
                m_Rigidbody2D.AddForce(new Vector2(move, 0f), ForceMode2D.Force);
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            //m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Force);
        }
    }

    private void OnDrawGizmos()
    {

    }
}
