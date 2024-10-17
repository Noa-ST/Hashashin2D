using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject damageTextPb;
    public GameObject healthTextPb;
    public Canvas gameCanvas;
    private Camera _mainCamera;

    protected override void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, float damageReceived)
    {
        // Lấy vị trí của nhân vật trong không gian world và chuyển đổi sang vị trí trong không gian màn hình
        Vector3 spawnPosition = _mainCamera.WorldToScreenPoint(character.transform.position);

        // Tạo và gán giá trị cho TextMeshPro
        TMP_Text tmpText = Instantiate(damageTextPb, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // Lấy vị trí của nhân vật trong không gian world và chuyển đổi sang vị trí trong không gian màn hình
        Vector3 spawnPosition = _mainCamera.WorldToScreenPoint(character.transform.position);

        // Tạo và gán giá trị cho TextMeshPro
        TMP_Text tmpText = Instantiate(healthTextPb, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
}


