using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBlock : MonoBehaviour
{
    Vector3 drawPosition;
    private SpriteRenderer drawSpriteRenderer;
    Color stDrawColor;
    Color drawColor;
    private void Awake()
    {
        drawSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        stDrawColor = Color.white;
        drawColor.a = 1f;
    }
    void Update()
    {
        if (Spawner.ifDraw)
        {
            FindObjectOfType<Spawner>().DrawB();
            }
        
            if (Spawner.changeColor&& drawSpriteRenderer.color!=stDrawColor)
            {
                    drawSpriteRenderer.color = stDrawColor;
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Spawner.ifDraw)
        {
            Spawner.changeColor = false;
            drawSpriteRenderer.color = drawColor;
            Spawner.notNull = true;
                    int objectNumber = Convert.ToInt32(gameObject.name);
            Spawner.Blocknums[objectNumber] = 1;
        }
        }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Spawner.ifDraw)
        {
            Spawner.changeColor = false;
            drawSpriteRenderer.color = drawColor;
                Spawner.notNull = true;
                int objectNumber = Convert.ToInt32(gameObject.name);
                Spawner.Blocknums[objectNumber] = 1;
        }
    }
    }
    
