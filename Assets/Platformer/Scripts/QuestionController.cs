using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{   
    public Texture question1;
    public Texture question2;
    public Texture question3;
    public Texture question4;
    public Texture question5;
    private int frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        frame++;
        //changing texture
        switch(frame){
            case 1:
                GetComponent<Renderer>().material.SetTexture("_BaseMap", question1);
                break;
            case 12:
                GetComponent<Renderer>().material.SetTexture("_BaseMap", question2);
                break;
            case 23:
                GetComponent<Renderer>().material.SetTexture("_BaseMap", question3);
                break;
            case 34:
                GetComponent<Renderer>().material.SetTexture("_BaseMap", question4);
                break;
            case 45:
                GetComponent<Renderer>().material.SetTexture("_BaseMap", question5);
                frame = -7;
                break;
        }

    }
}
