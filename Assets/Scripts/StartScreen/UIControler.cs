using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void Exit()
    {
        Application.Quit();
    }
}
