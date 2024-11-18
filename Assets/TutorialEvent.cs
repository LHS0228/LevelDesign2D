using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Gamekit2D;
using UnityEngine.Rendering.Universal;

public class TutorialEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerCharacter playerCharacter;
    [SerializeField, Header("[Ä«¸Þ¶ó ¼¼ÆÃ]")]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject animCamera;

    [SerializeField, Header("[ºû]")]
    private Light2D lightPart;

    [SerializeField, Header("[¾Æ·§ ¹Ù´Ú]")]
    private GameObject leftFloor;
    [SerializeField]
    private GameObject rightFloor;

    public bool onEvent;
    private int animCount;
    private float runTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(onEvent)
        {
            runTime += Time.deltaTime;
            EventAnim();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onEvent)
        {
            onEvent = true;
        }
    }

    void EventAnim()
    {
        switch(animCount)
        {
            case 0:
                player.GetComponent<PlayerInput>().ReleaseControl(true);
                player.GetComponent<Animator>().SetFloat("HorizontalSpeed", 0);
                player.GetComponent<Animator>().SetFloat("VerticalSpeed", 0);
                nextCount(1);
                break;
            case 1:
                if(runTime > 2f)
                {
                    mainCamera.SetActive(false);
                    animCamera.SetActive(true);
                    animCamera.transform.DOMove(new Vector3(108.3f, 38.7f, -13.5f), 3);
     
                    nextCount(2);
                }
                break;
            case 2:
                if(runTime > 3)
                {
                    DOTween.To(() => lightPart.intensity, x => lightPart.intensity = x, 10, 3);
                    nextCount(3);
                }
                break;
            case 3:
                if (runTime > 3)
                {
                    DOTween.To(() => lightPart.intensity, x => lightPart.intensity = x, 2.5f, 3);
                    nextCount(4);
                }
                break;
            case 4:
                if(runTime > 3)
                {
                    animCamera.transform.DOMove(new Vector3(105.9f, 35.9f, -13.5f), 5);
                    DOTween.To(() => animCamera.GetComponent<Camera>().fieldOfView, x => animCamera.GetComponent<Camera>().fieldOfView = x, 79.3f, 5);
                    nextCount(5);
                }
                break;
            case 5:
                if(runTime > 5)
                {
                    DOTween.To(() => lightPart.intensity, x => lightPart.intensity = x, 3f, 3);
                    DOTween.To(() => lightPart.pointLightOuterRadius, x => lightPart.pointLightOuterRadius = x, 91.55f, 3);
                    nextCount(6);
                }
                break;
            case 6:
                if(runTime > 2)
                {
                    leftFloor.transform.DOMoveX(-3.6f, 3);
                    rightFloor.transform.DOMoveX(2.6f, 3);
                    nextCount(7);
                }
                break;
            case 7:
                if(runTime > 3)
                {
                    animCamera.transform.DOMove(new Vector3(90.95f, 27.37f, -13.5f), 3);
                    DOTween.To(() => animCamera.GetComponent<Camera>().fieldOfView, x => animCamera.GetComponent<Camera>().fieldOfView = x, 46, 3);
                    nextCount(8);
                }
                break;
            case 8:
                if(runTime > 3)
                {
                    player.GetComponent<PlayerInput>().GainControl();
                    mainCamera.SetActive(true);
                    animCamera.SetActive(false);
                    nextCount(9);
                }
                break;
            case 9:
                break;
        }
    }

    void nextCount(int num)
    {
        runTime = 0;
        animCount = num;
    }
}