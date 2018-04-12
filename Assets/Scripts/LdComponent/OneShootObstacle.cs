using UnityEngine;

public class OneShootObstacle : MonoBehaviour {

    [SerializeField]
    private float m_speed = 5f;
    [SerializeField] 
    private float autoDestroyDelay = 2f;

    private void Start()
    {
        Destroy(gameObject, autoDestroyDelay);
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
            GameMng.Instance.SpawnPlayer();
        }
    }
}
