using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

//samyam's YT tutorial is used for all code https://www.youtube.com/watch?v=ERAN5KBy2Gs&t=228s

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    // delegate allows another script to handle the functionality
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private TouchControls touchControls;

    private void Awake() {
        touchControls = new TouchControls();
        EnhancedTouchSupport.Enable();
    }

    private void OnEnable(){
        touchControls.Enable();
        //Skriv her om 
        TouchSimulation.Enable();

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        //UnityEngine.InputSystem.EnhancedTouch.Touch.get_activeTouches()

    }

    private void OnDisbale(){
        touchControls.Disable();
        TouchSimulation.Disable();

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;

    }

    private void Start(){
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);

    }

    private void StartTouch(InputAction.CallbackContext context){
        Debug.Log("Touch started" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());    
        if (OnStartTouch != null) OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(),(float)context.startTime);
    }
    private void EndTouch(InputAction.CallbackContext context){
        Debug.Log("Touch ended");   
        if (OnEndTouch != null) OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(),(float)context.time);

    }

    private void FingerDown(Finger finger){
        if (OnStartTouch != null) OnStartTouch(finger.screenPosition, Time.time);

    }

    private void Update(){
        Debug.Log(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches);
        foreach(UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches){
            Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Began); 
            //Began is one of the 6 options meaning "A touch as just begun, i.e. a finger has touched the screen.. 
            //Only the first touch input in any given touch will have this phase.
        }

    }


}
