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

    private bool isPlaying = false;
    private float timer = 0f;
    public float delay = 1f;

    void Start()
    {
        SaveTurn();
        UpdateView();
    }

    void Update()
    {
        if (!isPlaying || timeline.pivot == null)
            return;

        timer += Time.deltaTime;

        if (timer >= delay)
        {
            timer = 0f;

            ApplyTurn();

            timeline.pivot = timeline.pivot.Next;

            if (timeline.pivot == null)
            {
                isPlaying = false;
            }
        }
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

    [Button]
    public void PlayAuto()
    {
        if (timeline.head == null)
            return;

        isPlaying = true;
        timer = 0f;
        timeline.pivot = timeline.head;
    }

    [Button]
    public void StopAuto()
    {
        isPlaying = false;
    }
}