using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class FileSaver : MonoBehaviour
{
    string saveFilePath = "taskTimer.csv";

    TaskInstanciator taskInstanciator;

    private void Start()
    {
        taskInstanciator = GetComponent<TaskInstanciator>();
        saveFilePath = Application.dataPath + "/" + saveFilePath;
        print(saveFilePath);
        LoadTask();

    }

    public void SaveTask()
    {
        List<GameObject> taskList = taskInstanciator.taskList;
        List<string> taskNames = taskList.ConvertAll(t => t.transform.Find("Active/TaskName").GetComponent<TextMeshProUGUI>().text);
        List<string> HMS = taskList.ConvertAll(t => t.transform.Find("Active/Timer").GetComponent<TextMeshProUGUI>().text);
        string saveText = "";
        for(int i = 0; i < HMS.Count; i++)
        {
            saveText += $"{taskNames[i]},{HMS[i]}\n";
        }
        File.WriteAllText(saveFilePath, saveText);
    }
    public void LoadTask()
    {
        TaskInstanciator taskInstanciator = GetComponent<TaskInstanciator>();
        //何もなかった場合ロードしない
        if (!File.Exists(saveFilePath))
        {
            taskInstanciator.AddTask("New Task", "00:00:00");
            return;
        }

        string data = File.ReadAllText(saveFilePath);
        string[] taskData = data.Split("\n");
        for(int i = 0; i < taskData.Length; i++)
        {
            print(taskData[i]);
            string[] taskData_i = taskData[i].Split(",");
            if (taskData_i.Length != 2) continue;
            taskInstanciator.AddTask(taskData_i[0], taskData_i[1]);
        }
        taskInstanciator.AddTask();
    }


    /*
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * if (!File.Exists(totalTimeFilePath))
        {
            //保存されているファイルがない場合、読み込まない
            AddNewTask("New Task");
            return;
        }

        string data = File.ReadAllText(totalTimeFilePath);
        string[] lines = data.Split("\n");
        foreach(string i in lines)
        {
            if (i == "") continue;
            string[] columns = i.Split(",");
            taskTotalTimeDigits[columns[0]] = columns[1];
            savedTaskNames.Add(columns[0]);
        }
        foreach (KeyValuePair<string, string> kvp in taskTotalTimeDigits) print($"{kvp.Key}: {kvp.Value}");








     File.WriteAllText(totalTimeFilePath, saveText);
    */
}
