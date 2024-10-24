using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveInEffect : MonoBehaviour
{
    [SerializeField] private GameObject player; //�÷��̾�
    [SerializeField] private bool isCave; //������ ����?
    [SerializeField] private bool isLeft; //������ �����̾�?
    [SerializeField] private float lightCount; //�� ��ǥ ����
    [SerializeField] private float maxDarkX = 20; //�ִ� ��ο� x�Ÿ�

    //���� ��ġ
    private Vector3 startTrans;
    private float startLight;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCave)
        {
            // ���� X�� �̵��� ���
            float distance = Mathf.Abs(player.transform.position.x - startTrans.x);
            float t = Mathf.Clamp01(distance / maxDarkX); // 0���� 1 ���� ������ ����

            // ���� ���⸦ ���� ���� (startLight���� lightCount�� ���� ����)
            float currentLightIntensity = Mathf.Lerp(startLight, lightCount, t);
            player.GetComponentInChildren<Light2D>().pointLightOuterRadius = currentLightIntensity;

            if(startTrans.x-1 > player.transform.position.x)
            {
                isCave = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            isCave = true;
            startTrans = player.transform.position;
            startLight = player.GetComponentInChildren<Light2D>().pointLightOuterRadius;
        }
    }
}
