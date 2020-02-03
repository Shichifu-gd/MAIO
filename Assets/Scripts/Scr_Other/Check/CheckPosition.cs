using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    private Cell cell;

    public bool CanGo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Cell>())
        {
            cell = collision.GetComponent<Cell>();
            CanGo = true;
        }
    }

    public void Discharge()
    {
        CanGo = false;
    }

    public Cell GetCell()
    {
        return cell;
    }
}