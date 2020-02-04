using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Enemy CurrentEnemy;

    private bool SkipMove;
    public bool EnemyActionCompleted { get; set; } = true;
    private bool Skip;
    private bool NextActionEnemies;

    public List<Enemy> ListEnemy;

    public void EnemyAction()
    {
        if (ListEnemy.Count > 0)
        {
            Skip = true;
            for (int index = 0; index < ListEnemy.Count;)
            {
                if (CurrentEnemy != null) NextActionEnemies = CurrentEnemy.SetNextActionEnemies();
                if (Skip || !NextActionEnemies)
                {
                    CurrentEnemy = ListEnemy[index];
                    Step();
                    index++;
                }
                else if (!Skip && NextActionEnemies)
                {
                    Debug.Log("Error");
                    break;
                }
            }
        }
    }

    private void Step()
    {
        SkipMove = GetWaitTime();
        if (!SkipMove)
        {
            StartCoroutine(CurrentEnemy.DetermineDirectionMovement());
            NextActionEnemies = false;
            Skip = false;
        }
        if (SkipMove) Skip = true;
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
        if (CurrentEnemy != null)
        {
            if (CurrentEnemy.CurrentTarget == null)
            {
                int random = Random.Range(0, 2);
                if (random == 0) return true;
                else return false;
            }
            else return false;
        }
        else return false;
    }

    public bool SetUpdateTime()
    {
        return EnemyActionCompleted;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        ListEnemy.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        ListEnemy.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}