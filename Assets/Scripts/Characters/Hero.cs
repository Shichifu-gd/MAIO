using UnityEngine;

public class Hero : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;
    public GameController gameController;

    public int Health { get; set; }
    public int Attack { get; set; }

    private void Awake()
    {
        bodyMovementWalking = GetComponent<BodyMovementWalking>();
    }

    private void Update()
    {
        if (!bodyMovementWalking.InMove) DetermineDirectionWalking();
    }

    private void DetermineDirectionWalking()
    {
        if (!bodyMovementWalking.InMove && Input.GetKeyDown(KeyCode.UpArrow))
        {
            bodyMovementWalking.StepNorth();
            gameController.EndAction();
        }
        if (!bodyMovementWalking.InMove && Input.GetKeyDown(KeyCode.DownArrow))
        {
            bodyMovementWalking.StepSouth();
            gameController.EndAction();
        }
        if (!bodyMovementWalking.InMove && Input.GetKeyDown(KeyCode.RightArrow))
        {
            bodyMovementWalking.StepEast();
            gameController.EndAction();
        }
        if (!bodyMovementWalking.InMove && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bodyMovementWalking.StepWest();
            gameController.EndAction();
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) HasDied();
    }

    public void HasDied()
    {
        Debug.Log("player_Die_d");
    }
}