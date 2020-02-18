using UnityEngine;
using UnityEngine.UI;

public class InBattlePage : MonoBehaviour
{
    public GameController Controller;
    public Button AttackButton;
    public Button SwitchButton;
    public Button PauseButton;
    public Button ContinueButton;
    public Button ReloadButton;
    public Button MainMenuButton;
    public GameObject VisualPartMenu;

    private void Awake()
    {
        VisualPartMenu.SetActive(false);
        AttackButton.onClick.AddListener(OnAttackButtonClick);
        SwitchButton.onClick.AddListener(() => Controller.SwitchCharacter());
        PauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnAttackButtonClick()
    {
        Controller.PlayerMove();
    }

    private void OnPauseButtonClick()
    {
        VisualPartMenu.SetActive(true);
    }

}
