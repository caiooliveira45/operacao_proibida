using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D _movementController;
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField]
    private TrailRenderer _trail;
    [SerializeField]
    private GameObject _pauseMenu;

    public float runSpeed = 40f;
    public float maxFallingSpeed = 40f;
    public float dashPower;
    public float dashTime;
    public float dashCooldown;

    private float _horizontalMove = 0f;
    // private float _verticalMove = 0f;
    private Vector2 _inputDirections;
    private bool _canMove = true;
    private bool _isJumping = false;
    private bool _canDash = true;
    private bool _isDashing = false;
    private bool _dashPressCD = false;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _movementController = gameObject.GetComponent<CharacterController2D>();

        _inputDirections = new Vector2(0, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isDashing)
            return;

        _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -maxFallingSpeed, maxFallingSpeed));

        _horizontalMove = InputManager.instance.MoveInput.x * runSpeed;
        _inputDirections.x = InputManager.instance.MoveInput.x;
        _inputDirections.y = InputManager.instance.MoveInput.y;

        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (_canMove)
            Inputs();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
            return;

        if (_canMove)
        {
            _movementController.Move(_horizontalMove * Time.fixedDeltaTime, false, _isJumping);
            _isJumping = false;
        }
    }

    public void OnLanding()
    {
        _animator.SetBool("IsJumping", false);
        _canDash = true;
    }

    public void OnDying()
    {
        _rb.velocity = Vector2.zero;
        _animator.SetBool("IsDying", true);
        _canMove = false;
        _canDash = false;
    }

    public void OnRespawning()
    {
        _rb.velocity = Vector2.zero;
        RespawnManager.instance.RespawnPlayer();
        _animator.SetBool("IsDying", false);
        _canMove = true;
        _canDash = true;
    }

    private void Inputs()
    {
        if (InputManager.instance.JumpPressed)
        {
            _isJumping = true;
            _animator.SetBool("IsJumping", true);
        }

        if (InputManager.instance.DashPressed && !_dashPressCD && _canDash)
        {
            StartCoroutine(Dash());
            _dashPressCD = true;
        }

        if (InputManager.instance.PausePressed)
        {
            if (!PauseManager.instance.IsPaused)
            {
                //Pause();
                PauseManager.instance.Pause();
                _pauseMenu.SetActive(true);
            }
        }
        else if (InputManager.instance.UnpauseInput)
        {
            if (PauseManager.instance.IsPaused)
            {
                //Unpause();
                PauseManager.instance.Unpause();
                _pauseMenu.SetActive(false);
            }

        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;
        if (_inputDirections.x == 0f && _inputDirections.y == 0f)
        {
            float baseValue = transform.localScale.x;
            if (baseValue < 0)
                baseValue = -1;
            else
                baseValue = 1;
            _rb.velocity = new Vector2(baseValue * dashPower, 0f);
        }
        else _rb.velocity = new Vector2(_inputDirections.x * dashPower, _inputDirections.y * dashPower);
        _trail.emitting = true;
        
        yield return new WaitForSeconds(dashTime);
        _trail.emitting = false;
        _rb.gravityScale = originalGravity;
        _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y/2);
        _isDashing = false;
        
        yield return new WaitForSeconds(dashCooldown);
        if (_movementController.m_Grounded)
            _canDash = true;
        _dashPressCD = false;
    }

    private IEnumerator DieAndRespawn()
    {
        OnDying();
        yield return new WaitForSeconds(1.0f);
        OnRespawning();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Danger" && _canMove)
        {
            StartCoroutine(DieAndRespawn());
        }
    }
}
