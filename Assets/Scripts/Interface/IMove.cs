using System.Collections;
using UnityEngine;

public interface IMove
{
    void DeterminesWhereGo();

    IEnumerator StartMove();
    void Move(Vector2 direction);
}