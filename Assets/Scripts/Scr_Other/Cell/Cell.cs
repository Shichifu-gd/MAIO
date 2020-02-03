using UnityEngine;

public class Cell : MonoBehaviour
{
    public string OptionCell { get; set; } = "standart";
    public bool CageForWalking { get; set; } = true;

    public void SetCageForWalking()
    {
        CageForWalking = false;
    }

    public void SetDefaultCageForWalking()
    {
        CageForWalking = true;
    }
}