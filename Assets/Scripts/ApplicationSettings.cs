using UnityEngine;
using Zenject;

public class ApplicationSettings : IInitializable
{
    public void Initialize()
    {
        Application.targetFrameRate = 120;
    }
}