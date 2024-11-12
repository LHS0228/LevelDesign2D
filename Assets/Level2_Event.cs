using DG.Tweening;
using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Level2_Event : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject animCamera;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject upBlock;
    [SerializeField]
    private GameObject downBlock;
    [SerializeField]
    private Light2D lightObj;

    private float runTime;
    [SerializeField]
    private float animCount;
    [SerializeField]
    private bool onEvent;
    [SerializeField]
    private bool endEvent;

    private void Start()
    {
        endEvent = LoadBool("Level2_Event", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!endEvent && onEvent)
        {
            Anim();
            runTime += Time.deltaTime;
        }
    }

    private void Anim()
    {
        switch (animCount)
        {
            case 0:
                player.GetComponent<PlayerInput>().ReleaseControl(true);
                player.GetComponent<Animator>().SetFloat("HorizontalSpeed", 0);
                player.GetComponent<Animator>().SetFloat("VerticalSpeed", 0);
                NextCount(1);
                break;
            case 1:
                if(runTime >= 2f)
                {
                    animCamera.transform.position = mainCamera.transform.position;
                    animCamera.SetActive(true);
                    mainCamera.SetActive(false);
                    NextCount(2);
                }
                break;
            case 2:
                if(runTime >= 2)
                {
                    animCamera.transform.DOMove(new Vector3(93.34f, 28.6f, -18), 3);
                    NextCount(3);
                }
                break;
            case 3:
                if (runTime >= 3)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 10, 2);
                    NextCount(4);
                }
                break;

            case 4:
                if (runTime >= 2)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 0, 2);
                    NextCount(5);
                }
                break;

            case 5:
                if (runTime >= 4)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 80, 5);
                    NextCount(6);
                }
                break;

            case 6:
                if(runTime >= 5)
                {
                    DOTween.To(() => lightObj.intensity, x => lightObj.intensity = x, 0, 4);
                    NextCount(7);
                }
                break;

            case 7:
                if (runTime >= 5)
                {
                    upBlock.transform.DOMoveY(5, 3);
                    downBlock.transform.DOMoveY(-5, 3);
                    NextCount(8);
                }
                break;

            case 8:
                if (runTime >= 2)
                {
                    animCamera.transform.DOMove(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -13.5f), 3);
                    NextCount(9);
                }
                break;

            case 9:
                if (runTime >= 2)
                {
                    player.GetComponent<SkillSetting>().AddSkill("DoubleJump");
                    player.GetComponent<PlayerInput>().GainControl();
                    animCamera.SetActive(false);
                    mainCamera.SetActive(true);
                    endEvent = true;
                    NextCount(10);
                }
                break;

            case 10:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onEvent = true;
        //SaveBool("Level2_Event", false);
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

    private void NextCount(int num)
    {
        animCount = num;
        runTime = 0;
    }
}
