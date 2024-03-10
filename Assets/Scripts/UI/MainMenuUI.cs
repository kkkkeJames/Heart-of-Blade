using UnityEngine;

public class MainMenu : UI
{
    public static MainMenu Instance { get; private set; }
    
    [SerializeField] private ButtonUI newGameButton;
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
        quitGameButton.AddListener(OnQuitGameClicked);
        settingsButton.AddListener(OnSettingsClicked);
        
        Show();
    }

    private void ToggleButtons(bool enable)
    {
        if (enable)
        {
            newGameButton.Enable();
            quitGameButton.Enable();
            settingsButton.Enable();
        }
        else
        {
            newGameButton.Disable();
            quitGameButton.Disable();
            settingsButton.Disable();
        }
    }

    private void OnNewGameClicked()
    {
        Hide();
        Debug.Log("NEW GAME is not currently implemented but here we go...");
        Loader.Load("MainScene");
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

    protected override void LateDeactivate()
    {
        ToggleButtons(false);
    }

    private void OnSettingsClicked()
    {
        Hide();
        OptionsMenu.Instance.Show();
    }
    
    protected override void Activate()
    {
        ToggleButtons(true);
    }
}