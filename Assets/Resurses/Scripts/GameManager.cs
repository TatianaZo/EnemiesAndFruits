using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject restartUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    [SerializeField] private Text bestScore;
    [SerializeField] private Apple applePref;
    [SerializeField] private Enemy enemyPref;
    private GameObject apple;

    public int score { get; private set; }

    public int lives { get; private set; }


    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        // рестарт игры 
        if (lives <= 0)
        {
            restartUI.SetActive(true);
        }
    }

    public void NewGame()
    {
        // очещаем поле от врагов
        Enemy[] Enemy = FindObjectsOfType<Enemy>();
        for (int i = 0; i < Enemy.Length; i++) 
        {
            Destroy(Enemy[i].gameObject);
        }

        restartUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        Respawn();
        SpawnApple();
        SpawnEnemy(3);
    }

    private void SpawnApple()
    {
        float spawnX = Random.Range(-2.2f, 2.2f);
        float spawnY = Random.Range(-5f, 5f);
        apple = Instantiate(this.applePref.gameObject, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
    }

    public void SpawnEnemy(int count)
    {   
        for (int i=0; i < count; i++)
        {
            float spawnX = Random.Range(-2.2f, 2.2f);
            float spawnY = Random.Range(-5f, 5f);
            Instantiate(this.enemyPref.gameObject, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        }            
    }

    public void Respawn()
    {
        // перемещаем игрока после смерти в центр экрана
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    public void AppleConsume()
    {
        // начисление очков за яблоки
            SetScore(score + 100);
        apple.GetComponent<Apple>().Respawn();
    }
 
    public void PlayerDeath()
    {

        // -1  жизнь
        SetLives(lives - 1);

        // проверка на наличие жизней
        if (lives <= 0)
        {
            SaveBestScore(score);
            Destroy(apple);
            GameOver();
        }
        else 
        {
            // перерождение игрока
            Invoke("Respawn",3.0f);
        }
    }

    private void SaveBestScore(int score)
    {
        if (PlayerPrefs.GetInt("BestScore")< score) 
        PlayerPrefs.SetInt("BestScore", score);
    }

    private void GameOver()
    {
        // показ интерфейса проигрыша
        restartUI.SetActive(true);
        bestScore.text = "Ваш рекорд:\n" + PlayerPrefs.GetInt("BestScore");
    }

    private void SetScore(int score)
    {
        // показ кол-ва очков
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        // показ жизней
        this.lives = lives;
        livesText.text = lives.ToString();
    }

}
