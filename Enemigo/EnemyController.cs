using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float chaseSpeed = 2f;

    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Chasing,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private Vector2 startingPosition;

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Chasing:
                Chasing();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < chaseRange && !(enemyType is Grape))
        {
            FindTarget();
            state = State.Chasing;
        }

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat || Vector2.Distance(transform.position, roamPosition) < 1f)
        {
            roamPosition = GetRoamingPosition();
        }

    }

    private void Chasing()
    {
        enemyPathfinding.MoveTo(PlayerController.Instance.transform.position);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > chaseRange)
        {
            state = State.Roaming;
            roamPosition = GetRoamingPosition();
        }

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (enemyType is SlimeAttack slime)
        {
            slime.Attack();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
            roamPosition = GetRoamingPosition();
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (enemyType is Grape)
            {
                (enemyType as Grape).SpawnProjectile();
            }

            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    public static Vector2 GetRandomDir()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void FindTarget()
    {
        float targetRange = 50f;
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < targetRange)
        {
            state = State.Chasing;
        }
    }
}