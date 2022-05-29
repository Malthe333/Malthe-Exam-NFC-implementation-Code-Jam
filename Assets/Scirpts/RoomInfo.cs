using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomInfo : MonoBehaviour
{
    //This string determines the displayed room name
    public string roomNavn;

    //This string determines the displayed room type
    public string roomType;

    //This string determines the displayed date of entering the room  - May scrap this later
    public string datoÅbning;

    // Custom accessType
    public accessType cleareance;
    
    // Custom Text Mesh Pro Text input
    public TMP_Text textNavn;

    // Custom Text Mesh Pro Text input
    public TMP_Text textType;

    // Custom Text Mesh Pro Text input
    public TMP_Text textAccess;

    // Custom Text Mesh Pro Text input
    public TMP_Text textÅbning;

    // Changes colour for the different access types
    public TMP_ColorGradient farve;

    /// <summary>
    /// Three custom string values as enumaroters to assign access types of rooms: Afgang, BegrænsetAdgang, IngenAdgang
    /// </summary>

    public enum accessType 
    {
        Adgang = 0,
        BegrænsetAdgang = 1,
        IngenAdgang = 2
       


    }

    // The definition of data of the rooms happen in start
    public void Start()
    {
        //This string holds the information of the room access data(Which type)
        string accessText = "";

        //These if statements are looking for which access type is chosen for the room, and changes the colour accordingly
        if (cleareance == accessType.Adgang)
        {
            accessText = "Adgang";
            textAccess.color = Color.green;
        }
        if (cleareance == accessType.BegrænsetAdgang)
        {
            accessText = "Begrænset adgang";
            textAccess.color = Color.yellow;

        }
        if (cleareance == accessType.IngenAdgang)
        {
            accessText = "Ingen adgang";
            textAccess.color = new Color(1, 0, 0);

        }

        //Displays the type of access of the room
        textAccess.text = accessText;

        //Displays the name of the room
        textNavn.text = "Lokale: " + roomNavn;

        //Displays the type of room
        textType.text = roomType;

        //Displays the date of opening the room, for now as manual writing
        textÅbning.text = datoÅbning;

        

        
    }

}
