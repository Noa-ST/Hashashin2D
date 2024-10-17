using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI coinsCollectedText;
    public List<Transform> startPoints;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;
    private List<EnemyWalk> enemiesInArea = new List<EnemyWalk>(); // Danh sách kẻ thù trong khu vực
    private bool bossDefeated = false; // Trạng thái của boss

    public void TransitionToNextLevel(GameObject player)
    {
        if (currentLevelIndex < levels.Count - 1)
        {
            levels[currentLevelIndex].SetActive(false); // Tắt level hiện tại
            currentLevelIndex++;
            levels[currentLevelIndex].SetActive(true); // Bật level tiếp theo
            MovePlayerToStartPoint(player);
            ResetLevelState(); // Reset trạng thái cho level mới
        }
    }

    public void MovePlayerToStartPoint(GameObject player)
    {
        if (startPoints != null && startPoints.Count > currentLevelIndex)
        {
            player.transform.position = startPoints[currentLevelIndex].position;
        }
    }

    public void Restart()
    {
        ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    }

    private void ResetLevelState()
    {
        enemiesInArea.Clear(); // Xóa danh sách kẻ thù trong khu vực
        bossDefeated = false; // Reset trạng thái boss
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
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
        bossDefeated = true; // Đánh dấu boss đã bị tiêu diệt
        CheckEnemies(); // Kiểm tra xem có cần chuyển điểm xuất phát không
    }

    // Phương thức kiểm tra xem tất cả kẻ thù và boss đã bị tiêu diệt chưa
    private void CheckEnemies()
    {
        if (enemiesInArea.Count == 0 && bossDefeated)
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
            Debug.Log("Moving to the next start point!");
        }
    }
}
