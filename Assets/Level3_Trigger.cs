using DG.Tweening;
using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Level3_Trigger : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private GameObject maincamera;
    [SerializeField]
    private GameObject eventCamera;

    [Header("Player")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerCharacter character;
    [SerializeField]
    private Light2D lightObj;

    private bool onTrigger = false; //이벤트 여부
    private bool endEvent = false; //한번 발생한 이벤트?

    private float currentTime;
    private int animCount;

    private void Awake()
    {
        eventCamera.SetActive(false);
    }

    private void Start()
    {
        endEvent = LoadBool("Level3_Event", false);
    }

    private void FixedUpdate()
    {
        if (!endEvent && onTrigger)
        {
            currentTime += Time.deltaTime;
            StartEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onTrigger)
        {
            onTrigger = true;
            player.GetComponent<SkillSetting>().AddSkill("Glide");
        }
    }
    private void StartEvent()
    {
        switch (animCount)
        {
            case 0:
                player.GetComponent<PlayerInput>().ReleaseControl(true);
                player.GetComponent<Animator>().SetFloat("HorizontalSpeed", 0);
                player.GetComponent<Animator>().SetFloat("VerticalSpeed", 0);
                nextCount(1);
                break;

            case 1:
                if (currentTime > 1f)
                {
                    maincamera.SetActive(false);
                    eventCamera.SetActive(true);
                    eventCamera.transform.DOMove(new Vector3(30f, 38.5f, -13.5f), 2);
                    nextCount(2);
                }
                break;
            case 2:
                if (currentTime > 2f)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 2.5f, 2);
                    nextCount(3);
                }
                break;
            case 3:
                if (currentTime > 3f)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 0, 2);
                    nextCount(4);
                }
                break;
            case 4:
                if (currentTime > 4f)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 2f, 2);
                    nextCount(5);
                }
                break;
            case 5:
                if (currentTime > 5f)
                {
                    eventCamera.transform.DOMove(new Vector3(74f, 24f, -13.5f), 2);
                    nextCount(6);
                }
                break;
            case 6:
                if (currentTime > 6f)
                {
                    player.GetComponent<PlayerInput>().GainControl();
                    maincamera.SetActive(true);
                    eventCamera.SetActive(false);
                    //SaveBool("Level3_Event", true); //빌드할꺼면 이거 풀고해야함
                    nextCount(7);                }
                break;
            case 7:
                if (currentTime > 7f)
                {
                    nextCount(8);
                }
                break;
            case 8:
                break;



        }



    }

    void nextCount(int num)
    {
        currentTime = 0;
        animCount = num;
    }

    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool LoadBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }
}
