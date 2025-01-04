using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro.EditorUtilities;
using TMPro;

public class BarrelDisplay : MonoBehaviour
{
    [SerializeField] private Barrel _barrel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _radiusText;
    [SerializeField] private Image _image;

    private void Start()
    {
        _titleText.text = _barrel.title;
        _descriptionText.text = _barrel.description;
        _radiusText.text = _barrel.radius.ToString();

        _image.sprite = _barrel.sprite;
    }
}
