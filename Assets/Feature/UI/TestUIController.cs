using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestUIController : MonoBehaviour
{
    [SerializeField] private RectTransform _gameOverPanel;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private AbstractHealth _playerHealth;

    private float _time;

    private void Awake() => Subscribe();

    private void Update() => ChangeTime();

    private void Start() => OnHealthChanged(_playerHealth.CurrentHealth);

    private void OnDestroy() => Unsubscribe();

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDeath()
    {
        Time.timeScale = 0.1f;
        _gameOverPanel.gameObject.SetActive(true);
    }

    private void ChangeTime()
    {
        _time += Time.deltaTime;
        _timeText.text = _time.ToString("0.00");
    }

    private void OnHealthChanged(int value) => _healthText.text = value.ToString();

    private void Subscribe()
    {
        _playerHealth.onHealthChanged += OnHealthChanged;
        _playerHealth.onDeath += OnDeath; ;
    }

    private void Unsubscribe()
    {
        _playerHealth.onHealthChanged -= OnHealthChanged;
        _playerHealth.onDeath -= OnDeath; ;
    }
}