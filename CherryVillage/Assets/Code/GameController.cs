using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rowList;
    [SerializeField] private int _currentRow;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private float _zRowDistamce = 10f;
    public static GameController singleton;

    private void Start()
    {
        singleton = this;
    }

    public void TransferUp()
    {
        _rowList[_currentRow++].SetActive(false);
        if (_currentRow >= _rowList.Count)
            Debug.LogError("ERROR: 'GameController' -> 'TransferUp()': _currentRow >= _rowList.Count");
        _rowList[_currentRow].SetActive(true);

        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + _zRowDistamce);

        _player.DeapthMovingCompleted();
        _player.transform.localScale = Vector3.one;
    }

    public void TransferDown()
    {
        _rowList[_currentRow--].SetActive(false);
        if (_currentRow < 0)
            Debug.LogError("ERROR: 'GameController' -> 'TransferDown()': _currentRow < 0");
        _rowList[_currentRow].SetActive(true);

        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z - _zRowDistamce);

        _player.DeapthMovingCompleted();
        _player.transform.localScale = Vector3.one;
    }
}
