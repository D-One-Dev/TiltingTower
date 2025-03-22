using TMPro;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private TMP_Text maxHeightText;
    [SerializeField] private Transform floorPoint;
    public override void InstallBindings()
    {
        Container.Bind<TMP_Text>()
            .WithId("MaxHeightText")
            .FromInstance(maxHeightText)
            .AsTransient();

        Container.BindInterfacesAndSelfTo<MaxHeightCounter>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<Transform>()
            .WithId("FloorPoint")
            .FromInstance(floorPoint)
            .AsTransient();
    }
}