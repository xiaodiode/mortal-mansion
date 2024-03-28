using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    [SerializeField] public bool isLocked;

    // mouse glowing effect
    [SerializeField] public GameObject glowLight;
    [SerializeField] public bool mouseLightOn;

    Vector3 oldMousePosition, mouseScreen, mouseWorld;
    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        mouseLightOn = true;

        mouseScreen = Vector3.zero;
        mouseWorld = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(mouseLightOn){
            enableGlow();
        }
    }
    private void enableGlow(){

        mouseScreen = Input.mousePosition;
        mouseWorld = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, mainCamera.transform.position.y));
        mouseWorld.z = glowLight.transform.position.z;

        glowLight.transform.position = mouseWorld;
    }

    public void lockMouse(bool locked){
        if(locked){
            oldMousePosition = Input.mousePosition;
            Cursor.visible = false;
            isLocked = true;
        }
        else{
            Mouse.current.WarpCursorPosition(oldMousePosition);
            Cursor.visible = true;
            isLocked = false;
        }
    }

    public void hideMouse(bool hide){

        Cursor.visible = hide;
    }

}
