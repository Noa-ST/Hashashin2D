using UnityEngine;
using UnityEngine.UI;

public class ImageFilled : MonoBehaviour
{

    [SerializeField] private Image m_filledImg;

    private Transform m_root;

    public Transform Root { get => m_root; set => m_root = value; }

    public void UpdateValue(float curVal, float totalVal, bool isReverse = false)
    {
        Show(true);
        if (m_filledImg == null) return;

        float rate = 0;

        if (isReverse)
        {
            rate = 1f - (curVal / totalVal);
        }
        else
        {
            rate = curVal / totalVal;
        }

        m_filledImg.fillAmount = rate;
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    private void Update()
    {
        if (m_root)
        {
            transform.localRotation = m_root.rotation;
        }
    }
}
