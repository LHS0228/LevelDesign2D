using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveInEffect : MonoBehaviour
{
    [SerializeField] private GameObject player; //플레이어
    [SerializeField] private bool isCave; //동굴로 들어갔어?
    [SerializeField] private bool isLeft; //왼쪽이 진입이야?
    [SerializeField] private float lightCount; //빛 목표 세기
    [SerializeField] private float maxDarkX = 20; //최대 어두움 x거리

    //시작 위치
    private Vector3 startTrans;
    private float startLight;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCave)
        {
            // 현재 X축 이동량 계산
            float distance = Mathf.Abs(player.transform.position.x - startTrans.x);
            float t = Mathf.Clamp01(distance / maxDarkX); // 0에서 1 사이 값으로 보정

            // 빛의 세기를 점차 줄임 (startLight에서 lightCount로 선형 보간)
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
