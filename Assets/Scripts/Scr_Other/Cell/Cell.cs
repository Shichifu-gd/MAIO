using UnityEngine;

public class Cell : MonoBehaviour
{
    public Person WhoIsThere { get; set; } = Person.None;

    public string OptionCell { get; set; } = "standart";

    public bool CageForWalking { get; set; } = true;

    public void OccupyCage()
    {
        CageForWalking = false;
    }

    public void SetDefaultValueForWalking()
    {
        CageForWalking = true;
    }

    public void SetWhoIsThere(Person type)
    {
        WhoIsThere = type;
    }
}