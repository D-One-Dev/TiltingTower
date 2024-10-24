using TMPro;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private TMP_Text maxHeightText;
    [SerializeField] private Transform floorPoint;
    public override void InstallBindings()
    {
        this.Container.Bind<TMP_Text>()
            .WithId("MaxHeightText")
            .FromInstance(maxHeightText)
            .AsTransient();

        this.Container.BindInterfacesAndSelfTo<MaxHeightCounter>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        this.Container.Bind<Transform>()
            .WithId("FloorPoint")
            .FromInstance(floorPoint)
            .AsTransient();
    }
}