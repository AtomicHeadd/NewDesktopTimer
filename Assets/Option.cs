using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject option;

    GameObject showingOptionTarget;
    TMP_InputField activeInput;

    FileSaver fileSave;

    private void Awake()
    {
        fileSave = GetComponent<FileSaver>();
    }
    public void ShowOption(GameObject target)
    {
        option.SetActive(true);
        option.transform.position = target.transform.Find("Active/Option").position;
        showingOptionTarget = target;
    }

    public void CloseOption()
    {
        option.SetActive(false);
    }

    public void ShowRename()
    {
        activeInput = showingOptionTarget.transform.Find("Active/Rename").GetComponent<TMP_InputField>();
        activeInput.text = showingOptionTarget.transform.Find("Active/TaskName").GetComponent<TextMeshProUGUI>().text;
        activeInput.gameObject.SetActive(true);
        option.SetActive(false);
    }

    public void SetRename()
    {
        showingOptionTarget.transform.Find("Active/TaskName").GetComponent<TextMeshProUGUI>().text = activeInput.text;
        activeInput.gameObject.SetActive(false);
        fileSave.SaveTask();
    }

    public void OptionRemove()
    {
        GetComponent<TaskInstanciator>().RemoveTask(showingOptionTarget);
        CloseOption();
        fileSave.SaveTask();
    }

    public void OptionReset()
    {
        showingOptionTarget.transform.Find("Active/Timer").GetComponent<TextMeshProUGUI>().text = "00:00:00.00";
        CloseOption();
        fileSave.SaveTask();
    }

    private void Update()
    {
        if (activeInput && Input.GetKeyDown(KeyCode.Return))
        {
            SetRename();
        }
    }
}
