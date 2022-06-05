using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LabyrinthCreator _creator = null;
    [SerializeField] private LabyrinthSolver _solver = null;
    [SerializeField] private TMP_Dropdown _picker = null;
    [SerializeField] private Button _createButton = null;
    [SerializeField] private Button _solveButton = null;
    [SerializeField] private List<TextAsset> _labyrinthTextFiles = new List<TextAsset>();

    private void Awake()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (TextAsset file in _labyrinthTextFiles) options.Add(new TMP_Dropdown.OptionData(file.name));
        _picker.ClearOptions();
        _picker.AddOptions(options);

        _createButton.onClick.AddListener(() => _creator.CreateLabyrinthFromFile(_labyrinthTextFiles[_picker.value]));
        _solveButton.onClick.AddListener(() => _solver.SolveLabyrinth());
    }
}