using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour {

    private Transform[] hearts = new Transform[5]; //массив сердец

    private Character character; 

    private void Awake()
    {
        character = FindObjectOfType<Character>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i); //в iый эл-т hearts записывает, что вернёт getchild (получаем сердца)
            
        }
        
    }

    public void Refresh() //чтобы не каждый раз обновлялось, а только когда меняется кол-во жизней
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < character.Lives)
                hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false); //если жизней меньше, чем сердец в ливсбаре, то убираем одно
        }
    }
}
