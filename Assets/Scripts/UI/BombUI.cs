using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombUI : MonoBehaviour
{
    [SerializeField] private BombPlacement _bombPlacementComp;
    [SerializeField] private GameObject _btnPlaceBomb;
    [SerializeField] private Image _imgTimer;
    [SerializeField] private TMP_Text _textTimer;

    
    void Update()
    {
        if (_bombPlacementComp.Ready)
        {
            _btnPlaceBomb.SetActive(true);
            _imgTimer.gameObject.SetActive(false);
        }
        else {
            _btnPlaceBomb.SetActive(false);
            _imgTimer.gameObject.SetActive(true);
            _textTimer.text = _bombPlacementComp.LastTime.ToString("0.0");
            _imgTimer.fillAmount = 1 - (_bombPlacementComp.LastTime / _bombPlacementComp.FullTime);
        }
    }
}
