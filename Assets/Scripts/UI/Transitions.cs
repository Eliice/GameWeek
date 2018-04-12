using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Transitions : MonoBehaviour {

	private UIControler Controler;
	private GameMng GM;

	public Animator Animator;

	void Awake () {
		Animator = this.GetComponent<Animator>();

		if (this.name == "Transitions")
			DontDestroyOnLoad(this);

		GameObject Manager = GameObject.Find("ManagerHolder");
		GM = Manager.GetComponent<GameMng>();
		Controler = Manager.GetComponent<UIControler>();
	}


	public void LoadingScreen()
	{
		Controler.LoadPlayScene();
	}


	public void SwitchScreen()
	{
		Controler.SwitchScreen();
	}


    public void StartTransition()
    {
        Animator.SetTrigger("Death");
    }


    public void Respawn()
    {
        GM.SpawnPlayer();
    }
}
