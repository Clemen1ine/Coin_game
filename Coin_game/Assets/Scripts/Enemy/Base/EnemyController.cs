using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private Transform _target;

    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    public Transform homePos;

    private bool canAttack = true;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private int damageAmount = 5;

    private void Start()
    {
        homePos.parent = null;
        _animator = GetComponent<Animator>();
        _target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(_target.position, transform.position) <= maxRange && Vector3.Distance(_target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(_target.position, transform.position) >= maxRange)
        {
            GoHome();
        }
    }

    public void FollowPlayer()
    {
        _animator.SetBool("isMoving", true);
        _animator.SetFloat("moveX", (_target.position.x - transform.position.x));
        _animator.SetFloat("moveY", (_target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, speed * Time.deltaTime);
    }

    public void GoHome()
    {
        _animator.SetFloat("moveX", (_target.position.x - transform.position.x));
        _animator.SetFloat("moveY", (_target.position.y - transform.position.y));

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            _animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (canAttack)
        {
            Health playerHealth = _target.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            canAttack = false;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}