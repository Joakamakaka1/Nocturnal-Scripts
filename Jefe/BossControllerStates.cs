using System.Collections;
using UnityEngine;

public class BossControllerStates : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private BossController bossController;

    private bool canAttack = true;
    private bool isAttacking = false; 

    private enum State
    {
        Roaming,
        Chasing,
        Attacking,
        Idle
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private Vector2 startingPosition;
    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Idle;
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
            case State.Idle:
                Idle();
                break;
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

    private void Idle()
    {
        bossController.SetIdle();
        enemyPathfinding.StopMoving();

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < chaseRange)
        {
            state = State.Chasing;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPosition);
        bossController.SetRunning(true);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < chaseRange)
        {
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
        bossController.SetRunning(true);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > chaseRange)
        {
            state = State.Idle;
            bossController.SetIdle();
        }
        else if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
    }

    private void Attacking()
    {
        if (isAttacking) return;

        isAttacking = true;
        bossController.SetRunning(false);
        bossController.Attack();
        enemyPathfinding.StopMoving();
        bossController.SetAttacking(true);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Chasing;
            bossController.SetAttacking(false);
            bossController.SetRunning(true);
            isAttacking = false; // Permitir otro ataque
        }
        else if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    public void ApplyDamage()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            PlayerVida.Instance.TakeDamage(1, transform);
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        bossController.SetAttacking(false);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        else
        {
            state = State.Chasing;
        }

        isAttacking = false;
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

    /*
    public void Die()
    {
        bossController.Die();
    }
    */
}