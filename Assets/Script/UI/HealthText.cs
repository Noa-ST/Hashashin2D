using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 30, 0); 
    public float timeToFade = 2f;  

    private RectTransform _textTransform;
    private TextMeshProUGUI _textMPR;
    private float _timeElapsed = 0f;
    private Color _startColor;

    private void Awake()
    {
        _textTransform = GetComponent<RectTransform>();
        _textMPR = GetComponent<TextMeshProUGUI>();
        _startColor = _textMPR.color;
    }

    private void Update()
    {
        // Di chuyển text từ từ lên phía trên
        _textTransform.position += moveSpeed * Time.deltaTime;

        // Đếm thời gian
        _timeElapsed += Time.deltaTime;

        // Kiểm tra thời gian để thực hiện quá trình mờ dần
        if (_timeElapsed < timeToFade)
        {
            // Làm mờ dần từ từ theo thời gian
            float fadeAlpha = _startColor.a * Mathf.Pow(1 - (_timeElapsed / timeToFade), 2);  // Mờ dần mượt mà hơn
            _textMPR.color = new Color(_startColor.r, _startColor.g, _startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);  // Xóa text sau khi mờ hoàn toàn
        }
    }
}
