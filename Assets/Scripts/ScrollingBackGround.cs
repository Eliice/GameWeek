using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackGround : MonoBehaviour {

    [SerializeField]
    private float m_speed = 1;


    private Renderer m_renderer = null;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }



    void Update () {
        m_renderer.material.mainTextureOffset = new Vector2(Time.time * m_speed, 0f);
	}
}
