using System.ComponentModel;

namespace Calculator;

public class OwnQueue<T>
{
    private T[] _array;
    private int _capacity;
    private int _startingPoint;
    private int _endingPoint;

    public int Size => _endingPoint - _startingPoint;

    public OwnQueue(int capacity)
    {
        if (capacity <= 0)
        {
            throw new Exception($"Capacity should be greater then 0, not {capacity}");
        }

        _capacity = capacity;
        _array = new T[capacity];
    }

    public void Add(T value)
    {
        if (_endingPoint <= _capacity)
        {
            _array[_endingPoint] = value;
            _endingPoint++;
            return;
        }
        if (_startingPoint == 0)
        {
            throw new Exception("Queue overflowed");
        }

        for (int i = _startingPoint; i < _endingPoint; i++)
        {
            _array[_startingPoint - i] = _array[_startingPoint];
            _array[_startingPoint] = default(T);
        }

        _endingPoint -= _startingPoint;
        _startingPoint = 0;
        _array[_endingPoint] = value;
        _endingPoint++;
        
    }

    public T Pop()
    {
        if (Size == 0)
        {
            throw new Exception("There are no elements in queue");
        }

        T value = _array[_startingPoint];
        _startingPoint++;
        return value;
    }

    public void Clear()
    {
        _array = new T[_capacity];
        _startingPoint = 0;
        _endingPoint = 0;
    }

    public void ShowQueue()
    {
        for (int i = _startingPoint; i < _endingPoint; i++ )
        {
            Console.WriteLine(_array[i]);
        }
    }

    public T[] ToArray()
    {
        T[] array = new T[Size];
        for (int index = _startingPoint; index < _endingPoint; index ++)
        {
            array[index - _startingPoint] = _array[index];
        }

        return array;
    }
    
}