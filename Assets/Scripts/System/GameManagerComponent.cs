using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManagerComponent : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private GameObject _statusUI = null;
    [SerializeField] private TextMeshProUGUI _timeText = null;
    [SerializeField] private TextMeshProUGUI _killCountText = null;

    [Header("Disables")]
    [SerializeField] private GameObject[] _toDisables = null;

    private int _targetsCount;
    private int _targetsDeathCount;

    private float _timeElapsed;

    private int _currentLevel;

    private void Awake()
    {
        _targetsCount = FindObjectsOfType<TargetComponent>().Length;
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        _targetsDeathCount = 0;
        _timeElapsed = 0;

        _statusUI.SetActive(false);
        enabled = true;

        Time.timeScale = 1.0f;
    }
    private void Update() => _timeElapsed += Time.deltaTime;
        
    public void OnTargetDie()
    {
        _targetsDeathCount++;        

        if(_targetsDeathCount >= _targetsCount)
        {
            Time.timeScale = 0.0f;
            enabled = false;
            ShowStatus();
        }
    }
    public void ResetScene() => SceneManager.LoadScene(_currentLevel);

    private void ShowStatus()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;        

        _timeText.text = _timeElapsed.ToString("F3");
        _killCountText.text = _targetsDeathCount.ToString();

        _statusUI.SetActive(true);

        foreach (GameObject go in _toDisables)
            go.SetActive(false);
    }
}
