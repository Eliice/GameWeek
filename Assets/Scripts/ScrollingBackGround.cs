using UnityEngine;

public class ScrollingBackGround : MonoBehaviour {

    [SerializeField]
    private float firstLayerSpeed = 1f;
    [SerializeField]
    private float secondLayerSpeed = 1f;

    [SerializeField]
    private GameObject m_bgFirstLayer1 = null;
    private Vector3 initalPosBgFirstLayeur1;
    [SerializeField]
    private GameObject m_bgFirstLayer2 = null;
    private Vector3 initalPosBgFirstLayeur2;
    [SerializeField]
    private GameObject m_bgFirstLayer3 = null;
    private Vector3 initalPosBgFirstLayeur3;


    [SerializeField]
    private GameObject m_bgSecondLayer1 = null;
    private Vector3 initialPosBgSecondLayeur1;
    [SerializeField]
    private GameObject m_bgSecondLayer2 = null;
    private Vector3 initialPosBgSecondLayeur2;
    [SerializeField]
    private GameObject m_bgSecondLayer3 = null;
    private Vector3 initialPosBgSecondLayeur3;




    [SerializeField]
    private Transform m_frontAnchor = null;
    [SerializeField]
    private Transform m_backAnchor = null;


    private Scrolling m_scrollingData= null;

    private void Start()
    {
        m_scrollingData = Camera.main.GetComponent<Scrolling>();

        initalPosBgFirstLayeur1 = m_bgFirstLayer1.transform.position;
        initalPosBgFirstLayeur2 = m_bgFirstLayer2.transform.position;
        initalPosBgFirstLayeur3 = m_bgFirstLayer3.transform.position;

        initialPosBgSecondLayeur1 = m_bgSecondLayer1.transform.position;
        initialPosBgSecondLayeur2 = m_bgSecondLayer2.transform.position;
        initialPosBgSecondLayeur3 = m_bgSecondLayer3.transform.position;
    }



    void Update ()
    {
        if(!m_scrollingData)
            m_scrollingData = Camera.main.GetComponent<Scrolling>();
        float scrollingCoef = m_scrollingData.CameraSpeed;

        m_bgFirstLayer1.transform.Translate(firstLayerSpeed  * Time.deltaTime * scrollingCoef, 0, 0);
        m_bgFirstLayer2.transform.Translate(firstLayerSpeed * Time.deltaTime * scrollingCoef, 0, 0);
        m_bgFirstLayer3.transform.Translate(firstLayerSpeed * Time.deltaTime * scrollingCoef, 0, 0);
        
        m_bgSecondLayer1.transform.Translate(secondLayerSpeed * Time.deltaTime * scrollingCoef, 0, 0);
        m_bgSecondLayer2.transform.Translate(secondLayerSpeed * Time.deltaTime * scrollingCoef, 0, 0);
        m_bgSecondLayer3.transform.Translate(secondLayerSpeed * Time.deltaTime * scrollingCoef, 0, 0);

        if (checkPosLayerOne())
            ReplaceLayerOne();
        if (CheckPosLayerTwo())
            ReplaceLayerTwo();
	}

    private void ReplaceLayerTwo()
    {
        m_bgSecondLayer1.transform.position = initialPosBgSecondLayeur1;
        m_bgSecondLayer2.transform.position = initialPosBgSecondLayeur2;
        m_bgSecondLayer3.transform.position = initialPosBgSecondLayeur3;
    }

    private bool CheckPosLayerTwo()
    {
        if (m_frontAnchor.position.x <= m_bgSecondLayer2.transform.position.x)
            return true;
        else
            return false;
    }

    private void ReplaceLayerOne()
    {
        m_bgFirstLayer1.transform.position = initalPosBgFirstLayeur1;
        m_bgFirstLayer2.transform.position = initalPosBgFirstLayeur2;
        m_bgFirstLayer3.transform.position = initalPosBgFirstLayeur3;
    }

    private bool checkPosLayerOne()
    {
        if (m_frontAnchor.position.x <= m_bgFirstLayer2.transform.position.x)
            return true;
        else
            return false;
    }

}
