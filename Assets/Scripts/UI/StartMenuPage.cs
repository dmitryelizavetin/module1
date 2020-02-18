using UnityEngine;
using UnityEngine.UI;

public class StartMenuPage : MonoBehaviour
{
    public string BattleSceneName;
    public string NewBattleSceneName;
    public Button PlayButton;
    public Button NewPlayButton;
    public LoadingLogicPage LoadingLogic;

    private void Awake()
    {
        PlayButton.onClick.AddListener(PlayGame);
        NewPlayButton.onClick.AddListener(PlayNewGame);
    }

    private void PlayGame()
    {
        LoadingLogic.LoadScene(BattleSceneName);
    }

    private void PlayNewGame()
    {
        LoadingLogic.LoadScene(NewBattleSceneName);
    }

}
