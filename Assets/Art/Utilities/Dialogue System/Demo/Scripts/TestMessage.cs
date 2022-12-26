using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class TestMessage : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dialogTexts.Add(new DialogData("/size:up/Hi Player, /size:init/my name is Hannah!.", "Li"));

        dialogTexts.Add(new DialogData("Lord Arlecchino sent you a mission. She required you to FIND a long-black-hair and fair-skin girl /emote:Sad/like this. :) /click//emote:Happy/", "Li",  () => Show_Example(2)));
    
        dialogTexts.Add(new DialogData("Either you or she DIES, the mission is FAILED.", "Li"));

        dialogTexts.Add(new DialogData("So please KEEP both you and her SAFE.", "Li"));

        dialogTexts.Add(new DialogData("You will WIN if you LEAD HER TO THIS HOUSE. /emote:House/  /click//emote:Normal/", "Li"));

        dialogTexts.Add(new DialogData("You should KILL ALL ZOMBIES FIRST.", "Li"));

        dialogTexts.Add(new DialogData("Hints: She is somewhere in this forest!", "Li"));

        dialogTexts.Add(new DialogData("That's it! Good luck to you.", "Li"));

        dialogTexts.Add(new DialogData("Now Let's back to your mission by CLICKING THE RIGHT MOUSE BUTTON /emote:Command/, and waiting for a few seconds :)", "Li", ()=> Show_Example(2)));

        DialogManager.Show(dialogTexts);
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void Show_Example(int index)
    {
        Example[index].SetActive(true);
    }
}
