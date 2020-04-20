using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController2D m_controller;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private HealthSystem m_healthSystem;
    private PlayerItemPicker m_playerItemPicker;

    public float runSpeed = 40f;

    float m_horizontalMove = 0f;
    bool m_jump = false;
    bool m_crouch = false;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_controller = GetComponent<CharacterController2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_healthSystem = GetComponent<HealthSystem>();
        m_playerItemPicker = gameObject.AddComponent(typeof(PlayerItemPicker)) as PlayerItemPicker;

        m_healthSystem.OnDeathEvent.AddListener(OnDeath);
    }

    private void OnDestroy()
    {
        m_healthSystem.OnDeathEvent.RemoveListener(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_healthSystem.Health == 0) { return; }

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        m_horizontalMove = move.x * runSpeed;

        m_animator.SetFloat("velocityX", Mathf.Abs(m_horizontalMove));

        if (move.x > 0.01f)
        {
            if (m_spriteRenderer.flipX == true)
            {
                m_spriteRenderer.flipX = false;
            }
        }
        else if (move.x < -0.01f)
        {
            if (m_spriteRenderer.flipX == false)
            {
                m_spriteRenderer.flipX = true;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            m_jump = true;
            m_animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            m_crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            m_crouch = false;
        }
    }

    public void OnLanding()
    {
        m_animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        m_animator.SetBool("IsCrouching", isCrouching);
    }

    public void OnDeath()
    {
        m_animator.Play("Player_Die");
    }

    void FixedUpdate()
    {
        // Move our character
        m_controller.Move(m_horizontalMove * Time.fixedDeltaTime, m_crouch, m_jump);
        m_jump = false;
    }
}
