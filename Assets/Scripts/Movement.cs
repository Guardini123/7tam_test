using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DeleteGridObj))]
public class Movement : MonoBehaviour
{
	GridField _field;
	[Header("Movement settings")]
	[SerializeField] private float _speed = 0.0f;

	private Vector2 _targetPos = new Vector2();
	
	private int _curX;
	private int _curY;
	public int CurrentX => _curX;
	public int CurrentY => _curY;

	[SerializeField] private Direction _startDirection;
	private Direction _curDir;
	public Direction CurrentDirection => _curDir;

	private bool _moving = false;

	[Header("View settings")]
	[SerializeField] private Sprite _spriteUp;
	[SerializeField] private Sprite _spriteDown;
	[SerializeField] private Sprite _spriteLeft;
	[SerializeField] private Sprite _spriteRight;
	[SerializeField] private SpriteRenderer _spriteRendererComp;

	private DeleteGridObj _deleteGridObjComp;


	public enum Direction
	{
		up,
		down,
		left,
		right
	}


	void Start()
	{
		_field = GridField.Instant;

		_targetPos = current2DPos;

		_curDir = _startDirection;

		_deleteGridObjComp = this.gameObject.GetComponent<DeleteGridObj>();
	}


	void FixedUpdate()
	{
		if ((_targetPos - current2DPos).magnitude > 0.1f)
		{
			_moving = true;
			MoveToTargetPos(_targetPos);
		}
		else
		{
			_moving = false;
		}
	}


	public void SetCurrentXY(int x, int y)
	{
		_curX = x;
		_curY = y;
	}


	public void Move(Direction direction)
	{
		if (_moving) return;

		GridPoint nextPoint = null;
		switch (direction)
		{
			case Direction.up:
				if (_field.GetBordersIndexes().y == (_curY + 1)) return;
				nextPoint = _field.GetPointByXY(_curX, _curY + 1);
				if (!MoveOnField(nextPoint)) return;

				_curY += 1;

				break;
			case Direction.down:
				if ((_curY - 1) < 0) return;
				nextPoint = _field.GetPointByXY(_curX, _curY - 1);
				if (!MoveOnField(nextPoint)) return;

				_curY -= 1;

				break;
			case Direction.left:
				if ((_curX - 1) < 0) return;
				nextPoint = _field.GetPointByXY(_curX - 1, _curY);
				if (!MoveOnField(nextPoint)) return;

				_curX -= 1;

				break;
			case Direction.right:
				if (_field.GetBordersIndexes().x == (_curX + 1)) return;
				nextPoint = _field.GetPointByXY(_curX + 1, _curY);
				if (!MoveOnField(nextPoint)) return;

				_curX += 1;

				break;
			default:
				break;
		}

		_curDir = direction;

		ChangeView();

		_deleteGridObjComp.CurX = _curX;
		_deleteGridObjComp.CurY = _curY;
	}


	private bool MoveOnField(GridPoint nextPoint)
	{
		if (nextPoint.ObjAtPoint != null) return false;

		GridPoint currentPoint = _field.GetPointByXY(_curX, _curY);

		currentPoint.ObjAtPoint = null;
		nextPoint.ObjAtPoint = this.gameObject.transform;
		_targetPos = new Vector2(
			nextPoint.transform.position.x,
			nextPoint.transform.position.y
			);
		this.transform.parent = nextPoint.transform;
		this.transform.position = new Vector3(
			this.transform.position.x,
			this.transform.position.y,
			nextPoint.transform.localPosition.z
			);

		_moving = true;

		return true;
	}


	private Vector2 current2DPos => new Vector2(this.transform.position.x, this.transform.position.y);


	private void MoveToTargetPos(Vector2 targetPos)
	{
		var targetPos3D = new Vector3(targetPos.x, targetPos.y, this.transform.position.z);
		var dir = (targetPos3D - this.transform.position).normalized;
		this.transform.Translate(dir * _speed * Time.fixedDeltaTime);
	}


	private void ChangeView()
	{
		switch (_curDir)
		{
			case Direction.up:
				_spriteRendererComp.sprite = _spriteUp;
				break;
			case Direction.down:
				_spriteRendererComp.sprite = _spriteDown;
				break;
			case Direction.left:
				_spriteRendererComp.sprite = _spriteLeft;
				break;
			case Direction.right:
				_spriteRendererComp.sprite = _spriteRight;
				break;
			default:
				break;
		}
	}


	//--------------------------------UI funcs---------------------------------------------------

	public void MoveUp()
	{
		Move(Direction.up);
	}

	public void MoveDown()
	{
		Move(Direction.down);
	}

	public void MoveLeft()
	{
		Move(Direction.left);
	}

	public void MoveRight()
	{
		Move(Direction.right);
	}
}