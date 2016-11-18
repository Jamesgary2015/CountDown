using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Countdown : MonoBehaviour {

    public int Day=10; //设置倒计时天数
    private int fixedTime; //时间转换成秒
    private string _time; //时间显示在UI上
    private string _startTime; //第一次运行的时间
    private string _currentTime;//记录每次程序退出时的时间
	// Use this for initialization
	void Start ()
    {
        //第一次运行程序时将当前时间存储
        if (PlayerPrefs.GetString("startTime") == "")
        {
            PlayerPrefs.SetString("startTime", DateTime.Now.ToString());
        }
        _startTime = PlayerPrefs.GetString("startTime");//每次程序运行时，获取最初的时间
        //Debug.Log(_startTime);
        
        _currentTime = DateTime.Now.ToString();
        //Debug.Log(_currentTime);
        if (_startTime == "")//如果_startTime为空，则表示程序为第一次运行，从设置的天数开始倒计时
        {
            fixedTime = Day * 86400; //将天数转换成秒
        }
        else //否则，总的时间减去上次程序退出时间和初次运行时间的时间差之后开始计时
        {
            DateTime beginTime = DateTime.Parse(_startTime);
            DateTime endTime = DateTime.Parse(_currentTime);
            TimeSpan spanTime = endTime - beginTime;
            //Debug.Log(spanTime.TotalSeconds);  
            fixedTime = Day * 86400 -(int)spanTime.TotalSeconds;

        }

        InvokeRepeating("Timer", 1f, 1f);//重复调用Timer，意思为每隔1秒执行1次Timer（）方法
    }
	
	// Update is called once per frame
	void Update ()
    {
        //将秒数转换成时间，显示在UI上
        _time = (fixedTime / (60 * 60 * 24)).ToString() + "天" + ((fixedTime / 60 - fixedTime / (60 * 60 * 24) * 24 * 60) / 60).ToString() + "小时" + ((fixedTime / 60) % 60).ToString() + "分" + (fixedTime % 60).ToString() + "秒";
        this.GetComponent<Text>().text = _time;
        //如果时间为0，意味着倒计时结束，则停止调用Timer（）方法
        if (fixedTime <= 0)
        {
            fixedTime = 0;
            CancelInvoke("Timer");
            this.GetComponent<Text>().color = Color.red;
        }
    }

    void Timer()
    {
        if (fixedTime > 0)
        {
            fixedTime--;
        }
    }
   
}
