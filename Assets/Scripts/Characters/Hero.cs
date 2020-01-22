using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, ICharacteristic, IMove, ITakeDamage
{
    private CheckPosition CurrentCell;

    public int Health { get; set; }
    public int Attack { get; set; }

    private float Pace = 0.1f;

    public bool InMove;
    public bool MovementOnX;
    public bool MovementOnY;
    public bool MathAction;

    [SerializeField] private GameObject Directions;
    [SerializeField] private CheckPosition[] Direction = new CheckPosition[4];

    private void Update()
    {
        if (!InMove) IdentifySide();
    }

    private void IdentifySide()
    {
        if (!InMove && Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentCell = MotionCheck(0);
            MathAction = true;
            MovementOnY = true;
            InMove = true;
        }
        if (!InMove && Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentCell = MotionCheck(1);

            MathAction = false;
            MovementOnY = true;

            InMove = true;
        }
        if (!InMove && Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurrentCell = MotionCheck(2);
            MathAction = true;
            MovementOnX = true;
            InMove = true;
        }
        if (!InMove && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CurrentCell = MotionCheck(3);
            MathAction = false;
            MovementOnX = true;
            InMove = true;
        }
        if (InMove)
        {
            Directions.SetActive(false);
            DeterminesWhereGo();
        }
    }

    private CheckPosition MotionCheck(int index)
    {
        if (Direction[index].CanGo == true)
        {
            return Direction[index];
        }
        return null;
    }

    public void DeterminesWhereGo()
    {
        if (CurrentCell)
        {
            if (CurrentCell.CanGo == true) StartCoroutine(StartMove());
            else GiveBack();
        }
        else GiveBack();
    }

    public IEnumerator StartMove()
    {
        Vector2 currentPosition = gameObject.transform.position;
        for (int i = 0; i < 10; i++)
        {
            if (MovementOnX && MathAction) currentPosition.x += Pace;
            else if (MovementOnX && !MathAction) currentPosition.x -= Pace;
            if (MovementOnY && MathAction) currentPosition.y += Pace;
            else if (MovementOnY && !MathAction) currentPosition.y -= Pace;
            Move(currentPosition);
            yield return new WaitForSeconds(.05f);
        }
        GiveBack();
    }

    public void Move(Vector2 direction)
    {
        transform.position = direction;
    }

    private void GiveBack()
    {
        foreach (var item in Direction)
        {
            item.Discharge();
        }
        Directions.SetActive(true);
        CurrentCell = null;
        MovementOnX = false;
        MovementOnY = false;
        InMove = false;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) HasDied();
    }

    public void HasDied()
    {
        Debug.Log("Die_d");
    }
}