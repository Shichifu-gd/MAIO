using System.Collections;
using UnityEngine;

public class BodyMovementWalking : MonoBehaviour, IMove
{
    private CheckPosition currentDirection;
    private Cell CurrentCell;

    private float Pace = 0.1f;

    public bool InMove;
    public bool MovementOnX;
    public bool MovementOnY;
    public bool MathAction;

    [SerializeField] private GameObject Directions;
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
            DeterminesWhereGo();
            CurrentCell.SetCageForWalking();
        }
        else GiveBack();
    }

    public void DeterminesWhereGo()
    {
        if (currentDirection)
        {
            if (currentDirection.CanGo == true) StartCoroutine(StartMove());
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

    public void GiveBack()
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