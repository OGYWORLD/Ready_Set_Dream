using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 오가을
#endregion

/// <summary>
/// 레이저를 생성하는 스크립트입니다.
/// 숏노트와 롱노트를 구분하여 pool을 구현했습니다.
/// 생성 위치에서 플레이어의 위치(카메라의 위치)까지 2초 걸리므로
/// notes 리스트의 시간보다 GameManager.instance.noteMoveSpeed초 먼저 활성화 한 후 카메라 쪽으로 이동합니다.
/// </summary>
public class CShowLazer : MonoBehaviour
{
    public enum NoteCategory
    {
        ShortNote,
        LongNote,
        DoubleNote
    }

    private float speed = 10f; // 레이저 이동 속도

    // 오브젝트 풀링
    public GameObject shortNotePrefab; // 숏 노트 프리팹
    public GameObject longNotePrefab; // 롱 노트 프리팹
    public GameObject doubleNotePrefab; // 더블 노트 프리팹

    private int shortNoteNum = 9; // 숏 노트 풀 크기
    private int longNoteNum = 3; // 롱 노트 풀 크기
    private int doubleNoteNum = 8; // 더블 노트 풀 크기

    private List<GameObject> shortPool = new List<GameObject>(); // 숏 노트 풀
    private List<GameObject> longPool = new List<GameObject>(); // 롱 노트 풀
    private List<GameObject> doublePool = new List<GameObject>(); // 더블 노트 풀

    private int shortIdx; // 숏 노트 풀 인덱스
    private int longIdx; // 롱 노트 풀 인덱스
    private int doubleIdx; // 더블 노트 풀 인덱스

    private int noteIdx; // 노트 리스트 인덱스

    private CLazerSetActive lazerSet; // 노트 레이저 세팅 인덱스

    /// <summary>
    /// 인스펙터 창에서 리스폰 베리어에서의 리스폰 오브젝트의 위치를 받습니다.
    /// </summary>
    public List<Transform> shortNoteTrans = new List<Transform>(); // 숏 노트 생성 위치
    public Transform longNoteTrans; // 롱 노트 생성 위치

    private void Start()
    {
        // 풀 생성
        MakePool();

        StageManager.instance.mainMusic.Play();
    }

    private void Update()
    {
        SetDistance();
        RespawnLazer();
    }

    void MakePool()
    {
        // 숏 노트 오브젝트 풀 초기화
        for(int i = 0; i < shortNoteNum; i++)
        {
            GameObject obj = Instantiate(shortNotePrefab, shortNoteTrans[i % shortNoteTrans.Count].position, Quaternion.identity);
            obj.SetActive(false);
            shortPool.Add(obj);
        }

        // 롱 노트 오브젝트 풀 초기화
        for (int i = 0; i < longNoteNum; i++)
        {
            GameObject obj = Instantiate(longNotePrefab, longNoteTrans.position, Quaternion.identity);
            obj.SetActive(false);
            longPool.Add(obj);
        }

        // 더블 노트 오브젝트 풀 초기화
        for (int i = 0; i < doubleNoteNum; i++)
        {
            GameObject obj = Instantiate(doubleNotePrefab, longNoteTrans.position, Quaternion.identity);
            obj.SetActive(false);
            doublePool.Add(obj);
        }
    }

    void SetDistance()
    {
        StageManager.instance.noteSize = StageManager.instance.notes[noteIdx].endTime - StageManager.instance.notes[noteIdx].srtTime;
        StageManager.instance.betweenDis = 58f - (StageManager.instance.notes[noteIdx].endTime - StageManager.instance.notes[noteIdx].srtTime);
    }

    void RespawnLazer()
    {
        // 노트 시간 보다 StageManager.instance.noteMoveSpeed초 전에 레이저를 생성한다.
        if (StageManager.instance.mainMusic.time >= StageManager.instance.notes[noteIdx].srtTime - StageManager.instance.noteMoveSpeed)
        {
            switch (StageManager.instance.notes[noteIdx].noteCategory)
            {
                case (int)NoteCategory.ShortNote:
                    lazerSet = shortPool[shortIdx].GetComponent<CLazerSetActive>();
                    lazerSet.noteIdx = noteIdx;
                    lazerSet.isLong = false;

                    shortPool[shortIdx].transform.position = shortNoteTrans[shortIdx % shortNoteTrans.Count].position;
                    shortPool[shortIdx].SetActive(true);
                    shortIdx++;

                    if (shortIdx == shortPool.Count)
                    {
                        shortIdx = 0;
                    }

                    noteIdx++;
                    break;

                case (int)NoteCategory.LongNote:
                    lazerSet = longPool[longIdx].GetComponent<CLazerSetActive>();
                    lazerSet.noteIdx = noteIdx;
                    lazerSet.isLong = true;

                    longPool[longIdx].transform.localScale = new Vector3(
                        StageManager.instance.noteSize * (StageManager.instance.betweenDis / StageManager.instance.noteMoveSpeed),
                        longPool[longIdx].transform.localScale.y,
                        longPool[longIdx].transform.localScale.z
                        );

                    longPool[longIdx].transform.position = longNoteTrans.position;
                    longPool[longIdx].SetActive(true);
                    longIdx++;

                    if (longIdx == longPool.Count)
                    {
                        longIdx = 0;
                    }

                    noteIdx++;
                    break;

                case (int)NoteCategory.DoubleNote:
                    lazerSet = doublePool[doubleIdx].GetComponent<CLazerSetActive>();
                    lazerSet.noteIdx = noteIdx;
                    lazerSet.isLong = false;

                    doublePool[doubleIdx].transform.position = longNoteTrans.position;
                    doublePool[doubleIdx].SetActive(true);
                    doubleIdx++;

                    if (doubleIdx == doublePool.Count)
                    {
                        doubleIdx = 0;
                    }

                    noteIdx++;
                    break;
            }
        }
    }
}
