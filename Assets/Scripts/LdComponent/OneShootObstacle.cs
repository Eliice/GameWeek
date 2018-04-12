using UnityEngine;

public class OneShootObstacle : MonoBehaviour {

    [SerializeField]
    private float m_speed = 5f;
    [SerializeField] 
    private float autoDestroyDelay = 2f;

    private AudioSource m_audiSource = null;
    private void Start()
    {
        if (autoDestroyDelay == 0)
            return;
        else
            Destroy(gameObject, autoDestroyDelay);
        m_audiSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= m_speed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!m_audiSource)
                m_audiSource = GetComponent<AudioSource>();
            m_audiSource.Play();
            GameMng.Instance.SpawnPlayer();
        }
    }
}
