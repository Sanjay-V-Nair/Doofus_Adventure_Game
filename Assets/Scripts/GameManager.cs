using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    public static float score = -1;

    public GroundScript groundScript;
    [SerializeField]private GameObject GameOverUI;
    [SerializeField]private GameObject GamePauseUI;
    [SerializeField] private AudioSource bgAudio;

    public bool isGameOver = false;

    private string mainMenuScene = "Start_Scene";

    private void Start() {
        TileScript.OnEnterNewTile += UpdateScore;
        groundScript.OnPlayerTouchGround += EnableGameOverScreen;
    }

    private void Update() {
        if (isGameOver) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Toggle();
        }
    }

    private void EnableGameOverScreen(object sender, EventArgs e) {
        EndGame();
    }

    private void EndGame() {
        GameOverUI.SetActive(true);
        isGameOver = true;
        Time.timeScale = 0;
    }

    public void Toggle() {
        GamePauseUI.SetActive(!GamePauseUI.activeSelf);
        if (GamePauseUI.activeSelf) {
            Time.timeScale = 0;
            bgAudio.Pause();
        }
        else {
            Time.timeScale = 1;
            bgAudio.Play();
        }
    }

    private void UpdateScore(object sender, EventArgs e) {
        score++;
        scoreText.text = score.ToString();
    }

    public void GoToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Retry() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() {
        Application.Quit();
    }
    
}
