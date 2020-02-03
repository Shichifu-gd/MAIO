using System.Collections;
using UnityEngine;

public enum DirectionTravel
{
    North,
    South,
    East,
    West,
}

public class BodyMovementWalking : MonoBehaviour, IMove
{
    private CheckPosition currentDirection;
    private Cell CurrentCell;

    private float Pace = 0.5f;

    public bool InMove;
    public bool Step;
    public bool MovementOnX;
    public bool MovementOnY;
    public bool MathAction;

    private Vector2 DirectionCoordinate;
    private Vector2 CurrentPosition;

    public GameObject Directions;
    [SerializeField] private CheckPosition[] Direction = new CheckPosition[4];

    public void StepNorth()
    {
        InMove = true;
        if (CurrentCell) CurrentCell.SetDefaultCageForWalking();
        currentDirection = MotionCheck(0);
        MathAction = true;
        MovementOnY = true;
        CheckOpportunityTakeStep();
    }

    public void StepSouth()
    {
        InMove = true;
        if (CurrentCell) CurrentCell.SetDefaultCageForWalking();
        currentDirection = MotionCheck(1);
        MathAction = false;
        MovementOnY = true;
        CheckOpportunityTakeStep();
    }

    public void StepEast()
    {
        InMove = true;
        if (CurrentCell) CurrentCell.SetDefaultCageForWalking();
        currentDirection = MotionCheck(2);
        MathAction = true;
        MovementOnX = true;
        CheckOpportunityTakeStep();
    }

    public void StepWest()
    {
        InMove = true;
        if (CurrentCell) CurrentCell.SetDefaultCageForWalking();
        currentDirection = MotionCheck(3);
        MathAction = false;
        MovementOnX = true;
        CheckOpportunityTakeStep();
    }

    private CheckPosition MotionCheck(int index)
    {
        if (Direction[index].CanGo == true)
        {
            return Direction[index];
        }
        else return null;
    }

    private void CheckOpportunityTakeStep()
    {
        Directions.SetActive(false);
        if (InMove && currentDirection)
        {
            CurrentCell = currentDirection.GetCell();
            DirectionCoordinate = currentDirection.transform.position;
            DeterminesWhereGo();
            CurrentCell.SetCageForWalking();
        }
        else ResetElements();
    }

    public void DeterminesWhereGo()
    {
        if (currentDirection)
        {
            if (currentDirection.CanGo == true) StartCoroutine(StartMove());
            else ResetElements();
        }
        else ResetElements();
    }

    public IEnumerator StartMove()
    {
        CurrentPosition = gameObject.transform.position;
        Step = false;
        while (!Step)
        {
            if (MovementOnX) MoveOnX();
            else if (MovementOnY) MoveOnY();
            else Step = true;
            Move(CurrentPosition);
            yield return new WaitForSeconds(.05f);
        }
        ResetElements();
    }

    private void MoveOnX()
    {
        if (MathAction && CurrentPosition.x < DirectionCoordinate.x) CurrentPosition.x += Pace;
        else if (!MathAction && CurrentPosition.x > DirectionCoordinate.x) CurrentPosition.x -= Pace;
        else
        {
            Step = true;
            CurrentPosition = DirectionCoordinate;
        }
    }

    private void MoveOnY()
    {
        if (MathAction && CurrentPosition.y < DirectionCoordinate.y) CurrentPosition.y += Pace;
        else if (!MathAction && CurrentPosition.y > DirectionCoordinate.y) CurrentPosition.y -= Pace;
        else
        {
            Step = true;
            CurrentPosition = DirectionCoordinate;
        }
    }

    public void Move(Vector2 direction)
    {
        transform.position = direction;
    }

    public void ResetElements()
    {
        foreach (var item in Direction)
        {
            item.Discharge();
        }
        Directions.SetActive(true);
        currentDirection = null;
        MovementOnX = false;
        MovementOnY = false;
        InMove = false;
    }
}