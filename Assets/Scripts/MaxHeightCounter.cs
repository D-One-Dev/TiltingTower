using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MaxHeightCounter : IInitializable
{
    [Inject(Id = "MaxHeightText")]
    private readonly TMP_Text _maxHeightText;
    [Inject(Id = "FloorPoint")]
    private readonly Transform _floorPoint;

    private float _maxHeight = float.NegativeInfinity;
    private float _verticalBias;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        eventHandler.OnUpdateMaxHeight += UpdateMaxHeight;
    }

    public void Initialize()
    {
        _verticalBias = 0 - _floorPoint.transform.position.y;
        _maxHeightText.text = "Max height: 0";
    }

    public void UpdateMaxHeight(GameObject activeBlock)
    {
        if (activeBlock.transform.position.y > _maxHeight)
        {
            _maxHeight = activeBlock.transform.position.y;
            _maxHeightText.text = "Max height: " + Math.Round((decimal)((_maxHeight + _verticalBias) * 10), 1);
        }
    }
}