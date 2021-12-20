using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 单例模式
    public static AudioManager Instance;
    // 音效播放器组件
    private AudioSource audioSource;
    // 音效片段数组 0死亡2， 1跳 ，2踩敌人 ，3吃金币
    public AudioClip[] audioClips;

    // 游戏一开始执行
    void Awake()
    {
        
        // 确定场景中是否存在音效管理器
        // 如果没有
        if (Instance ==null)
        {
            // 标志自身是
            Instance = this;
            // 让Unity不在要场景切换的时候销毁自身
            DontDestroyOnLoad(gameObject);

            // 查找音效播放器组件
            audioSource = GetComponent<AudioSource>();
        }
        // 如果没有
        else
        {
            // 说明自己是多余的，销毁自己
            Destroy(gameObject);
        }

    }

    // 检查背景音乐
    public void CheckBG()
    {
        // 如果背景音乐没有在播放中
        if (audioSource.isPlaying==false)
        {
            // 播放背景音乐
            audioSource.Play();
        }
    }
    // 播放音效 播放第几个，是不是要关闭背景音乐
    public void Play(int index,bool bgOver = false)
    {
        // 如果需要关闭背景音乐
        if (bgOver)
        {
            // 停止背景音乐
            audioSource.Stop();
        }
        // 播放一次指定的音效
        audioSource.PlayOneShot(audioClips[index]);
    }
}
