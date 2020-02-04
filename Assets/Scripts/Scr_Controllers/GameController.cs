using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public EnemyController enemyController;

    public Image ImageHourglass;

    public float ActionTime { get; set; }
    public float TimeEnd { get; set; }

    public bool UpdateTime;

    private void Start()
    {
        TimeEnd = 2f;
    }

    private void Update()
    {
        UpdateTime = enemyController.SetUpdateTime();
        if (UpdateTime)
        {
            if (ActionTime < TimeEnd)
            {
                ActionTime += Time.deltaTime;
                Hourglass();
            }
            else
            {
                ActionTime = 0;
                Hourglass();
                ActionOpponents();
            }
        }
    }

    private void ActionOpponents()
    {
        enemyController.EnemyAction();
    }

    public void ResetVolue()
    {

    }

    public bool SetPlayerMove()
    {
        return enemyController.EnemyActionCompleted;
    }

    public void EndAction()
    {
        ActionTime = TimeEnd;
    }

    private void Hourglass()
    {
        float num = ActionTime;
        ImageHourglass.fillAmount = 1 - (num / TimeEnd);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}