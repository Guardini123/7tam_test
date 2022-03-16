using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;

    private Movement _moveComp;
    private Health _healthComp;
    private GridField _field;


    void Start()
    {
        _moveComp = this.gameObject.GetComponent<Movement>();
        _healthComp = this.gameObject.GetComponent<Health>();
        _field = GridField.Instant;
    }

    
    void Update()
    {
        CheckPlayerAround();

        RandomWalk();
    }


    private void CheckPlayerAround()
	{
        var pointsAround = _field.GetPointsAround(_moveComp.CurrentX, _moveComp.CurrentY);

        GridPoint playersPoint = null;
        
        foreach(var point in pointsAround)
		{
            if (point.ObjAtPoint != null)
            {
                if (point.ObjAtPoint.gameObject.tag == "Player")
                {
                    playersPoint = point;
                    break;
                }
            }
		}

		if (playersPoint != null)
		{
            playersPoint.ObjAtPoint.gameObject.GetComponent<Health>().SetDamage(_damage);
		}
	}


    private void RandomWalk()
	{
        int rnd = Random.Range(0, 4);
        _moveComp.Move((Movement.Direction)rnd);
	}
}
