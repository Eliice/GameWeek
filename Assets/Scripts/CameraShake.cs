﻿using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    private Vector3 _originalPos;
    public static CameraShake _instance;

    void Awake()
    {
        _originalPos = transform.localPosition;

        _instance = this;
    }

    private void Update()
    {
        _originalPos.x += gameObject.GetComponent<Scrolling>().CameraSpeed;
    }

    public static void Shake(float duration, float amount)
    {
        _instance.StopAllCoroutines();
        _instance.StartCoroutine(_instance.cShake(duration, amount));
    }

    public IEnumerator cShake(float duration, float amount)
    {
        Debug.Log("Shake your booty !");
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= Time.deltaTime;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }
}