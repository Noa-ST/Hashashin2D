using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    STARTING,
    PLAYING,
    PAUSED,
    GAMEOVER
}

public class GameManager : Singleton<GameManager>
{
    public static GameState state;
    public List<Transform> startPoints;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;
    private List<EnemyWalk> enemiesInArea = new List<EnemyWalk>();
    private bool _bossDefeated = false;
    public PlayerController player;

    public PlayerController Player { get => player; set => player = value; }

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    private void Start()
    {
        state = GameState.STARTING;
        GUIManager.Ins.ShowGameGui(false);
    }

    public void StartGame(GameObject player)
    {
        // Đảm bảo level đầu tiên và trạng thái game được thiết lập
        currentLevelIndex = 0; // Bắt đầu từ level đầu tiên
        levels[currentLevelIndex].SetActive(true); // Bật level đầu tiên
        MovePlayerToStartPoint(player); // Đưa player đến vị trí bắt đầu của level đầu tiên
        ResetLevelState(); // Reset trạng thái cho level đầu tiên

        state = GameState.PLAYING; // Đặt trạng thái thành PLAYING

        UpdateGUI(); // Cập nhật GUI
    }

    public void TransitionToNextLevel(GameObject player)
    {
        if (currentLevelIndex < levels.Count - 1)
        {
            levels[currentLevelIndex].SetActive(false); // Tắt level hiện tại
            currentLevelIndex++;
            levels[currentLevelIndex].SetActive(true); // Bật level tiếp theo
            MovePlayerToStartPoint(player);
            ResetLevelState(); // Reset trạng thái cho level mới

            state = GameState.PLAYING; // Đặt trạng thái thành PLAYING
            UpdateGUI(); // Cập nhật GUI khi bắt đầu level mới
        }
        else
        {
            state = GameState.GAMEOVER;
        }
    }

    private void UpdateGUI()
    {
        if (GUIManager.Ins == null)
        {
            Debug.LogError("GUIManager chưa được khởi tạo.");
            return;
        }
        GUIManager.Ins.ShowGameGui(true);
        GUIManager.Ins.UpdateCoinsCounting(Pref.coins);

        if (player != null)
        {

            GUIManager.Ins.UpdateHpInfo(player.CurHp, player.PlayerStats.hp);
            GUIManager.Ins.UpdateLevelInfo(player.PlayerStats.level, player.PlayerStats.xp, player.PlayerStats.levelUpXpRequied);
        }
        else
        {
            Debug.LogError("PlayerStats không được tìm thấy trên Player.");
        }
    }


    public void MovePlayerToStartPoint(GameObject player)
    {
        if (startPoints != null && startPoints.Count > currentLevelIndex)
        {
            player.transform.position = startPoints[currentLevelIndex].position;
        }
    }

    public void PlayerDied()
    {
        state = GameState.GAMEOVER; // Đặt trạng thái thành GAMEOVER
        Restart(); // Khởi động lại level vừa chơi
    }

    public void Restart()
    {
        ResetLevelState(); // Reset trạng thái level
        levels[currentLevelIndex].SetActive(true); // Bật lại level hiện tại
        MovePlayerToStartPoint(GameObject.FindWithTag("Player")); // Đưa player trở về điểm xuất phát
    }

    private void ResetGame()
    {
        currentLevelIndex = 0;
        foreach (GameObject level in levels)
        {
            level.SetActive(false); // Tắt tất cả các level
        }
        levels[currentLevelIndex].SetActive(true); // Bật level đầu tiên
        ResetLevelState(); // Reset trạng thái cho level đầu tiên
        state = GameState.PLAYING; // Đặt lại trạng thái thành PLAYING
    }

    private void ResetLevelState()
    {
        enemiesInArea.Clear(); // Xóa danh sách kẻ thù trong khu vực
        _bossDefeated = false; // Reset trạng thái boss
    }

    // Phương thức để đăng ký kẻ thù
    public void RegisterEnemy(EnemyWalk enemy)
    {
        if (!enemiesInArea.Contains(enemy))
        {
            enemiesInArea.Add(enemy);
        }
    }

    // Phương thức để hủy đăng ký kẻ thù
    public void UnregisterEnemy(EnemyWalk enemy)
    {
        if (enemiesInArea.Contains(enemy))
        {
            enemiesInArea.Remove(enemy);
            CheckEnemies(); // Kiểm tra kẻ thù sau khi hủy đăng ký
        }
    }

    // Phương thức gọi khi boss bị tiêu diệt
    public void BossDefeated()
    {
        _bossDefeated = true; // Đánh dấu boss đã bị tiêu diệt
        CheckEnemies(); // Kiểm tra xem có cần chuyển điểm xuất phát không
    }

    // Phương thức kiểm tra xem tất cả kẻ thù và boss đã bị tiêu diệt chưa
    private void CheckEnemies()
    {
        if (enemiesInArea.Count == 0 && _bossDefeated)
        {
            MoveToNextStartPoint();
        }
    }

    // Phương thức để di chuyển đến điểm xuất phát mới
    private void MoveToNextStartPoint()
    {
        if (currentLevelIndex < startPoints.Count - 1)
        {
            MovePlayerToStartPoint(GameObject.FindWithTag("Player")); // Di chuyển player đến điểm xuất phát tiếp theo
            TransitionToNextLevel(GameObject.FindWithTag("Player")); // Chuyển sang level tiếp theo
        }
    }
}
