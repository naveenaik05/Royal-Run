using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpParticleSystem;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 60f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifire = 5f;

    CinemachineCamera cinemachineCamera;

    void Awake() 
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();   
    }
    public void ChangeCameraFOV(float speedAmout)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmout));
        // Debug.Log("Speed amunt is " + speedAmout);
        if(speedAmout > 0)
        {
            speedUpParticleSystem.Play();
        }
        else
        {
            speedUpParticleSystem.Stop();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmout)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV+speedAmout * zoomSpeedModifire, minFOV, maxFOV);

        float elapsedTime = 0f;

        while(elapsedTime<zoomDuration)
        {
            float t = elapsedTime/zoomDuration;
            elapsedTime += Time.deltaTime;

            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV,targetFOV,t);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV; 
    }


}
