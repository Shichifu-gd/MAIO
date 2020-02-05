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
            if (cell.CageForWalking) CanGo = true;
            // else Debug.Log("The cell is busy");
        }
    }

    public void ResetCanGo()
    {
        CanGo = false;
        cell = null;
    }

    public Cell GetCell()
    {
        return cell;
    }
}