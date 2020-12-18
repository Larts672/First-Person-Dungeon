using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishData : MonoBehaviour
{
    public int finishType; // Как завершилась игра: 0 - игрок умер, 1 - игрок прошёл игру
    private void Start()
    {
        finishType = -1;
    }
}
