using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour {

    private bool isStartingScreen = true;

    [SerializeField]
    private GameObject m_startingScreen = null;
    [SerializeField]
    private GameObject m_menuScreen = null;

    [SerializeField]
    private GameObject m_loadingScreen = null;


    [SerializeField]
    private float m_loadingTime = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isStartingScreen)
            return;
        if (Input.anyKey)
        {
            SwitchScreen();
        }
    }

    private void SwitchScreen()
    {
        m_startingScreen.SetActive(false);
        m_menuScreen.SetActive(true);
        isStartingScreen = false;
    }

    public void Exit()
    {
        Application.Quit();
    }


    public void LoadPlayScene()
    {
        m_loadingScreen.SetActive(true);
        m_menuScreen.SetActive(false);
        StartCoroutine(loadingRoutine());
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }


    private void StartGame()
    {
        GameMng.Instance.SpawnPlayer();
        SceneManager.UnloadSceneAsync(0);
    }

    private IEnumerator loadingRoutine()
    {
        float timer = 0f;
        while (timer < m_loadingTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        StartGame();
    }
}
