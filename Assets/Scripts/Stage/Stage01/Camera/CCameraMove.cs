using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 오가을
#endregion

/// <summary>
/// Stage01에서 카메라의 움직임을 제어하는 스크립트입니다.
/// </summary>
public class CCameraMove : MonoBehaviour
{
    protected float speed; // 초속

    protected bool isIntroEnd; // 인트로 재생용 변수

    protected virtual void Start()
    {
        speed = (StageManager.instance.betweenDis / StageManager.instance.noteRespawnTime);

        StartCoroutine(WaitIntro());
    }

    protected virtual void Update()
    {
        if(isIntroEnd)
        {
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }  
    }

    protected IEnumerator WaitIntro()
    {
        yield return new WaitForSeconds(4f);

        isIntroEnd = true;
    }
}
