using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionGameButton;

    public override void Initialize()
    {
        startGameButton.onClick.AddListener(StartGameHandler);
        optionGameButton.onClick.AddListener(OpenOptionsHandler);
    }

    protected override void OpenEnd()
    {
        base.OpenEnd();
        startGameButton.interectable = true;
        optionGameButton.interectable = true;
    } 

    protected override void CloseStart()
    {
        base.CloseStart();
        startGameButton.interectable = false;
        optionGameButton.interectable = false;
    }

    private void StartGameHandler()
    {
        GameManager.Instance.StartGame();
        WindowsService.Instance.WindowsService.SnowWindow<GameplayWindow>(false);
    }

    private void OpenOptionsHandler()
    {
        Hide(false);
        GameManager.Instance.WindowsService.SnowWindow<OptionsWindow>(false);
    }
}
