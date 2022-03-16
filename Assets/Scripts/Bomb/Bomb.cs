using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(DeleteGridObj))]
public class Bomb : MonoBehaviour
{
	[SerializeField] private int _damage;
	[SerializeField] private float _explozionDelay;
	private float _lastTime;

	[SerializeField] private TMP_Text _timer;

	[SerializeField] private DeleteGridObj _deleteGridObj;
	private GridField _field;

	private int _curX;
	private int _curY;


	private void Start()
	{
		_lastTime = _explozionDelay;
		_field = GridField.Instant;
	}


	private void Update()
	{
		if(_lastTime > 0)
		{
			_lastTime -= Time.deltaTime;
			_timer.text = _lastTime.ToString("0.0");
		}
		else
		{
			Explosion();
		}
	}


	public void SetCoordinates(int x, int y)
	{
		_curX = x;
		_curY = y;

		_deleteGridObj.CurX = x;
		_deleteGridObj.CurY = y;
	}


	private void Explosion()
	{
		var pointsAround = _field.GetPointsAround(_curX, _curY);
		List<GridPoint> pointsAround2 = new List<GridPoint>();

		foreach (var point in pointsAround)
		{
			pointsAround2.Add(point);
			var pointsAroundPoint = _field.GetPointsAround(point.CurX, point.CurY);
			foreach (var pt in pointsAroundPoint)
			{
				if (!pointsAround2.Contains(pt))
				{
					pointsAround2.Add(pt);
				}
			}
		}

		foreach (var point in pointsAround2)
		{
			if (point.ObjAtPoint != null)
			{
				Health objHealth = null;
				if (point.ObjAtPoint.gameObject.TryGetComponent<Health>(out objHealth))
				{
					objHealth.SetDamage(_damage);
				}
			}
		}

		_deleteGridObj.DeleteObj();
	}
}