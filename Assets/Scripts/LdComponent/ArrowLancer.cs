using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLancer : MonoBehaviour {

    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotStartPoint;
    [SerializeField] private float fireRate = 60;

    private float counter = 0;
    private bool canShoot = false;

    void Update()
    {
        if (!canShoot)
            return;

        counter++;
        if (counter == fireRate)
        {
            Shoot();
            counter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canShoot = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canShoot = false;
    }

    private void Shoot()
    {
        Instantiate(shot, shotStartPoint.position, transform.rotation);
    }
}
