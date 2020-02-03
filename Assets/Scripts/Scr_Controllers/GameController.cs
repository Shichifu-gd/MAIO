using UnityEngine;

public class GameController : MonoBehaviour
{
    public EnemyController enemyController;

    public float ActionTime { get; set; }
    public float TimeEnd { get; set; }

    private void Start()
    {
        TimeEnd = 2f;
    }

    private void Update()
    {
        ActionTime += Time.deltaTime;
        if (ActionTime >= TimeEnd)
        {
            enemyController.EnemyAction();
            ActionTime = 0;
        }
    }

    public void ResetVolue()
    {

    }

    public void EndAction()
    {
        ActionTime = TimeEnd;
    }
}