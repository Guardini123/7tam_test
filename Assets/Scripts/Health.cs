using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Animation))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _startHP;
    private int _currentHP;
    private bool Dead => _currentHP <= 0 ? true : false;
	[SerializeField] private string _deadAnimName;
	private Animation _animComp;
	public UnityEvent OnDeath;
	public UnityEvent AfterDeath;


	private void Start()
	{
		_animComp = this.gameObject.GetComponent<Animation>();
		_currentHP = _startHP;
	}


	public void SetDamage(int damage)
	{
        _currentHP -= damage;

		if (Dead) { 
			OnDeath?.Invoke();
			_animComp.Play(_deadAnimName, PlayMode.StopAll);
		}
	}


	public void AfterAnim()
	{
		AfterDeath?.Invoke();
	}
    
}
