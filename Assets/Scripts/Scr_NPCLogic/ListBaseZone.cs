using System.Collections.Generic;
using UnityEngine;

public class ListBaseZone : MonoBehaviour
{
    public List<EnemyBaseZone> ListEnemyBaseZone;

    public void RegisterBaseZone(EnemyBaseZone newZone)
    {
        ListEnemyBaseZone.Add(newZone);
    }

    public void UnregisterBaseZone(EnemyBaseZone newZone)
    {

    }
}