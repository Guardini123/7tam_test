using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGridObj : MonoBehaviour
{
	[HideInInspector] public int CurX;
	[HideInInspector] public int CurY;

	public void DeleteObj()
	{
		var targetPoint = GridField.Instant.GetPointByXY(CurX, CurY);
		var targetObj = targetPoint.ObjAtPoint.gameObject;
		targetPoint.ObjAtPoint = null;
		GameObject.Destroy(targetObj);
	}
}
