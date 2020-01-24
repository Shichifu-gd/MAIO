using UnityEngine;

public class ZoneAggression : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>()) NewTarget(collision.transform);
    }

    private void NewTarget(Transform newTarget)
    {
        enemy.SetTarget(newTarget);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>()) RemovesTarget();
    }

    private void RemovesTarget()
    {
        enemy.SetTarget(null);
    }
}