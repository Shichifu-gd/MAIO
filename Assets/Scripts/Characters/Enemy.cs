using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;
    private PathFinder pathFinder;

    public int Health { get; set; }
    public int Attack { get; set; }

    public bool InAggression { get; set; }

    public Transform CurrentTarget;
    [SerializeField]
    private CircleCollider2D RangZoneAggression;

    private List<Transform> WayToGoal = new List<Transform>();
    private List<Vector2> WayToTarget = new List<Vector2>();

    private void Awake()
    {
        bodyMovementWalking = GetComponent<BodyMovementWalking>();
        pathFinder = GetComponent<PathFinder>();
    }

    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }

    public void DetermineDirectionMovement()
    {
        if (CurrentTarget == null)
        {
            int index = Random.Range(0, 4);
            if (index == 0) bodyMovementWalking.StepNorth();
            if (index == 1) bodyMovementWalking.StepSouth();
            if (index == 2) bodyMovementWalking.StepEast();
            if (index == 3) bodyMovementWalking.StepWest();
        }
        else
        {
            CalculateWay();
        }
    }

    private void CalculateWay()
    {
        WayToTarget = pathFinder.GetPath(CurrentTarget);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) HasDied();
    }

    public void HasDied()
    {
        Debug.Log("enemy_Die_d");
    }
}