using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLine : MonoBehaviour
{
	[SerializeField] private List<GridPoint> _points;
    public List<GridPoint> Points => _points;
}
