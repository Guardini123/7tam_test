using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
	private Transform _objAtPoint = null;
	public Transform ObjAtPoint
	{
		get
		{
			return _objAtPoint;
		}
		set
		{
			_objAtPoint = value;
		}
	}


	[HideInInspector] public int CurX;
	[HideInInspector] public int CurY;


	private void Awake()
	{
		SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			if (spriteRenderer.enabled == true)
			{
				spriteRenderer.enabled = false;
			}
		}

		if( this.transform.childCount == 1)
		{
			_objAtPoint = this.transform.GetChild(0);
		}
	}
}
