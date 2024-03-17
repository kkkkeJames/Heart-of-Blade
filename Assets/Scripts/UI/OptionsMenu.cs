using UnityEngine;

public class OptionsMenu : UI
{
    public static OptionsMenu Instance { get; private set; }
    
    [SerializeField] private ButtonUI difficultySettingButton;
    [SerializeField] private ButtonUI sfxSettingButton;
    [SerializeField] private ButtonUI musicSettingButton;
    [SerializeField] private ButtonUI backToMainMenuButton;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Start() 
    {
        difficultySettingButton.AddListener(OnDifficultyButtonClicked);
        sfxSettingButton.AddListener(OnSFXButtonClicked);
        musicSettingButton.AddListener(OnMusicButtonClicked);
        backToMainMenuButton.AddListener(OnBackClicked);

        Hide();
    }

    private void ToggleButtons(bool enable)
    {
        if (enable)
        {
            difficultySettingButton.Enable();
            sfxSettingButton.Enable();
            musicSettingButton.Enable();
            backToMainMenuButton.Enable();
        }
        else
        {
            difficultySettingButton.Disable();
            sfxSettingButton.Disable();
            musicSettingButton.Disable();
            backToMainMenuButton.Disable();
        }
    }

    private void OnDifficultyButtonClicked()
    {
        // TODO: change difficulty of game here
    }
    
    private void OnSFXButtonClicked()
    {
        // TOOD: change SFX volume here
    }

    private void OnMusicButtonClicked()
    {
        // TODO: change music volume here
    }

    private void OnBackClicked()
    {
        Hide();
        MainMenu.Instance.Show();
    }
    
    protected override void LateDeactivate()
    {
        ToggleButtons(false);
    }
    
    protected override void Activate()
    {
        ToggleButtons(true);
    }
}