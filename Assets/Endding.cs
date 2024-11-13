using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Endding : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI enddingText;

    private void Start()
    {
        enddingText.DOColor(new Color(1, 1, 1, 1), 5);

    }
}
