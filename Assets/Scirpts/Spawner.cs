using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    // Hvordan ved den hvilken en den skal spawne?
    
    
    /// <summary>
    /// Liste som gør man kan tilføje i inspector, da den er en public access modifier.
    /// public int gør det samme så man kan redigere i inspector.
    /// </summary>
   public GameObject[] objectsToSpawn;
     //if (touchControls.Touch.Hold.IsPressed())
    public int popupCounter;


    // bool gør at den enten kan være true eller false. Ved Reset starter den altid som false.

    bool isEnabled = false;
    TouchControls touchControls;


    // private void start gør brug af Reset. Fra første frame benytter vi Reset til at nulstille funktionen af gameobjekterne i popupCounter.

    private void Start() {
       touchControls = new TouchControls();
        touchControls.Enable();
       Reset();
    }



    void Update()
    // void Update bruges da den kontant skal se om den er true eller false.
    // Derefter laver vi en if-else statement, hvor en handling skal ske hver gang man trykker "Jump" eller "Space"
    {

        if (touchControls.Touch.Hold.IsPressed())
        {

            // if(isEnabled == false) tjekker om den er false. Hvis den er false spawner den objektet og gør den true.
            // Hvis den ikke er false går den ned i else og laver et Reset.
            if (isEnabled == false)
            {
                SpawnObject();
                isEnabled=true;

            }
            else
            {
                Reset();
                isEnabled=false;
            }

        }
    }

    public void Reset()
    {
        // Hvert objekt skal kunne gøres aktivt ved at bruge "Jump","Space".
        foreach (GameObject obj in objectsToSpawn)
        {
            obj.SetActive(false);
        }
    }
    /// <summary>
    // Den ved ik hvilke en som den skal spawne
    /// Dette er koden vi rent faktisk bruger når vi skal spawne objekterne.
    /// Når vi trykker "Space" kalder vi på metoden objectsToSpawn. Den gør at vores objekter i vores counter bliver aktive.
    /// Da den er public kan vi vælge hvilken popupcounter der skal være aktiv i vores inspektor.
    /// Vores første if-statement siger at hvis vi gør brug af element 0 skal den benytte lyden fra soundmanageren og metoden deri som hedder PlayAdgangSound.
    /// Dette gælder også for de to andre else-if statements
    /// </summary>

    public void SpawnObject()
    {
        objectsToSpawn[popupCounter].SetActive(true);

        if(popupCounter == 0)
        {
         SoundManager.Instance.PlayAdgangSound();

        }
        else if(popupCounter == 1)
        {
         SoundManager.Instance.PlayIngenSound();

        }
        else if(popupCounter == 2)
        {
         SoundManager.Instance.PlayBegrænsetSound();

        }
        //Instantiate(objectsToSpawn, transform.position, Quaternion.identity);
    }
}
