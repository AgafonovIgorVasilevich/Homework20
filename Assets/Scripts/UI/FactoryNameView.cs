using UnityEngine;
using TMPro;

public class FactoryNameView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Show(string name) => _text.text = name;
}