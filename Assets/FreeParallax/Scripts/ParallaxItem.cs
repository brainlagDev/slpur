using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxItem : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float RateX;
    [SerializeField, Range(0.0f, 1.0f)] private float RateY;
    [SerializeField] private SpriteRenderer Sprite;
    private SpriteRenderer MinorSprite;
    private float Width;

    private void Start()
    {
        Width = Sprite.bounds.size.x;
        MinorSprite = Instantiate(Sprite, this.transform);
        MinorSprite.name = "Minor" + Sprite.name;
        MinorSprite.transform.position = new Vector2(Sprite.transform.position.x + Width, Sprite.transform.position.y);
    }

    public void Move(Transform target)
    {
        Sprite.transform.position = new Vector2(
            target.position.x * RateX,
            target.position.y * RateY);

        // Defining minor sprite postion
        if (target.position.x > Sprite.transform.position.x)
        {
            MinorSprite.transform.position = new Vector2(
                Sprite.transform.position.x + Width,
                Sprite.transform.position.y);
            Debug.Log($"{this.name} minor sprite goes right");
        }
        else if (target.position.x < Sprite.transform.position.x)
        {
            MinorSprite.transform.position = new Vector2(
                Sprite.transform.position.x - Width,
                Sprite.transform.position.y);
            Debug.Log($"{this.name} minor sprite goes left");
        }

        // Swapping sprites
        if (target.transform.position.x > Sprite.transform.position.x + Width / 2 ||
            target.transform.position.x < Sprite.transform.position.x - Width / 2)
        {
            Vector2 temp = Sprite.transform.position;
            Sprite.transform.position = MinorSprite.transform.position;
            MinorSprite.transform.position = temp;
            Debug.Log($"{this.name} swap sprites");
        }
    }
}
