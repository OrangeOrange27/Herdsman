using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : UIBehaviour
{
    [SerializeField] private TMP_Text _text;
    public void UpdateUI(int current, int max)
    {
        _text.text = $"{current}/{max}";
    }
}
