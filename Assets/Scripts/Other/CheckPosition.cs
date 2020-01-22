using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    public bool CanGo { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Cell>()) CanGo = true;
        else CanGo = false;
    }

    public void Discharge()
    {
        CanGo = false;
    }
}