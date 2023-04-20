using System;

public interface IConsumable<T>
{
    public void SetConsumeCallback (Action<T> callback);
}
