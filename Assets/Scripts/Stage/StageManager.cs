using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 오가을
#endregion

public class StageManager : MonoBehaviour
{
    /// <summary>
    /// Stage에서 사용되는 StageManager입니다. Stage 내 여러 단계는 Laoding을 고려해서 한 Scene에서 구현되었습니다.
    /// </summary>
    public static StageManager instance { get; private set; } // 싱글톤

    public AudioSource mainMusic; // 스테이지 노래

    public float bpm; // bpm

    public List<Note> notes { get; set; } = new List<Note>(); // 노트 정보 리스트

    public float noteRespawnTime { get; set; } = 4f; // 노트 리스폰 초 (판정 n 초 전 생성)

    public float betweenDis { get; set; } = 58f; // 노트 생성 위치와 카메라 사이의 거리

    public float noteSize { get; set; } = 0f; // 노트 사이즈, 롱노트 사이즈를 위해 사용됨

    public int inputNoteIdx { get; set; } = 0; // 입력할 노트의 인덱스

    public int combo { get; set; } = 0; // 콤보

    public Slider yesNoBar; // yesNoBar 상단 게이지바

    public int score { get; set; } = 0; // 스코어

    public int perfectCnt { get; set; } = 0; // 퍼펙트 개수
    public int goodCnt { get; set; } = 0; // 굿 개수
    public int wrCnt { get; set; } = 0; // 틀린 개수

    public int maxCombo { get; set; } = 0; // 최대 콤보

    public bool isCantMakeNote { get; set; } = false; // 노트 생성 불가능 여부

    public bool isPlayingCutScene { get; set; } = false; // 컷씬 재생 여부

    public bool isGameOver { get; set; } = false; // 게임 오버 여부

    // 현재 스테이지 0: neon city, 1: menu UI, 2: code
    public int curStage { get; set; } = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}

[Serializable]
public class Note
{
    public int noteCategory; // 0: short, 1: long
    public float srtTime;
    public float endTime;

    public Note(int c, float s, float e)
    {
        noteCategory = c;
        srtTime = s;
        endTime = e;
    }
}