using UnityEngine.UI;
using UnityEngine;

public class Hero : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;
    public GameController gameController;

    public Image ImageCurrentHealth;

    private int MaxHealth;
    public int Health { get; set; }
    public int Attack { get; set; }

    private bool Immortality;

    private void Awake()
    {
        bodyMovementWalking = GetComponent<BodyMovementWalking>();
        PlayerController.SwipeEvent += DetermineDirectionWalking;
    }

    public void Start()
    {
        Health = 100;
        MaxHealth = Health;
        Attack = 15;
    }

    private void DetermineDirectionWalking(DirectionTravel type)
    {
        if (type == DirectionTravel.North)
        {
            bodyMovementWalking.StepNorth();
            gameController.EndAction();
        }
        if (type == DirectionTravel.South)
        {
            bodyMovementWalking.StepSouth();
            gameController.EndAction();
        }
        if (type == DirectionTravel.East)
        {
            bodyMovementWalking.StepEast();
            gameController.EndAction();
        }
        if (type == DirectionTravel.West)
        {
            bodyMovementWalking.StepWest();
            gameController.EndAction();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!Immortality)
        {
            Health -= damage;
            if (Health <= 0) HasDied();
            float num = Health;
            ImageCurrentHealth.fillAmount = num / MaxHealth;
        }
        else Debug.Log("haha, i am invincible");
    }

    public void HasDied()
    {
        Debug.Log("player_Die_d");
    }
}