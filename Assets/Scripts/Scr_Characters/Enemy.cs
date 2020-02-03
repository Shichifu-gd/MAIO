using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;
    private PathFinder pathFinder;

    private DirectionTravel DirectionTravel_enemy;

    public int Health { get; set; }
    public int Attack { get; set; }
    private int MaxHealth;
    private int Index;

    private bool InMove;
    public bool InAggression { get; set; }

    public Hero CurrentTarget;
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
        Health = 100;
        MaxHealth = Health;
        Attack = Random.Range(5, 10);
    }

    public void SetTarget(Hero newTarget)
    {
        CurrentTarget = newTarget;
    }

    public void DetermineDirectionMovement()
    {
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
            if (WayToTarget.Count > 1)
            {
                Index = WayToTarget.Count - 1;
                if (WayToTarget[Index].x < gameObject.transform.position.x ||
                    WayToTarget[Index].x > gameObject.transform.position.x) DirectionTravel_enemy = WayToTarget[Index].x < gameObject.transform.position.x ? DirectionTravel.West : DirectionTravel.East;
                else DirectionTravel_enemy = WayToTarget[Index].y > gameObject.transform.position.y ? DirectionTravel.North : DirectionTravel.South;
                InMove = true;
            }
            else EnemyAttack();
        }
        if (InMove) EnemyMove();
    }

    private void SearchPath()
    {
        WayToTarget = pathFinder.GetPath(CurrentTarget.transform);
    }

    private void EnemyMove()
    {
        if (DirectionTravel_enemy == DirectionTravel.North) bodyMovementWalking.StepNorth();
        if (DirectionTravel_enemy == DirectionTravel.South) bodyMovementWalking.StepSouth();
        if (DirectionTravel_enemy == DirectionTravel.East) bodyMovementWalking.StepEast();
        if (DirectionTravel_enemy == DirectionTravel.West) bodyMovementWalking.StepWest();
        InMove = false;
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

    public void HasDied()
    {
        Debug.Log("enemy_Die_d");
    }
}