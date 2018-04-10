using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour {

    private bool isStartingScreen = true;

    [SerializeField]
    private GameObject m_startingScreen = null;
    [SerializeField]
    private GameObject m_menuScreen = null;


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
        SceneManager.LoadScene(1);
        GameMng.Instance.SpawnPlayer();
    }
}
