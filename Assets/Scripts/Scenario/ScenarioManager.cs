using System.Collections;
using UnityEngine;

public partial class ScenarioManager // IO
{
    public bool isWaveOver { get; private set; }
    public int wave { get; private set; }
    public int maxWave { get; private set; }



    public void Init() => _Init();
    
}

public partial class ScenarioManager // SerializeField
{
    [SerializeField] private GameManager _gameManager;
}

public partial class ScenarioManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        _StartNewWave();
    }
}

public partial class ScenarioManager // body
{
    private string _loadPath;
    private string _fileName;

    private ScenarioLists _scenarioLists;
    private JsonConverter _jsonConverter;

    private ScenarioList _currentList;


    private void _Init()
    {
        _loadPath = Application.dataPath + "/JsonData";
        _fileName = "EnemyLists";

        _jsonConverter = new JsonConverter();
        _scenarioLists = _jsonConverter.LoadJsonFile<ScenarioLists>(_loadPath, _fileName);
        if (_scenarioLists.ValidateLists() == false)
        {
            Debug.LogError("ScenarioLists Validation Fails!");
        }

        wave = 0;
        maxWave = _scenarioLists.waveList.Count;
    }

    private void _StartNewWave()
    {
        if (wave >= maxWave)
        {
            return;
        }
        else if (_gameManager.enemyManager.State == EMState.waiting)
        {
            _currentList = new ScenarioList(_scenarioLists.waveList[wave]);
            float waveStartDelay = _scenarioLists.waveList[wave].waveStartDelay;
            _gameManager.enemyManager.InjectScenario(_currentList, waveStartDelay);
            wave = wave + 1;
        }
    }
}