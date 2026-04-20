using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI turnText;

    private DoubleLinkedList<TurnData> timeline = new DoubleLinkedList<TurnData>();

    private int playerLife = 100;
    private int playerAttack = 10;

    void Start()
    {
        SaveTurn();
        UpdateView();
    }

    [Button]
    public void MoveForward()
    {
        player.position += Vector3.forward;

        SaveTurn();
        UpdateView();
    }

    [Button]
    public void MoveBack()
    {
        player.position += Vector3.back;

        SaveTurn();
        UpdateView();
    }

    [Button]
    public void MoveRight()
    {
        player.position += Vector3.right;

        SaveTurn();
        UpdateView();
    }

    [Button]
    public void MoveLeft()
    {
        player.position += Vector3.left;

        SaveTurn();
        UpdateView();
    }

    [Button]
    public void AddLife()
    {
        playerLife += 5;

        SaveTurn();
        UpdateView();
    }

    [Button]
    public void RemoveLife()
    {
        playerLife -= 5;

        SaveTurn();
        UpdateView();
    }

    [Button]
    public void SaveTurn()
    {
        if (timeline.pivot != timeline.last)
        {
            timeline.RemoveFuture();
        }

        TurnData data = new TurnData()
        {
            PlayerPosition = player.position,
            PlayerLife = playerLife,
            PlayerAttack = playerAttack
        };

        timeline.Add(data);
    }

    [Button]
    public void NextTurn()
    {
        timeline.MoveNext();
        ApplyTurn();
    }

    [Button]
    public void PrevTurn()
    {
        timeline.MovePrev();
        ApplyTurn();
    }

    void ApplyTurn()
    {
        if (timeline.pivot == null)
            return;

        TurnData data = timeline.pivot.Value;

        player.position = data.PlayerPosition;
        playerLife = data.PlayerLife;
        playerAttack = data.PlayerAttack;

        UpdateView();
    }

    void UpdateView()
    {
        if (timeline.pivot == null)
            return;

        TurnData data = timeline.pivot.Value;

        turnText.text =
            "Pos: " + data.PlayerPosition +
            "\nVida: " + data.PlayerLife +
            "\nAtaque: " + data.PlayerAttack;
    }
}