using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseZone : MonoBehaviour
{
    private ListBaseZone listBaseZone;

    [SerializeField]
    private float Range_EBZ = 3.5f;

    [SerializeField]
    private bool ChangeCollider = false;

    private BoxCollider2D ColliderRange;

    public List<Vector2> ListAvailablePosition;

    private void Awake()
    {
        listBaseZone = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<ListBaseZone>();
        ColliderRange = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        listBaseZone.RegisterBaseZone(this);
        if (ChangeCollider) ColliderRange.size = new Vector2(Range_EBZ, Range_EBZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Cell>()) ListAvailablePosition.Add(collision.GetComponent<Cell>().transform.position);
    }

    public Vector2 GetPoint()
    {
        var index = Random.Range(0, ListAvailablePosition.Count);
        return ListAvailablePosition[index];
    }
}