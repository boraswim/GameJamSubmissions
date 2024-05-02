using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MeteorManager : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Vector3 spawnPoint, targetPoint;
    public float speed, delay;
    public UnityEvent OnFinish;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public void SpawnMeteor()
    {
        StartCoroutine("DelayLookAt");
    }
    public IEnumerator DelayLookAt()
    {
        yield return new WaitForSecondsRealtime(delay);
        GameObject meteorObject = Instantiate(meteorPrefab, spawnPoint, Quaternion.identity);
        meteorObject.GetComponent<Meteor>().PlayMeteorAnimation(OnFinish, speed, targetPoint);
        cinemachineVirtualCamera.LookAt = meteorObject.transform;
    }
    void OnValidate()
    {
        if (speed <= 0)
            speed = 1;
    }
}
