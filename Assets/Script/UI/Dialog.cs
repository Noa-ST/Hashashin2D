using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text titleTxt;
    public Text contentTxt;

    public virtual void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    public virtual void UpdateDialog(string title, string content)
    {
        if (titleTxt != null)
        {
            titleTxt.text = title;
        }

        if (contentTxt != null)
        {
            contentTxt.text = content;
        }
    }

    public virtual void Close()
    {
        Show(false);
    }
}
