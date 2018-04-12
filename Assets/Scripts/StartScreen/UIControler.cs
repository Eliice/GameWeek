using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour {

    private bool LogoIsart = true;
    private bool video = false;
    private bool isStartingScreen = false;
	private Transitions Transition;

    [SerializeField]
    private GameObject m_startingScreen = null;
    [SerializeField]
    private GameObject m_menuScreen = null;

    [SerializeField]
    private GameObject m_loadingScreen = null;

    [SerializeField]
    private GameObject m_IsartLogo = null;

    [SerializeField]
    private GameObject m_video = null;

    [SerializeField]
    private GameObject m_bg = null;

    private float currentTime = 0;

    [SerializeField]
    private float m_loadingTime = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(LogoIsart)
        {
            if (currentTime > 2)
            {
                m_IsartLogo.SetActive(false);
                //m_video.SetActive(true);
                LogoIsart = false;
                //video = true;
                currentTime = 0;
                m_bg.SetActive(true);
                m_startingScreen.SetActive(true);
                isStartingScreen = true;
                AudioSource[]  source = Camera.main.GetComponents<AudioSource>();
                foreach (AudioSource item in source)
                {
                    item.Play();
                }
            }
            else
                currentTime += Time.deltaTime;
        }
        if(video)
        {
            //((MovieTexture)m_video.renderer.guiTexture).Play();
        }
        if (Input.anyKey)
        {
            if(isStartingScreen)
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
