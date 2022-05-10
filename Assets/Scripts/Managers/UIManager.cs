using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Activity Markers")]
    public ActivityMarker reverseMarker;
    public ActivityMarker playMarker;

    [Header("Options Menu")]
    public OptionsMenu optionsMenu;
    public bool showOptionsMenu;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);
    }

    private void Update()
    {
        UpdateActivityMarkers();

        SimulationManager.Instance.cameraMovement.panDown = showOptionsMenu;
    }

    private void UpdateActivityMarkers()
    {
        reverseMarker.active = SimulationManager.Instance.IsPlaying() && SimulationManager.Instance.GetPlayingSpeed() < 0;
        playMarker.active = SimulationManager.Instance.IsPlaying() && SimulationManager.Instance.GetPlayingSpeed() > 0;
    }

    public void OnMenuButtonPressed()
    {
        showOptionsMenu = true;
        optionsMenu.Show();
    }

    public void OnNewSequenceButtonPressed()
    {
        SimulationManager.Instance.GenerateNewSequence();
        SimulationManager.Instance.RunSimulation();
        SimulationManager.Instance.Rewind();
    }

    public void OnReverseButtonPressed()
    {
        SimulationManager.Instance.PlayBackward();
    }

    public void OnPlayButtonPressed()
    {
        SimulationManager.Instance.PlayForward();
    }

    public void OnRewindButtonPressed()
    {
        SimulationManager.Instance.Rewind();
    }

    public void OnForwardButtonPressed()
    {
        SimulationManager.Instance.Forward();
    }
}
