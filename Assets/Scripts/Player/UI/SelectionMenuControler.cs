using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SelectionMenuControler : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<string> characters;
    [SerializeField] int index;
    public PlayerInput input;
    private void Awake()
    {
        index = 0;
    }

    public void OnSelecting(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            float value = ctx.ReadValue<float>();
            print(value);
            if (value > 0)
            {
                index = NextIndex();
                text.SetText(characters[index]);
            }
            else
            {
                index = PreviousIndex();
                text.SetText(characters[index]);
            }
        }
    }
    public void OnReady()
    {
            print("ready ");
    }
    public int NextIndex()
    {
        if(index+1 < characters.Count)
        {
            return index + 1;
        }
        return 0;
    }
    public int PreviousIndex()
    {
        if (index - 1 < 0)
        {
            return characters.Count-1;
        }
        return index-1;
    }

}
