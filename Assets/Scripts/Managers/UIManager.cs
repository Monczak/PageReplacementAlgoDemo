﻿using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Activity Markers")]
    public ActivityMarker reverseMarker;
    public ActivityMarker playMarker;

    [Header("Options Menu")]
    public OptionsMenu optionsMenu;
    public bool showOptionsMenu;

    private Controls controls;
    private bool step;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);

        controls = new Controls();

        controls.Global.Step.performed += OnStepKeyPressed;
        controls.Global.Step.canceled += OnStepKeyReleased;

        controls.Enable();
    }

    private void OnStepKeyReleased(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        step = false;
    }

    private void OnStepKeyPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        step = true;
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
        if (step)
        {
            if (SimulationManager.Instance.IsPlaying() && SimulationManager.Instance.GetPlayingSpeed() > 0)
                SimulationManager.Instance.Pause();
            SimulationManager.Instance.StepBack();
        }
        else
            SimulationManager.Instance.PlayBackward();
    }

    public void OnPlayButtonPressed()
    {
        if (step)
        {
            if (SimulationManager.Instance.IsPlaying() && SimulationManager.Instance.GetPlayingSpeed() < 0)
                SimulationManager.Instance.Pause();
            SimulationManager.Instance.StepForward();
        }
        else
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
