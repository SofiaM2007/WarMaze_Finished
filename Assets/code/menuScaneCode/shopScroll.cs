using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class shopScroll : MonoBehaviour
{
    public Button[] buttons;  // Массив кнопок
    public ScrollRect scrollRect; // Ссылка на ScrollRect
    public RectTransform content; // Контейнер для кнопок
    public float buttonWidth = 250f;  // Ширина кнопки (с учётом отступов)

    public int currentIndex = 6; // Индекс текущей выбранной кнопки
    private float targetPositionX = 0f; // Целевая позиция для прокрутки
    public float smoothSpeed = 10f;


    void Start()
    {
        currentIndex = 6; //всегда начинает с дефолтного лабиринта (без фильтров)
        UpdateScrollPosition();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelection(-1);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelection(1);
        }

    }

    public void MoveSelection(int direction)
    {

        if (currentIndex + direction >= buttons.Length) currentIndex = 0;
        else if (currentIndex + direction < 0) currentIndex = buttons.Length - 1;
        else currentIndex = currentIndex + direction;

        //Debug.Log(currentIndex);
        UpdateScrollPosition();
    }

    public void UpdateScrollPosition()
    {
        float targetPositionX = (currentIndex * buttonWidth) + currentIndex * 548f / 2; //(currentIndex * buttonWidth)+434f/2+(currentIndex-1)*434;
        content.anchoredPosition = new Vector2(-targetPositionX, content.anchoredPosition.y);

    }
}