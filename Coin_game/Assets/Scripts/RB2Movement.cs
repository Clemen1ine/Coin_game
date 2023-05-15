using UnityEngine;

public class Rb2Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _moveVelocity;
    private Animator _animator;
    private SwordAttack _swordAttack;
    private bool _canMove = true;
    private SpriteRenderer _spriteRenderer;
    private Transform _characterHeadTransform; // Separate transform for the character's head

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _swordAttack = GetComponentInChildren<SwordAttack>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _characterHeadTransform = transform.Find("Character/Head"); // Update with the correct path to the character's head
    }

    private void Update()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveVelocity = _moveInput.normalized * speed;

        if (_moveVelocity != Vector2.zero)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

        if (_moveInput.x > 0)
        {
            _animator.SetBool("isFacingRight", true);
            _spriteRenderer.flipX = false;
        }
        else if (_moveInput.x < 0)
        {
            _animator.SetBool("isFacingRight", false);
            _spriteRenderer.flipX = true;
        }

        if (Input.GetMouseButtonDown(0) && _canMove)
        {
            _animator.SetTrigger("SwordAttack");
            LockMovement();
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveVelocity * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        print("attack");
        _animator.SetTrigger("SwordAttack");
        LockMovement();
    }

    void OnFire()
    {
        _animator.SetTrigger("SwordAttack");
    }

    public void EndAttack()
    {
        UnlockMovement();
        
        if (_swordAttack != null)
        {
            _swordAttack.StopAttack();
        }
    }
    public void LockMovement()
    {
        _canMove = false;
    }

    public void PerformSwordAttack()
    {
        print("attack");
        LockMovement();
        _swordAttack.AttackFront();
    }

    public void UnlockMovement()
    {
        _canMove = true;
    }
}