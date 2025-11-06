using TMPro;
using UnityEngine;

public class LossCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private int _enemyLossCounter;
    private int _playerLossCounter;

    public void SetEnemyLoss(int value)
    {
        _enemyLossCounter = value;
        UpdateText();
    }
    public void SetPlayerLoss(int value)
    {
        _playerLossCounter = value;
        UpdateText();
    }


    private void UpdateText()
    {
        textMeshProUGUI.text = $"{_playerLossCounter}:{_enemyLossCounter}";
    }
}

