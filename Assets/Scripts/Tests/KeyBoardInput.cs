using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : MonoBehaviour
{
    [SerializeField] private Movement _moveAbleObj;
    [SerializeField] private BombPlacement _bombPlaceAbleOnj;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.W))
		{
            _moveAbleObj.Move(Movement.Direction.up);
		}
        if (Input.GetKeyDown(KeyCode.S))
        {
            _moveAbleObj.Move(Movement.Direction.down);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveAbleObj.Move(Movement.Direction.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveAbleObj.Move(Movement.Direction.right);
        }
        if (Input.GetKeyDown(KeyCode.Space))
		{
            _bombPlaceAbleOnj.PlaceBomb();
		}
    }
}
