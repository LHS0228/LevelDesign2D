using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class Level1Trigger : MonoBehaviour
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

    private laser[] lasers;
    [SerializeField]
    private Light2D lightObj;

    private bool onTrigger = false; //이벤트 여부
    private bool endEvent = false; //한번 실행된 이벤트인지 검사

    private float currentTime;
    private int animCount;

    private void Awake()
    {
        eventCamera.SetActive(false);
        lasers = FindObjectsOfType<laser>();
    }

    private void Start()
    {
        endEvent = LoadBool("Level1_Event", false);
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
            //CharacterSpecialKey characterSpecialKey = collision.GetComponent<CharacterSpecialKey>();
            //characterSpecialKey.onDash = true;
            player.GetComponent<SkillSetting>().AddSkill("Dash");
            foreach(laser laser in lasers)
            {
                if(laser.CompareTag("Laser"))
                {
                    laser.OnLaser();
                }
            }
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
                if(currentTime > 1f)
                {
                    maincamera.SetActive(false);
                    eventCamera.SetActive(true);
                    eventCamera.transform.DOMove(new Vector3(123f, 36.5f, -13.5f), 3);
                    nextCount(2);
                }
                break;
            case 2:
                if (currentTime > 2f)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 10f, 2);
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
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 1f, 1);
                    nextCount(5);
                }
                break;
            case 5:
                if (currentTime > 5f)
                {
                    player.GetComponent<PlayerInput>().GainControl();
                    maincamera.SetActive(true);
                    eventCamera.SetActive(false);
                    eventCamera.transform.DOMove(new Vector3(113f, 32.5f, -13.5f), 1);
                    //SaveBool("Level1_Event", true); // 빌드할꺼면 풀고해야함
                    nextCount(6);
                }
                break;
            case 6:
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
