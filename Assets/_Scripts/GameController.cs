using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text livesLabel;

    private float scoreLabelHalfH, scoreLabelHalfW;
    private float livesLabelHalfH, livesLabelHalfW;

    [SerializeField] private PlayerController player;

    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private float gameOverDelay = 1.5f;
    [SerializeField] private int startLives = 3;
    private int lives;
    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            livesLabel.text = $"LIVES: {lives}";
        }
    }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyNum = 3;
    private List<EnemyBehaviour> enemies = new();
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreLabel.text = $"SCORE: {score}";
        }
    }

    [SerializeField] private PickupBehaviour pickupPrefab;
    [SerializeField] private Boundary pickupIntervalRange;

    public static GameController Instance;
    private void Awake() { if (Instance == null) Instance = this; }
    private void OnDestroy() { if (Instance == this) Instance = null; }
    private void OnEnable() => player.OnDied += OnPlayerDied;
    private void OnDisable() => player.OnDied -= OnPlayerDied;

    private void Start()
    {
        scoreLabelHalfH = scoreLabel.rectTransform.rect.height * 0.5f;
        scoreLabelHalfW = scoreLabel.rectTransform.rect.width * 0.5f;
        livesLabelHalfH = livesLabel.rectTransform.rect.height * 0.5f;
        livesLabelHalfW = livesLabel.rectTransform.rect.width * 0.5f;
        Lives = startLives;
        Score = 0;
        for (int i = 0; i < enemyNum; i++)
        {
            var enemyGO = Instantiate(enemyPrefab);
            var enemy = enemyGO.GetComponent<EnemyBehaviour>();
            enemies.Add(enemy);
            enemy.OnDied += OnEnemyDied;
        }
        StartCoroutine(SpawnPickupRoutine());
    }
    private void Update()
    {
        scoreLabel.rectTransform.position = new Vector2
        (Screen.safeArea.xMax - scoreLabelHalfW, Screen.safeArea.yMax - scoreLabelHalfH);
        livesLabel.rectTransform.position = new Vector2
        (Screen.safeArea.xMin + livesLabelHalfW, Screen.safeArea.yMax - livesLabelHalfH);
    }

    private void OnPlayerDied()
    {
        Lives--;
        StartCoroutine(Lives > 0 ? RespawnRoutine() : GameOverRoutine());
    }
    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        player.Respawn();
    }
    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("End");
    }

    private IEnumerator SpawnPickupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(pickupIntervalRange.min, pickupIntervalRange.max));
            var pickup = Instantiate(pickupPrefab);
            pickup.OnPickup += OnPickup;
        }
    }

    private void OnEnemyDied(int _score) => Score += _score;
    private void OnPickup(int _score) => Score += _score;
}