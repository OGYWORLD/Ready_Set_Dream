using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 오가을
#endregion

/// <summary>
/// 레이저 활성화 후 이동 시간 이후 비활성화 하는 스크립트 입니다.
/// </summary>
public class CLazerSetActive : MonoBehaviour
{
    private float movingTime = 4f; // 레이저가 움직이는 시간

    private void OnEnable()
    {
        StartCoroutine(LazerSetHide());
    }

    IEnumerator LazerSetHide()
    {
        yield return new WaitForSeconds(movingTime);

        gameObject.SetActive(false);
    }
}
