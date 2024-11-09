using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{// ������ �Ʒ��� ���� �������� ��
    public float currentLength;
    public LayerMask colliderLayer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // ��������Ʈ ������ �ʱ�ȭ
        currentLength = spriteRenderer.size.y;
        UpdateLaserLength();                              // ������ ���� ����
    }

    void Update()
    {
        UpdateLaserLength();  // ������ ���� ������Ʈ
    }

    // ������ ���̸� ������Ʈ�ϴ� �Լ�
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
        scale.y = currentLength * 0.08f; // �������� ��ĭ �Ÿ��϶�
        transform.localScale = scale;
    }

}
