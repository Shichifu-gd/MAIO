using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy CurrentEnemy;

    private bool SkipMove;

    public void EnemyAction()
    {
        //  NexEnemy();
        SkipMove = GetWaitTime();
        Step();
    }

    private void Step()
    {
        if (!SkipMove)
        {
            CurrentEnemy.DetermineDirectionMovement();
        }
        if (SkipMove) SkipMove = false;
    }

    private void NexEnemy()
    {

    }

    public void SetWaitTime()
    {
        SkipMove = true;
    }

    private bool GetWaitTime()
    {
        int random = Random.Range(0, 2);
        if (random == 0) return false;
        else return true;
    }
}