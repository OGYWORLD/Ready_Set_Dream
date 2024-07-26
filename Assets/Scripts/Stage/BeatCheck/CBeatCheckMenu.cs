using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ������
#endregion

public class CBeatCheckMenu : MonoBehaviour
{
    public GameObject menu;

    public AudioSource sound;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (menu.activeSelf is false)
            {
                sound.Play();
                MenuManager.instance.isOpenMenu = true;
                Time.timeScale = 0f;
            }
            else
            {
                MenuManager.instance.isOpenMenu = false;
                Time.timeScale = 1f;
            }

            menu.SetActive(!menu.activeSelf);
            
        }
    }
}
