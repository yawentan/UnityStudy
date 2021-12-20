using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    // 玩家，面板赋值
    public Transform Player;
    // X轴 允许的最右边坐标
    public float MaxX;
    // X轴 允许的最左边坐标
    public float MinX;

    // 每一帧都执行一次
    void Update()
    {
        // 跟随玩家，但是只跟随玩家的X轴坐标
        Vector3 newPos = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        // 检查新坐标有没有超过 边界
        newPos.x = Mathf.Clamp(newPos.x,MinX, MaxX);
        // 让摄像机等于新坐标
        transform.position = newPos;
    }
}
