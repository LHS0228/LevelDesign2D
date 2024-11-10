using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetting : MonoBehaviour
{
    private CharacterSpecialKey playerSpeicalKey;
    public bool isStart = false;

    private void Awake()
    {
        playerSpeicalKey = GetComponent<CharacterSpecialKey>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isStart)
        {
            ResetSkill();
        }
        else
        {
            LoadSkill();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSkill(string skill)
    {
        switch(skill)
        {
            case "DoubleJump":
                SaveBool("DoubleJump", true);
                playerSpeicalKey.onDoubleJump = true;
                break;

            case "Dash":
                SaveBool("Dash", true);
                playerSpeicalKey.onDash = true;
                break;

            case "Glide":
                SaveBool("Glide", true);
                playerSpeicalKey.onGlider = true;
                break;
        }
    }

    public bool ChooseLoadSkill(string skill)
    {
        switch(skill)
        {
            case "DoubleJump":
                return LoadBool("DoubleJump", false);
            case "Dash":
                return LoadBool("Dash", false);
            case "Glide":
                return LoadBool("Glide", false);
            default:
                Debug.Log("로드 조건 잘못 입력 버그");
                return false;
        }
    }

    public void LoadSkill()
    {
        playerSpeicalKey.onDoubleJump = LoadBool("DoubleJump", false);
        playerSpeicalKey.onDash = LoadBool("Dash", false);
        playerSpeicalKey.onGlider = LoadBool("Glide", false);
    }

    public void ResetSkill()
    {
        SaveBool("DoubleJump", false);
        SaveBool("Dash", false);
        SaveBool("Glide", false);

        playerSpeicalKey.onDoubleJump = false;
        playerSpeicalKey.onDash = false;
        playerSpeicalKey.onGlider = false;
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
