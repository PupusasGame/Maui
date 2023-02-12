using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HintManagerMiniGame : MonoBehaviour
{
    public GameObject hintPanel;

    [TextAreaAttribute(5,10)]
    public string[] hints;
    public int actualHint;

    public Text hintText;

    // Start is called before the first frame update
    void Start()
    {
        SetHintToText();
    }

    public void OpenhintPanel()
    {
        hintPanel.SetActive(true);
    }

    public void CloseHitPanel()
    {
        hintPanel.SetActive(false);
    }

    public void SetHintToText()
    {
        actualHint = GetComponent<MiniGameManager>().actualWord;
        hintText.text = hints[actualHint];
    }
}
