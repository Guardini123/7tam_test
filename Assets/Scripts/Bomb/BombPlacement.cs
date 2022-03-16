using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlacement : MonoBehaviour
{
	GridField _field;
	[SerializeField] private GameObject _bombPrefab;
	[SerializeField] private Movement _movementComp;

	[SerializeField] private float _placeDelay;
	private float _lastDelay;
	public float FullTime => _placeDelay;
	public float LastTime => _lastDelay;
	public bool Ready => _lastDelay > 0 ? false : true;


	void Start()
	{
		_field = GridField.Instant;

		_lastDelay = _placeDelay;
	}


	private void Update()
	{
		if (_lastDelay > 0)
		{
			_lastDelay -= Time.deltaTime;
		}
	}	


	public void PlaceBomb()
	{
		if (_lastDelay > 0) return;

		var currentDirection = _movementComp.CurrentDirection;
		var currentX = _movementComp.CurrentX;
		var currentY = _movementComp.CurrentY;

		int bombX = currentX;
		int bombY = currentY;

		GridPoint previousPoint = null;

		switch (currentDirection)
		{
			case Movement.Direction.up:
				if ((currentY - 1) < 0) return;
				previousPoint = _field.GetPointByXY(currentX, currentY - 1);
				if (previousPoint.ObjAtPoint != null) return;
				bombY -= 1;
				break;
			case Movement.Direction.down:
				if (_field.GetBordersIndexes().y == (currentY + 1)) return;
				previousPoint = _field.GetPointByXY(currentX, currentY + 1);
				if (previousPoint.ObjAtPoint != null) return;
				bombY += 1;
				break;
			case Movement.Direction.left:
				if (_field.GetBordersIndexes().x == (currentX + 1)) return;
				previousPoint = _field.GetPointByXY(currentX + 1, currentY);
				if (previousPoint.ObjAtPoint != null) return;
				bombX += 1;
				break;
			case Movement.Direction.right:
				if ((currentX - 1) < 0) return;
				previousPoint = _field.GetPointByXY(currentX - 1, currentY);
				if (previousPoint.ObjAtPoint != null) return;
				bombX -= 1;
				break;
			default:
				break;
		}

		var bombGO = Instantiate(_bombPrefab, previousPoint.transform.position, Quaternion.identity, previousPoint.transform);
		previousPoint.ObjAtPoint = bombGO.transform;
		bombGO.GetComponent<Bomb>().SetCoordinates(bombX, bombY);
		_lastDelay = _placeDelay;
	}
}
