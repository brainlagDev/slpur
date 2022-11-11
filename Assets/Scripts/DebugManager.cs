using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DebugManager : MonoBehaviour
{
    private Canvas          Window;
    private TMP_InputField  CommandInput;
    private TMP_Text        TextField;

    private void OnEnable()
    {
        Window = GetComponentInChildren<Canvas>();
        Window.worldCamera = Camera.main;

        CommandInput = Window.GetComponentInChildren<TMP_InputField>();
        TextField = Window.GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Slash))
        {
            Window.gameObject.SetActive(true);
            CommandInput.text = "/";
            CommandInput.ActivateInputField();
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            if (Window.gameObject.activeSelf)
            {
                if (CommandInput.text == "")
                {
                    Window.gameObject.SetActive(false);
                }
                else
                {
                    TextField.text += $"\n{CommandInput}";
                    HandleCommand(CommandInput.text);
                }
            }
        }
    }

    private void HandleCommand(string command)
    {
        switch (command)
        {
            // simple commands
            case "/restart":
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
                    break;
                }
            // parametrized commands
            default:
                {
                    if (command.Contains("/loadscene "))
                    {
                        command.Replace("/loadscene ", "");
                        try
                        {
                            SceneManager.LoadScene(command, LoadSceneMode.Single);
                        }
                        catch
                        {
                            TextField.text += "\nload scene error: incorrect name";
                        }
                    }
                    else if (command == "")
                    {

                    }
                    else
                    {
                        TextField.text += "\nincorrect command";
                    }
                    break;
                }
        }
    }
}
