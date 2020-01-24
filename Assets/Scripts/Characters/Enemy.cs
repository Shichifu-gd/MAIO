using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;

    public int Health { get; set; }
    public int Attack { get; set; }

    public bool InAggression { get; set; }

    public Transform CurrentTarget;
    [SerializeField]
    private CircleCollider2D RangZoneAggression;

    private List<Transform> WayToGoal;

    private void Awake()
    {
        bodyMovementWalking = GetComponent<BodyMovementWalking>();
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
            WorkingOutWay();
        }
    }

    private void WorkingOutWay() //fixme
    {
        Vector2 enemyVector = gameObject.transform.position;
        Vector2 heroVector = CurrentTarget.position;
        float x = heroVector.x - enemyVector.x;
        float y = heroVector.y - enemyVector.y;
        Debug.Log($"x| {x} , y| {y}");
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