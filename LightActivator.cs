using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Lights
{ 
    Game,
    Chapel,
    Home,
    Security,
    NOLIGHT
}

public class LightActivator : MonoBehaviour
{
    #region Variables
    [Header("Game")]
    public NavMeshSourceTag navmeshGame;
    private GameObject[] lightGroup1;
    private List<Light> lightGroup1Lights = new List<Light>();  
    public bool gameLightSwitch;

    [Header("Chapel")]
    public NavMeshSourceTag navmeshBarChapel;
    private GameObject[] lightGroup2;
    private List<Light> lightGroup2Lights = new List<Light>();
    public bool chapelLightSwitch;

    [Header("Home")]
    public NavMeshSourceTag navmeshHome;
    private GameObject[] lightGroup3;
    private List<Light> lightGroup3Lights = new List<Light>();
    public bool homeLightSwitch;

    [Header("Security")]
    public NavMeshSourceTag navmeshSecurity;
    private GameObject[] lightGroup4;
    private List<Light> lightGroup4Lights = new List<Light>();
    public bool securityLightSwitch;

    [Header("LightState")]
    public Lights currentLightsState;
    public Lights previousLightsState;


    [HideInInspector]
    public static LightActivator Instance;
    
    public bool fuseRemoved;
#endregion
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }   //Singleton instantiation

    void Start()
    {
        navmeshGame.enabled = true;
        navmeshBarChapel.enabled = true;
        navmeshHome.enabled = true;
        navmeshSecurity.enabled = true;

        lightGroup1 = GameObject.FindGameObjectsWithTag("DeterringLight1");        
        lightGroup2 = GameObject.FindGameObjectsWithTag("DeterringLight2");
        lightGroup3 = GameObject.FindGameObjectsWithTag("DeterringLight3");
        lightGroup4 = GameObject.FindGameObjectsWithTag("DeterringLight4");
                
        
        for (int i = 0; i < lightGroup1.Length; i++)
        {
            lightGroup1Lights.Add (lightGroup1[i].GetComponent<Light>());
        }
        for (int i = 0; i < lightGroup2.Length; i++)
        {
            lightGroup2Lights.Add(lightGroup2[i].GetComponent<Light>());
        }
        for (int i = 0; i < lightGroup3.Length; i++)
        {
            lightGroup3Lights.Add(lightGroup3[i].GetComponent<Light>());
        }
        for (int i = 0; i < lightGroup4.Length; i++)
        {
            lightGroup4Lights.Add(lightGroup4[i].GetComponent<Light>());
        }        
    }           //Load every gameobject into variable arrays

   
    void LateUpdate()
    {
        if (previousLightsState != currentLightsState)
            LightSwitch();

        else if (previousLightsState == currentLightsState && fuseRemoved)
            LightSwitch();
    }


   /* private void TurnOffLights()
    {

        if (currentLightsState == Lights.NOLIGHT && intensity >= 0.10f)
        {
           .intensity -= 0.05f;
        }

    }*/

    public void LightSwitch()       //chosing a RoomName will turn on the light in that room (that is tagged with "Deterring light")
    {
        switch (currentLightsState)
        {            
            case Lights.Game: //sets the intensity of each light to max
                {                  
                    if (previousLightsState != Lights.Game)
                        previousLightsState = Lights.Game;

                    gameLightSwitch = !gameLightSwitch;                    
                    navmeshGame.enabled = !gameLightSwitch;
                    if (gameLightSwitch)
                    {
                        for (int i = 0; i < lightGroup1.Length; i++)                    
                            lightGroup1Lights[i].intensity = 15f;                      
                    }
                    else
                    {
                        for (int i = 0; i < lightGroup1.Length; i++)                  
                            lightGroup1Lights[i].intensity = 0f;
                    
                        currentLightsState = Lights.NOLIGHT;
                    }
                break;
                }
            case Lights.Chapel: //sets the intensity of each light to max
                {
                    if (previousLightsState != Lights.Chapel)
                        previousLightsState = Lights.Chapel;

                    chapelLightSwitch = !chapelLightSwitch;
                    navmeshBarChapel.enabled = !chapelLightSwitch;
                    if (chapelLightSwitch)
                    {
                        for (int i = 0; i < lightGroup2.Length; i++)
                            lightGroup2Lights[i].intensity = 15f;
                    }
                    else
                    {
                        for (int i = 0; i < lightGroup2.Length; i++)
                            lightGroup2Lights[i].intensity = 0f;

                        currentLightsState = Lights.NOLIGHT;
                    }

                    break;
                   
                }
            case Lights.Home: //sets the intensity of each light to max 
                {
                    //Call the state swap once only
                    if (previousLightsState != Lights.Home)
                        previousLightsState = Lights.Home;

                    //turn the light switch from on to off depending on when was it called
                    //turn the enemy pathing on or off depending on when was it called
                    //lights on = enemy pathing off , lights off = enemy pathing on.
                    homeLightSwitch = !homeLightSwitch;
                    navmeshHome.enabled = !homeLightSwitch;

                    //if switch is turned on keep the lights on otherwise turn it down.
                    if (homeLightSwitch)
                    {
                        for (int i = 0; i < lightGroup3.Length; i++)   
                            lightGroup3Lights[i].intensity = 15f;
                        
                    }
                    else
                    {
                        for (int i = 0; i < lightGroup3.Length; i++)
                            lightGroup3Lights[i].intensity = 0f;

                        currentLightsState = Lights.NOLIGHT;
                    }
                    break;
                }
            case Lights.Security: //sets the intensity of each light to max, each light is slowly draining.
                {
                    if (previousLightsState != Lights.Security)
                        previousLightsState = Lights.Security;

                    securityLightSwitch = !securityLightSwitch;
                    navmeshSecurity.enabled = !securityLightSwitch; //this nwo
                    if (securityLightSwitch)
                    {
                        for (int i = 0; i < lightGroup4.Length; i++)                       
                            lightGroup4Lights[i].intensity = 15f;
                    }
                    else
                    {
                        for (int i = 0; i < lightGroup4.Length; i++)
                            lightGroup4Lights[i].intensity = 0f;

                        currentLightsState = Lights.NOLIGHT;
                    }
                    break;
                }
            case Lights.NOLIGHT:    //Turn off all lights enable enemy pathing through room.
                {
                    if (previousLightsState != Lights.NOLIGHT)
                        previousLightsState = Lights.NOLIGHT;

                    fuseRemoved = false;
                    break;
                }
        }
    }
}

