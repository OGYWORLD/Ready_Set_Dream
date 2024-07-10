using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 오가을
#endregion

public class CCameraMove : MonoBehaviour
{
    private float betweenDis = 58f; // 레이저 생성 위치와 카메라 사이의 거리

    private float speed; // 초속

    private void Start()
    {
        speed = (betweenDis / StageManager.instance.noteMoveSpeed);
    }

    private void Update()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);    
    }
}
