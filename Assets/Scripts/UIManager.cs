using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _spText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private GameObject[] _playerHealths;

    private int _healthIndex;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _gameManager.playerHealth; i++)
        {
            _playerHealths[i].SetActive(true);
        }

        _healthIndex = 2;
        _spText.text = _gameManager.sp.ToString();
        _costText.text = _gameManager.towerCost.ToString();
    }

    public void SetSpText(string str)
    {
        _spText.text = str;
    }

    public void SetCostText(string str)
    {
        _costText.text = str;
    }

    public void PlayerOnDamage(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            if (_healthIndex < 0)
                break;
            _playerHealths[_healthIndex--].SetActive(false);
        }
    }
}
