using UnityEngine;

public class DoubleLinkedList<T>
{
    public Node<T> head = null;
    public Node<T> last = null;
    public Node<T> pivot = null;

    public int Count;

    public void Add(T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (head == null)
        {
            head = newNode;
            last = newNode;
            pivot = newNode;
        }
        else
        {
            last.SetNext(newNode);
            newNode.SetPrev(last);

            last = newNode;
            pivot = newNode; 
        }

        Count++;
    }

    public void MoveNext()
    {
        if (pivot == null)
            return;

        if (pivot.Next != null)
        {
            pivot = pivot.Next;
        }
        else
        {
            Debug.Log("Ya estás en el último turno");
        }
    }

    public void MovePrev()
    {
        if (pivot == null)
            return;

        if (pivot.Prev != null)
        {
            pivot = pivot.Prev;
        }
        else
        {
            Debug.Log("Ya estás en el primer turno");
        }
    }

    public void RemoveFuture()
    {
        if (pivot == null)
            return;

        Node<T> current = pivot.Next;

        while (current != null)
        {
            Node<T> temp = current;
            current = current.Next;

            temp.SetPrev(null);
            temp.SetNext(null);

            Count--;
        }

        pivot.SetNext(null);
        last = pivot;
    }
}