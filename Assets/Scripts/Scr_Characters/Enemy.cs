using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private PathFinder pathFinder;
    private BodyMovementWalking bodyMovementWalking;
    public EnemyController enemyController;
    public EnemyBaseZone PersonTerritory;

    public Hero CurrentTarget { get; set; }

    private DirectionTravel DirectionTravel_enemy;

    public int Health { get; set; }
    public int Attack { get; set; }
    private int MaxHealth;
    private int Index;

    private float Range;

    private bool Search;
    private bool InMove;
    private bool InAction;
    public bool InAggression { get; set; }
    private bool TargetInZone;

    private Vector2 PastPosition;

    [SerializeField]
    private CircleCollider2D RangZoneAggression;

    public List<Vector2> WayToTarget = new List<Vector2>();

    private void Awake()
    {
        bodyMovementWalking = GetComponent<BodyMovementWalking>();
        pathFinder = GetComponent<PathFinder>();
    }

    private void Start()
    {
        ResetPastPosition();
        Range = 3;
        RangZoneAggression.radius = Range;
        Health = 100;
        MaxHealth = Health;
        Attack = Random.Range(5, 10);
        enemyController.RegisterEnemy(this);
    }

    public void SetTarget(Hero newTarget)
    {
        CurrentTarget = newTarget;
    }

    public IEnumerator DetermineDirectionMovement()
    {
        enemyController.EnemyActionCompleted = false;
        InAction = true;
        OnUpdateDirections();
        yield return new WaitForSeconds(.2f);
        if (CurrentTarget == null) OrdinaryMovement();
        else MovementTowardsTarget();
        if (InMove) EnemyMove();
        InAction = false;
        enemyController.EnemyActionCompleted = true;
    }

    private void OrdinaryMovement()
    {
        Vector2 newTarget;
        if (!TargetInZone) newTarget = PersonTerritory.GetPoint();
        else newTarget = PastPosition;
        Search = TargetMovementCheck(newTarget);
        if (Search)
        {
            TargetInZone = true;
            SearchPath(newTarget);
            PastPosition = newTarget;
            Index = WayToTarget.Count - 1;
        }
        else Index--;
        if (WayToTarget.Count > 0 && Index >= 0) DeterminationDirection();
        else
        {
            //TODO: Заменить скип на продолжение пути
            ResetPastPosition();
            TargetInZone = false;
        }
    }

    private void MovementTowardsTarget()
    {
        Search = TargetMovementCheck(CurrentTarget.transform.position);
        if (Search)
        {
            TargetInZone = false;
            SearchPath(CurrentTarget.transform.position);
            Index = WayToTarget.Count - 1;
        }
        else Index--;
        if (WayToTarget.Count > 0 && Index >= 0) DeterminationDirection();
    }

    private bool TargetMovementCheck(Vector2 currentPosition)
    {
        if (PastPosition != currentPosition)
        {
            PastPosition = currentPosition;
            return true;
        }
        else
        {
            PastPosition = currentPosition;
            return false;
        }
    }

    private void SearchPath(Vector2 position)
    {
        WayToTarget = pathFinder.GetPath(position);
    }

    private void DeterminationDirection()
    {
        if (WayToTarget[Index].x < gameObject.transform.position.x ||
            WayToTarget[Index].x > gameObject.transform.position.x) DirectionTravel_enemy = WayToTarget[Index].x < gameObject.transform.position.x ? DirectionTravel.West : DirectionTravel.East;
        else DirectionTravel_enemy = WayToTarget[Index].y > gameObject.transform.position.y ? DirectionTravel.North : DirectionTravel.South;
        InMove = true;
    }

    private void EnemyMove()
    {
        bool move = true;
        if (DirectionTravel_enemy == DirectionTravel.North) move = bodyMovementWalking.StepNorth();
        if (DirectionTravel_enemy == DirectionTravel.South) move = bodyMovementWalking.StepSouth();
        if (DirectionTravel_enemy == DirectionTravel.East) move = bodyMovementWalking.StepEast();
        if (DirectionTravel_enemy == DirectionTravel.West) move = bodyMovementWalking.StepWest();
        if (!move) EnemyAttack();
        InMove = false;
    }

    public void OnUpdateDirections()
    {
        StartCoroutine(bodyMovementWalking.UpdateDirections());
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) HasDied();
    }

    private void EnemyAttack()
    {
        if (CurrentTarget != null) CurrentTarget.TakeDamage(Attack);
    }

    public bool SetNextActionEnemies()
    {
        return InAction;
    }

    private void ResetPastPosition()
    {
        PastPosition = new Vector2(9.404f, 9.404f);
    }

    public void HasDied()
    {
        Debug.Log("enemy_Die_d");
    }
}