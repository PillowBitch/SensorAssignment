using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine.InputSystem;

public class DataWrite : MonoBehaviour
{
    public static DataWrite instance;
    public int testData = 0;
    public TMP_Text pathtext;
    public bool isActive = false;

    public TMP_Text accelXnum;
    public TMP_Text accelZnum;

    Vector3 dir = Vector3.zero;

    public string omegaContent;

    private List<KeyFrame> keyFrames = new List<KeyFrame>();

    float timer;
    public float timerSeconds = 10;
    public TMP_Text timerText;

    int timesRun = 0;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        if(Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }

    }

    // Update is called once per frame
    void Update()
    {
        dir.x = -Accelerometer.current.acceleration.ReadValue().y;
        dir.z = Accelerometer.current.acceleration.ReadValue().x;

        accelXnum.text = dir.x.ToString();
        accelZnum.text = dir.z.ToString();

        if (isActive)
            timer -= Time.deltaTime;

        if (timer <= 0 && isActive)
        {
            isActive = false;
            timer = 0;
            Write(ToCSV());
            timesRun++;
        }

        timerText.text = "Time left: " + timer.ToString();

    }

    private void FixedUpdate()
    {
        if(isActive)
            keyFrames.Add(new KeyFrame(dir.x, dir.z));
    }

    public void SetActive()
    {
        if(!isActive)
        {
            keyFrames = new List<KeyFrame>();
            isActive = true;
            timer = timerSeconds;
        }   
    }

    public void Write(string content)
    {
        var filePath = Path.Combine(Application.persistentDataPath, "export_" + timesRun.ToString() + ".csv");
        using (var writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine(content);
            //writer.WriteLine(dir.x + "," + dir.y);
        }
        pathtext.text = "File Path: " + filePath;
    }

    public string ToCSV()
    {
        var sb = new StringBuilder("AccelX,AccelZ");
        foreach (var frame in keyFrames)
        {
            sb.Append('\n').Append(frame.accelX.ToString()).Append(',').Append(frame.accelZ.ToString());
        }

        return sb.ToString();
    }

    public float GetProgress()
    {
        float t = timer / timerSeconds;
        return t;
    }
}