using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIControler : MonoBehaviour {

    [SerializeField]
    private Canvas beforVideo = null;

    private bool isStartingScreen = false;
	private Transitions Transition;

    [SerializeField]
    private GameObject m_startingScreen = null;
    [SerializeField]
    private GameObject m_menuScreen = null;

    [SerializeField]
    private GameObject m_loadingScreen = null;

    private float currentTime = 0;


    [SerializeField]
    private GameObject bg = null;
    [SerializeField]
    private float m_loadingTime = 2f;

    private AudioSource[] audio = null;

    private bool videoSeen = false;
	// Use this for initialization
	void Start () {
        audio = Camera.main.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if(currentTime < 7f)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            if (!videoSeen)
            {
                m_startingScreen.SetActive(true);
                bg.SetActive(true);
                foreach (AudioSource item in audio)
                {
                    item.Play();
                }
                videoSeen = true;
                isStartingScreen = true;
                GetComponent<VideoPlayer>().enabled = false;
            }
        }


        if (Input.anyKey)
        {
            if (isStartingScreen)
            {
                
                Transition = GameObject.Find("Canvas").GetComponent<Transitions>();
                Transition.Animator.SetTrigger("SwitchScreen");
                isStartingScreen = false;
            }
        }
    }

    public void SwitchScreen()
    {
        m_startingScreen.SetActive(false);
        m_menuScreen.SetActive(true);
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
        Transition = GameObject.Find("Transitions").GetComponent<Transitions>();
        Transition.Animator.SetTrigger("LoadScene");
    }
}
