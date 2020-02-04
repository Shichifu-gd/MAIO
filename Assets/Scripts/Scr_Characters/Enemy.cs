using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;
    private PathFinder pathFinder;
    public EnemyController enemyController;

    private DirectionTravel DirectionTravel_enemy;

    public int Health { get; set; }
    public int Attack { get; set; }
    private int MaxHealth;
    private int Index;

    private float Range;

    private bool InMove;
    private bool InAction;
    public bool InAggression { get; set; }

    public Hero CurrentTarget { get; set; }
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
        Range = 8;
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
        if (CurrentTarget == null)
        {
            int index = Random.Range(0, 4);
            if (index == 0) DirectionTravel_enemy = DirectionTravel.North;
            if (index == 1) DirectionTravel_enemy = DirectionTravel.South;
            if (index == 2) DirectionTravel_enemy = DirectionTravel.East;
            if (index == 3) DirectionTravel_enemy = DirectionTravel.West;
            InMove = true;
        }
        else
        {
            SearchPath();
            //TODO: исправить атаку !!
            if (WayToTarget.Count > 0)
            {
                Index = WayToTarget.Count - 1;
                if (WayToTarget[Index].x < gameObject.transform.position.x ||
                    WayToTarget[Index].x > gameObject.transform.position.x) DirectionTravel_enemy = WayToTarget[Index].x < gameObject.transform.position.x ? DirectionTravel.West : DirectionTravel.East;
                else DirectionTravel_enemy = WayToTarget[Index].y > gameObject.transform.position.y ? DirectionTravel.North : DirectionTravel.South;
                InMove = true;
            }
        }
        if (InMove) EnemyMove();
        InAction = false;
        enemyController.EnemyActionCompleted = true;
    }

    private void SearchPath()
    {
        WayToTarget = pathFinder.GetPath(CurrentTarget.transform);
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

    public void HasDied()
    {
        Debug.Log("enemy_Die_d");
    }
}