using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Card
{
    private CardType type;

    private Sprite sprite;

    public Card(CardType cardType)
    {
        type = cardType;
        switch (type)
        {
            case CardType.Kill:
                sprite = Resources.Load<Sprite>($"Sprites/Kill");
                break;
            case CardType.PlusOne:
                sprite = Resources.Load<Sprite>($"Sprites/PlusOne");
                break;
            case CardType.MinusTwo:
                sprite = Resources.Load<Sprite>($"Sprites/MinusTwo");
                break;
            case CardType.Zero:
                sprite = Resources.Load<Sprite>($"Sprites/Zero");
                break;
            default:
                sprite = null;
                break;
        }
    }

    public CardType GetCardType()
    {
        return type;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}

public enum CardType
{
    None,
    Kill,
    PlusOne,
    MinusTwo,
    Zero,
}
