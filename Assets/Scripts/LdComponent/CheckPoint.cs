using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameMng.Instance.Respawn = transform.position;
            Player.Instance.ResetStamina();
        }
    }
}
