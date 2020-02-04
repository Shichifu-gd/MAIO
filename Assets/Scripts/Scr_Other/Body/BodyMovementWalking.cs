using System.Collections;
using UnityEngine;

public enum DirectionTravel
{
    North,
    South,
    East,
    West,
}

public enum Person
{
    Player,
    Opponent,
    Companion,
    None,
}

public class BodyMovementWalking : MonoBehaviour, IMove
{
    private CheckPosition currentDirection;
    private Cell CurrentCell;

    public Person ThisPerson;

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

    public bool StepNorth()
    {
        InMove = true;
        currentDirection = MotionCheck(0);
        if (InMove)
        {
            MathAction = true;
            MovementOnY = true;
            CheckOpportunityTakeStep();
        }
        return InMove;
    }

    public bool StepSouth()
    {
        InMove = true;
        currentDirection = MotionCheck(1);
        if (InMove)
        {
            MathAction = false;
            MovementOnY = true;
            CheckOpportunityTakeStep();
        }
        return InMove;
    }

    public bool StepEast()
    {
        InMove = true;
        currentDirection = MotionCheck(2);
        if (InMove)
        {
            MathAction = true;
            MovementOnX = true;
            CheckOpportunityTakeStep();
        }
        return InMove;
    }

    public bool StepWest()
    {
        InMove = true;
        currentDirection = MotionCheck(3);
        if (InMove)
        {
            MathAction = false;
            MovementOnX = true;
            CheckOpportunityTakeStep();
        }
        return InMove;
    }

    private CheckPosition MotionCheck(int index)
    {
        if (Direction[index].CanGo == true)
        {
            if (CurrentCell != null)
            {
                CurrentCell.SetDefaultValueForWalking();
                CurrentCell.SetWhoIsThere(Person.None);
            }
            return Direction[index];
        }
        else
        {
            var newCell = Direction[index].GetCell();
            if (ThisPerson == Person.Player && newCell.WhoIsThere == Person.Opponent) InMove = false;
            if (ThisPerson == Person.Opponent && newCell.WhoIsThere == Person.Player) InMove = false;
            return currentDirection;
        }
    }

    public IEnumerator UpdateDirections()
    {
        Directions.SetActive(false);
        OnResetCanGo();
        yield return new WaitForSeconds(.1f);
    }

    private void CheckOpportunityTakeStep()
    {
        Directions.SetActive(false);
        if (InMove && currentDirection != null)
        {
            CurrentCell = currentDirection.GetCell();
            DirectionCoordinate = currentDirection.transform.position;
            DeterminesWhereGo();
            CurrentCell.OccupyCage();
            CurrentCell.SetWhoIsThere(ThisPerson);
        }
        else ResetElements();
    }

    public void DeterminesWhereGo()
    {
        if (currentDirection != null)
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
        OnResetCanGo();
        currentDirection = null;
        MovementOnX = false;
        MovementOnY = false;
    }

    private void OnResetCanGo()
    {
        foreach (var item in Direction)
        {
            item.ResetCanGo();
        }
        Directions.SetActive(true);
    }
}