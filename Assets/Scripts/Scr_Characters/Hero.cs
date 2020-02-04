using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Hero : MonoBehaviour, ICharacteristic, ITakeDamage
{
    private BodyMovementWalking bodyMovementWalking;
    public GameController gameController;

    private DirectionTravel DirectionTravel_player;

    public Image ImageCurrentHealth;

    private int MaxHealth;
    public int Health { get; set; }
    public int Attack { get; set; }

    private bool Immortality;
    private bool PlayerMove = true;
    private bool EndAction;

    private void Awake()
    {
        bodyMovementWalking = GetComponent<BodyMovementWalking>();
        PlayerController.SwipeEvent += OnAction;
    }

    public void Start()
    {
        Health = 100;
        MaxHealth = Health;
        Attack = 15;
    }

    private void OnAction(DirectionTravel type)
    {
        DirectionTravel_player = type;
        StartCoroutine(DetermineAction());
    }

    private IEnumerator DetermineAction()
    {
        bool move = true;
        OnUpdateDirections();
        yield return new WaitForSeconds(0.2f);
        PlayerMove = gameController.SetPlayerMove();
        if (PlayerMove)
        {
            if (DirectionTravel_player == DirectionTravel.North) move = bodyMovementWalking.StepNorth();
            if (DirectionTravel_player == DirectionTravel.South) move = bodyMovementWalking.StepSouth();
            if (DirectionTravel_player == DirectionTravel.East) move = bodyMovementWalking.StepEast();
            if (DirectionTravel_player == DirectionTravel.West) move = bodyMovementWalking.StepWest();
            if (!move) AttackPlayer();
            StartCoroutine(WaitingForEndOfAction());
        }
    }

    private IEnumerator WaitingForEndOfAction()
    {
        gameController.EndAction();
        yield return new WaitForSeconds(1f);
        PlayerMove = false;
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

    public void OnUpdateDirections()
    {
        StartCoroutine(bodyMovementWalking.UpdateDirections());
    }

    public void AttackPlayer()
    {
        // Debug.Log("atack");
    }

    public void HasDied()
    {
        Debug.Log("player_Die_d");
    }
}