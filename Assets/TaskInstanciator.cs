using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskInstanciator : MonoBehaviour
{
    public List<GameObject> taskList;
    [SerializeField] GameObject taskInstance;

    float startY = 297;
    float offsetY = 65;

    FileSaver fileSave;

    private void Awake()
    {
        fileSave =GetComponent<FileSaver>();
        taskList = new List<GameObject>();
    }

    public void ActivateTask(GameObject target)
    {
        target.transform.Find("Active").gameObject.SetActive(true);
        target.transform.Find("Add").gameObject.SetActive(false);
        AddTask();
    }
    public void AddTask()
    {
        GameObject newTask = Instantiate(taskInstance, taskInstance.transform.parent);
        newTask.SetActive(true);
        Vector3 pos = newTask.transform.localPosition;
        newTask.transform.localPosition = new Vector3(pos.x, startY - offsetY * taskList.Count, pos.z);
        taskList.Add(newTask);
        fileSave.SaveTask();
    }

    public void AddTask(string taskName, string taskHMS)
    {
        GameObject newTask = Instantiate(taskInstance, taskInstance.transform.parent);
        newTask.SetActive(true);
        Vector3 pos = newTask.transform.localPosition;
        newTask.transform.localPosition = new Vector3(pos.x, startY - offsetY * taskList.Count, pos.z);
        taskList.Add(newTask);
        newTask.transform.Find("Active/TaskName").GetComponent<TextMeshProUGUI>().text = taskName;
        newTask.transform.Find("Active/Timer").GetComponent<TextMeshProUGUI>().text = taskHMS;
        newTask.transform.Find("Active").gameObject.SetActive(true);
        newTask.transform.Find("Add").gameObject.SetActive(false);
        fileSave.SaveTask();
    }

    public void RemoveTask(GameObject target)
    {
        taskList.Remove(target);
        Destroy(target);
        AlignTask();
        fileSave.SaveTask();
    }

    void AlignTask()
    {
        for(int i = 0; i < taskList.Count; i++)
        {
            Vector3 pos = taskList[i].transform.localPosition;
            taskList[i].transform.localPosition = new Vector3(pos.x, startY - offsetY * i, pos.z);
        }
    }
}
