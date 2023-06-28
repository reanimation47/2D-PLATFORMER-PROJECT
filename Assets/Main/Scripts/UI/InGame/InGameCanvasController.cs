using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Common.InGame;

public class InGameCanvasController : MonoBehaviour
{
    public AlphaMask AlphaMask;
    public AlphaMask AlphaMask_Front;
    public GameObject PauseButton;
    [SerializeField] private GameOverController GameOverController;
    [SerializeField] private StageCompleteController StageCompleteController;
    [SerializeField] private PlayerControlController PlayerControlController;
    [SerializeField] private GamePauseController GamePauseController;
    [SerializeField] private EndlessModeController EndlessModeController;

    private bool CurrentLevelIsEndless = false;

    void Awake()
    {
        InGameCanvasInterface.LoadController(this);
        CurrentLevelIsEndless = IStage.GetCurrentPlayingLevel() > 999;
        Debug.Log("CurrentLevelIsEndless: " + CurrentLevelIsEndless);
    }

    private void Start()
    {
        StartCoroutine(GameIntro());
        EndlessModeController.ToggleEndlessView(CurrentLevelIsEndless);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    //custom

    IEnumerator GameIntro()
    {
        PlayerControlController.ToggleControls(false);
        SetBackGroundFullBlur();
        yield return new WaitForSeconds(0.5f);
        ToggleBackgroundBlur(0f, 0.02f);
        yield return new WaitForSeconds(0.5f);
        PlayerControlController.ToggleControls(true);
    }
    

    private void SetBackGroundFullBlur()
    {
        AlphaMask.SetBackGroundFullBlur();
    }

    private void ToggleBackgroundBlur(float _blur, float _speed)
    {
        AlphaMask.ToggleBackgroundBlur(_blur, _speed);
    }

    private void ToggleFrontGroundBlur(float _blur, float _speed)
    {
        AlphaMask_Front.ToggleBackgroundBlur(_blur, _speed);
    }

    private void ShowGameOverComponents()
    {
        PauseButton.SetActive(false);
        GameOverController.ToggleGroup(true);
        GameOverController.ShowTitle(0.5f);
        GameOverController.ShowBody(2f);
        GameOverController.ShowButtons(2.2f);
        PlayerControlController.ToggleControls(false);
    }
    private void ShowStageCompleteComponents()
    {
        PauseButton.SetActive(false);
        StageCompleteController.ToggleGroup(true);
        StageCompleteController.ShowTitle(0.5f);
        StageCompleteController.ShowBody(2f);
        StageCompleteController.ShowButtons(2.2f);
        PlayerControlController.ToggleControls(false);
    }
    private void ShowStagePauseComponents()
    {
        //GamePauseController.ChangeLerpSpeed(0.5f);
        GamePauseController.ToggleGroup(true);
        PlayerControlController.ToggleControls(false);
    }


    //public methods
    public void ShowGameOverScreen()
    {
        ToggleBackgroundBlur(0.7f, 0.05f);
        ShowGameOverComponents();
    }
    public void ShowStageCompleteScreen()
    {
        ToggleBackgroundBlur(0.7f, 0.05f);
        ShowStageCompleteComponents();
    }
    


    public void RestartGame()
    {
        StartCoroutine(GameRestart());
    }
    IEnumerator GameRestart()
    {
        ToggleFrontGroundBlur(1, 0.1f);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation async_load = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!async_load.isDone)
        {
            yield return null;
        }

    }

    public void BackToStageSelect()
    {
        StartCoroutine(CoBackToStageSelect());
    }
    IEnumerator CoBackToStageSelect()
    {
        ToggleFrontGroundBlur(1, 0.1f);
        yield return new WaitForSeconds(0.5f);
        ICommon.ToggleSkipToStageSelect(true);
        AsyncOperation async_load = SceneManager.LoadSceneAsync(InGameCommon.StartMenuSceneName);
        while (!async_load.isDone)
        {
            yield return null;
        }

    }

    //Pause game
    public void ShowPauseScreen()
    {
        StartCoroutine(CoShowPauseScreen());
    }
    IEnumerator CoShowPauseScreen()
    {
        PauseButton.SetActive(false);
        ToggleBackgroundBlur(0.7f, 1f);
        ShowStagePauseComponents();
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0; // pauses the game
    }
    public void ResumeGame()
    {
        ToggleBackgroundBlur(0, 0.05f);
        GamePauseController.ToggleGroup(false);
        Time.timeScale = 1;
        PlayerControlController.ToggleControls(true);
        PauseButton.SetActive(true);
    }

}
