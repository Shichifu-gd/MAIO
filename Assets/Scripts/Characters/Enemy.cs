using UnityEngine;

public class Enemy : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;

    public int Health { get; set; }
    public int Attack { get; set; }

    private void Awake()
    {
        bodyMovementWalking = GetComponent<BodyMovementWalking>();
    }

    public void DetermineDirectionMovement()
    {
        int index = Random.Range(0, 4);
        if (index == 0)
        {
            bodyMovementWalking.StepNorth();
        }
        if (index == 1)
        {
            bodyMovementWalking.StepSouth();
        }
        if (index == 2)
        {
            bodyMovementWalking.StepEast();
        }
        if (index == 3)
        {
            bodyMovementWalking.StepWest();
        }
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