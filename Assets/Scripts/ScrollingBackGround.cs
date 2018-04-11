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
        initalPosBgFirstLayeur1 = m_bgFirstLayer1.transform.localPosition;
        initalPosBgFirstLayeur2 = m_bgFirstLayer2.transform.localPosition;
        initalPosBgFirstLayeur3 = m_bgFirstLayer3.transform.localPosition;

        initialPosBgSecondLayeur1 = m_bgSecondLayer1.transform.localPosition;
        initialPosBgSecondLayeur2 = m_bgSecondLayer2.transform.localPosition;
        initialPosBgSecondLayeur3 = m_bgSecondLayer3.transform.localPosition;
    }



    void Update ()
    {
        if (!m_scrollingData)
            m_scrollingData = GetComponent<Scrolling>();
        float scrollingCoef = m_scrollingData.CameraSpeed;

        Vector3 pos = m_bgFirstLayer1.transform.localPosition;
        pos.x += firstLayerSpeed * Time.deltaTime * scrollingCoef;
        m_bgFirstLayer1.transform.localPosition = pos;

        pos = m_bgFirstLayer2.transform.localPosition;
        pos.x += firstLayerSpeed * Time.deltaTime * scrollingCoef;
        m_bgFirstLayer2.transform.localPosition = pos;

        pos = m_bgFirstLayer3.transform.localPosition;
        pos.x += firstLayerSpeed * Time.deltaTime * scrollingCoef;
        m_bgFirstLayer3.transform.localPosition = pos;



        pos = m_bgSecondLayer1.transform.localPosition;
        pos.x += secondLayerSpeed * Time.deltaTime * scrollingCoef;
        m_bgSecondLayer1.transform.localPosition = pos;

        pos = m_bgSecondLayer2.transform.localPosition;
        pos.x += secondLayerSpeed * Time.deltaTime * scrollingCoef;
        m_bgSecondLayer2.transform.localPosition = pos;

        pos = m_bgSecondLayer3.transform.localPosition;
        pos.x += secondLayerSpeed * Time.deltaTime * scrollingCoef;
        m_bgSecondLayer3.transform.localPosition = pos;

        if (checkPosLayerOne())
            ReplaceLayerOne();
        if (CheckPosLayerTwo())
            ReplaceLayerTwo();
	}

    private void ReplaceLayerTwo()
    {
        m_bgSecondLayer1.transform.localPosition = initialPosBgSecondLayeur1;
        m_bgSecondLayer2.transform.localPosition = initialPosBgSecondLayeur2;
        m_bgSecondLayer3.transform.localPosition = initialPosBgSecondLayeur3;
    }

    private bool CheckPosLayerTwo()
    {
        if (m_frontAnchor.localPosition.x <= m_bgSecondLayer2.transform.localPosition.x)
            return true;
        else
            return false;
    }

    private void ReplaceLayerOne()
    {
        m_bgFirstLayer1.transform.localPosition = initalPosBgFirstLayeur1;
        m_bgFirstLayer2.transform.localPosition = initalPosBgFirstLayeur2;
        m_bgFirstLayer3.transform.localPosition = initalPosBgFirstLayeur3;
    }

    private bool checkPosLayerOne()
    {
        if (m_frontAnchor.localPosition.x <= m_bgFirstLayer2.transform.localPosition.x)
            return true;
        else
            return false;
    }

}
