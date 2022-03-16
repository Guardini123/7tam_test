using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridField : MonoBehaviour
{
    [SerializeField] private List<GridLine> _lines;

    private GridPoint[,] _points;

    public static GridField Instant { get; private set; }


	private void Awake()
	{
        Instant = this;
	}


	void Start()
    {
        _points = initGrid();
    }


    /// <summary>
    /// Returns max index by X axce and by Y axce
    /// </summary>
    /// <returns> max X and max Y indexes in Vector2 </returns>
    public Vector2 GetBordersIndexes()
	{
        return new Vector2(_points.GetLength(0), _points.GetLength(1));
	}


    /// <summary>
    /// Returns point of field by x and y coordinates on field, not by position!
    /// </summary>
    /// <param name="x"> x coordinate </param>
    /// <param name="y"> y coordinate </param>
    /// <returns> Drid point </returns>
    public GridPoint GetPointByXY(int x, int y)
	{
        return _points[x,y];
	}


    public List<GridPoint> GetPointsAround(int x, int y) 
    {
        var result = new List<GridPoint>();
        try
        {
            result.Add(_points[x, y + 1]);
        }
        catch (Exception e) { }
        try
        {
            result.Add(_points[x, y - 1]);
        }
        catch (Exception e) { }
        try
        {
            result.Add(_points[x - 1, y]);
        }
        catch (Exception e) { }
        try
        {
            result.Add(_points[x + 1, y]);
        }
        catch (Exception e) { }
        return result;
    }


    private GridPoint[,] initGrid()
	{
        GridPoint[,] points = new GridPoint[_lines.Count, _lines[0].Points.Count];
        for (int i = 0; i < _lines.Count; i++)
        {
            for (int j = 0; j < _lines[i].Points.Count; j++)
            {
                points[i, j] = _lines[i].Points[j];
                points[i, j].CurX = i;
                points[i, j].CurY = j;

                if (points[i, j].ObjAtPoint != null)
				{
                    DeleteGridObj deleteAbleObj = null;
                    if (points[i, j].ObjAtPoint.TryGetComponent<DeleteGridObj>(out deleteAbleObj))
                    {
                        deleteAbleObj.CurX = i;
                        deleteAbleObj.CurY = j;
                    }

                    Movement moveAbleObj = null;
                    if(points[i, j].ObjAtPoint.TryGetComponent<Movement>(out moveAbleObj))
					{
                        moveAbleObj.SetCurrentXY(i, j);
					}
				}
            }
        }
        return points;
    }
}
