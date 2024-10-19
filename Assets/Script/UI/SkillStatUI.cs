using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillStatUI : MonoBehaviour
{
    [SerializeField] private Text m_statLabelTxt;
    [SerializeField] private Text m_statValueTxt;
    [SerializeField] private Text m_statUpValueTxt;

    public void UpdateStat(string label, string value, string upValue)
    {
        if (m_statLabelTxt != null) m_statLabelTxt.text = label;
        if (m_statValueTxt != null) m_statValueTxt.text = value;
        if (m_statUpValueTxt != null) m_statUpValueTxt.text = upValue;
    }
}
