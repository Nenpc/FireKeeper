using System;

public interface IEndGame
{
    void EndGameSubscribe(Action function);
    void EndGameUnsubscribe(Action function);
}