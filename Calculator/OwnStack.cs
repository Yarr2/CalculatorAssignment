namespace Calculator;

public class OwnStack<T>
{
    private T[] _array;
    private int _capacity;
    private int _pointer;
    public int StackSize
    {
        get => _pointer;
    }// used for getting number of elements in stack

    public OwnStack(int capacity)
    {
        if (capacity <= 0)
        {
            throw new Exception($"Capacity should be greater then 0, not {capacity}");
        }
        _array = new T[capacity];
        _capacity = capacity;
    }

    public void Push(T value)
    {
        if (_pointer >= _capacity)
        {
            throw new Exception("Stack overflowed");
        }

        _array[_pointer] = value;
        _pointer++;
    }

    public T Pull()
    {
        if (_pointer == 0)
        {
            throw new Exception("Stack is empty");
        }

        _pointer--;
        T value = _array[_pointer];
        _array[_pointer] = default(T);// to clear a little bit of memory 
        return value;
    }
    
    public bool IsEmpty()
    {
        return (_pointer == 0);
    }
    
    public void Clear()
    {
        _array = new T[_capacity];
        _pointer = 0;
    }

    public void ShowStack()
    {
        foreach (T value in _array)
        {
            Console.WriteLine(value);
        }
    }
}
    