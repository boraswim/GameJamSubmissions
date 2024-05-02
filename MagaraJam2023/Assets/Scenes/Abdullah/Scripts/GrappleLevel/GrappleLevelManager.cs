using System.Collections.Generic;
using UnityEngine;

public class GrappleLevelManager : MonoBehaviour
{
    [System.Serializable]
    private class HitAndDoor
    {
        public List<HitTarget> Targets;
        public List<FloorDoor> Doors;
    }

    [SerializeField]private List<HitAndDoor> hitsAndDoors;

    private void Awake()
    {
        foreach(var hit in hitsAndDoors)
        {
            foreach (var target in hit.Targets)
            {
                target.OnHitBySpear += CheckDoor;
            }
        }
    }

    private void CheckDoor()
    {
        foreach (var hit in hitsAndDoors)
        {
            bool _openDoor = true;
            foreach (var target in hit.Targets)
            {
                if (!target.hit)
                {
                    _openDoor = false;
                    break;
                }
            }
            if (_openDoor && !hit.Doors[0].isOpen)
            {
                foreach(var door in hit.Doors)
                {
                    door.Open();
                }
            }
        }
    }
}
