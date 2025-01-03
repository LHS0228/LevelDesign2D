using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class laser : MonoBehaviour
{// 위에서 아래로 가는 레이저일 때
    public float currentLength;
    public LayerMask colliderLayer;
    private SpriteRenderer spriteRenderer;
    private Damager damager;
    private void Awake()
    {
        damager = GetComponent<Damager>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // 스프라이트 렌더러 초기화
        currentLength = spriteRenderer.size.y;
        UpdateLaserLength();// 레이저 길이 설정

        string currentScene = SceneManager.GetActiveScene().name;

        if(currentScene == "Level_1")
        {
            if (gameObject.CompareTag("Laser"))
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(true);
        }

    }

    void Update()
    {
        UpdateLaserLength();  // 레이저 길이 업데이트
    }

    // 레이저 길이를 업데이트하는 함수
    void UpdateLaserLength()
    {
        Vector3 startPosition = transform.position;

        if (transform.eulerAngles.z == 0f)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector3.down, Mathf.Infinity, colliderLayer);

            if (hit.collider != null)
            {
                currentLength = hit.distance;
            }
            else
            {
                currentLength = spriteRenderer.size.y;
            }
            UpdateLaserScale();
        }
        else if (transform.eulerAngles.z == 90f)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector3.right, Mathf.Infinity, colliderLayer);

            if (hit.collider != null)
            {
                currentLength = hit.distance;
            }
            else
            {
                currentLength = spriteRenderer.size.y;
            }
            UpdateLaserScale();
            damager.size.x = currentLength;
        }
        else if (transform.eulerAngles.z == 180f)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector3.up, Mathf.Infinity, colliderLayer);

            if (hit.collider != null)
            {
                currentLength = hit.distance;
            }
            else
            {
                currentLength = spriteRenderer.size.y;
            }
            UpdateLaserScale();
        }


    }
    void UpdateLaserScale()
    {
        Vector3 scale = transform.localScale;
        scale.y = currentLength * 0.08f; // 레이저와 두칸 거리일때
        transform.localScale = scale;
    }

    public void OnLaser()
    {
        gameObject.SetActive(true);
    }
}
