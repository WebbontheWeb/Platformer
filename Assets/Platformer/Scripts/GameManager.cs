using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public TextMeshPro timeText;
    public TextMeshPro coinText;
    public float time = 300f;
    private int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.anyKeyDown){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)){
                if(hit.transform.name == "Question(Clone)"){
                    coins++;
                    coinText.text = $"{coins}";
                } else if (hit.transform.name == "Brick(Clone)"){
                    Destroy(hit.transform.gameObject);
                }
                Debug.Log($"hit: {hit.transform.name}");
            }
        }





        if(time > 0){
            time -= Time.deltaTime;
            timeText.text = $"{Mathf.FloorToInt(time)}";
        }

    }
}
