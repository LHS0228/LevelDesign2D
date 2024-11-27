using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OrbDoor : MonoBehaviour
{
    [Header("���ǵ�")]
    [SerializeField] private GameObject dashOrb;
    [SerializeField] private GameObject doubleOrb;
    [SerializeField] private GameObject gliderOrb;
    [SerializeField] private bool[] openDoor;

    [SerializeField, Header("���� ��Ż")]
    private GameObject endingPortal;

    [Header("�÷��̾�")]
    [SerializeField]
    private GameObject player;

    [Header("�� �̹���")]
    [SerializeField] private Sprite[] doorSprite;

    private bool hasExecuted = false;
    private bool okOpen = false;

    [Header("�ؽ�Ʈ ����")]
    public UnityEvent onUnlocked;

    void Start()
    {
        openDoor = new bool[3];
    }

    private void Update()
    {
        if(!hasExecuted)
        {
            ExecuteOnce();
            hasExecuted = true;
        }

        ExecuteOnce();

        if (openDoor[0] == true && openDoor[1] == true && openDoor[2] == true && !okOpen)
        {
            okOpen = true;
            onUnlocked.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("��");
        }

        if (collision.gameObject == player && okOpen && Input.GetKeyDown(KeyCode.E))
        {
            endingPortal.SetActive(true);
        }
    }

    void ExecuteOnce()
    {
        // �뽬
        if (player.GetComponent<SkillSetting>().ChooseLoadSkill("Dash") == true)
        {
            dashOrb.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite[0];
            openDoor[0] = true;
        }
        else
        {
            dashOrb.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        // ���� ����
        if (player.GetComponent<SkillSetting>().ChooseLoadSkill("DoubleJump") == true)
        {
            doubleOrb.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite[1];
            openDoor[1] = true;
        }
        else
        {
            doubleOrb.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        // �۶��̴�
        if(player.GetComponent<SkillSetting>().ChooseLoadSkill("Glide") == true)
        {
            gliderOrb.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite[2];
            openDoor[2] = true;
        }
        else
        {
            gliderOrb.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}
