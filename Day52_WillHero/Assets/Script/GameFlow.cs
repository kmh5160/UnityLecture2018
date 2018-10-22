using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : Singleton<GameFlow> {
    public RectTransform gameSplash;
    public RectTransform eventUI;
    public RectTransform titleScene;
    public RectTransform playScene;
    public RectTransform restartGameUI;

    [HideInInspector]
    public Animator fsm;
    [HideInInspector]
    public PlayerController player;

    protected GameFlow() { }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        fsm = GetComponent<Animator>();
        Physics.gravity *= 4f;

        StartCoroutine(RestartGame());
	}

    public IEnumerator RestartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Play");
        while (!asyncLoad.isDone)
        {
            print(asyncLoad.progress);
            yield return null;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
