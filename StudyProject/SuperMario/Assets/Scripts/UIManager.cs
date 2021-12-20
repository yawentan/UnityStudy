using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 单例模式
    public static UIManager Instance;
    // 金币UI 文本组件
    private Text goldText;
    // 当前金币数量
    private int goldNum;

    // 金币数量的属性，修改时候自动更新UI
    public int GoldNum { get => goldNum;

        set {
            // 修改当前金币数量
            goldNum = value;
            // 更新UI的文本显示
            goldText.text = "X " + goldNum;
        }
    }

    // 游戏一开始就执行
    private void Awake()
    {
        Instance = this;
        // 金币的Text组件查找
        goldText = transform.Find("Gold/Text").GetComponent<Text>();

    }

}
