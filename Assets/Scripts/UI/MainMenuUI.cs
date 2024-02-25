using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class MainMenu : UI
{
    public static MainMenu Instance { get; private set; }
    
    [SerializeField] private ButtonUI newGameButton;
    [SerializeField] private ButtonUI continueGameButton;
    [SerializeField] private ButtonUI loadGameButton;
    [SerializeField] private ButtonUI quitGameButton;
    [SerializeField] private ButtonUI settingsButton;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Start() 
    {
        newGameButton.AddListener(OnNewGameClicked);
        continueGameButton.AddListener(OnContinueGameClicked);
        loadGameButton.AddListener(OnLoadGameClicked);
        quitGameButton.AddListener(OnQuitGameClicked);
        settingsButton.AddListener(OnSettingsClicked);
        
        Show();
    }

    private void ToggleButtons(bool enable)
    {
        if (enable)
        {
            newGameButton.Enable();
            continueGameButton.Enable();
            loadGameButton.Enable();
            quitGameButton.Enable();
            settingsButton.Enable();
        }
        else
        {
            newGameButton.Disable();
            continueGameButton.Disable();
            loadGameButton.Disable();
            quitGameButton.Disable();
            settingsButton.Disable();
        }
    }

    private void OnNewGameClicked()
    {
        Hide();
        Debug.Log("NEW GAME!");
        // SaveSlotsMenu.Instance.DisplayNewGameMenu();
    }

    private void OnContinueGameClicked()
    {
        ToggleButtons(false);
        Debug.Log("CONTINUE");
        // Loader.Load(DataPersistenceManager.Instance.GameData.LastCheckPointScene);
    }
    
    private void OnLoadGameClicked() 
    {
        Hide();
        Debug.Log("LOAD GAME");
        // SaveSlotsMenu.Instance.DisplayLoadGameMenu();
    }

    private void OnQuitGameClicked()
    {
        Hide();
        ConfirmationPopupMenu.Instance.ActivateMenu("Quit the Game?", () =>
        {
            Application.Quit();
        }, () =>
        {
            Show();
        });
    }

    protected override void Deactivate()
    {
        ToggleButtons(false);
    }

    private void OnSettingsClicked()
    {
        Hide();
        // OptionsMainMenu.Instance.Show();
    }
    
    protected override void Activate()
    {
        ToggleButtons(true);
        // if (!DataPersistenceManager.Instance.HasGameData()) 
        // {
        //     continueGameButton.Disable();
        //     loadGameButton.Disable();
        // }
    }
}