using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
    }
    private EnemyPathfinding pathfinding;
    private Vector2 startingPosition;
    private Vector2 roamPosition;
    private State state;

    private void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }
    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        switch (state)
        {
            default:

            case State.Roaming:
                state = State.Roaming;
                pathfinding.MoveTo(roamPosition);
                float reachedPositionDistance = 1f;
                if (Vector2.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    roamPosition = GetRoamingPosition();
                }
                FindTarget();
                break;

             case State.ChaseTarget:
                pathfinding.MoveTo(PlayerController.Instance.transform.position);           
                break;
        }
        

       
    }

    private Vector2 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    public static Vector2 GetRandomDir()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void FindTarget()
    {
        float targetRange = 50f;
        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < targetRange){
            state = State.ChaseTarget;
        }
    }

}
