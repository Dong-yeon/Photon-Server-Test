using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance; // 다른 class 에서도 호출가능

    [SerializeField] Menu[] menus; // SerializeField를 사용하면 우리는 public 처럼 사용하지만 외부에서는 사용 불가

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName) // string을 받아서 해당이름을 가진 메뉴를 여는 스크립트
            {
                menus[i].Open(); // 오픈메뉴에 있는 for문이 똑같이 있기때문에 중복을 피하기 위해서 수정.
                // OpenMenu(menus[i]);
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
