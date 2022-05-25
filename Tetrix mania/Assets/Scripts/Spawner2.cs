using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner2 : MonoBehaviour
{
    [SerializeField] private BlockController blockController;
    [SerializeField] private GameObject[] prefBlocks;
    [SerializeField] private GameObject[] prefSolidBlocks;

    [SerializeField] private GameObject nextBlock_0;
    [SerializeField] private GameObject nextBlock_1;
    [SerializeField] private GameObject nextBlock_2;
    [SerializeField] private Transform holdPosition;
    [SerializeField] private UnityEvent onHold;

    private GameObject activeBlock;
    private List<int> nextBlocksIndex;
    private GameObject holdBlock;
    private bool isHolded;

    private void Start()
    {
        nextBlocksIndex = new List<int>();
        RefreshNextBlocks();
        SpawnNewBlock();
    }

    private void RefreshNextBlocks()
    {
        while (nextBlocksIndex.Count < 3)
            nextBlocksIndex.Add(Random.Range(0, prefBlocks.Length));

        var nextBlockQuaternion = Quaternion.Euler(-90, 0, 0);
        nextBlock_0 = Instantiate(prefSolidBlocks[nextBlocksIndex[0]], nextBlock_0.transform.position, nextBlockQuaternion);
        nextBlock_1 = Instantiate(prefSolidBlocks[nextBlocksIndex[1]], nextBlock_1.transform.position, nextBlockQuaternion);
        nextBlock_2 = Instantiate(prefSolidBlocks[nextBlocksIndex[2]], nextBlock_2.transform.position, nextBlockQuaternion);
    }

    public void SpawnNewBlock()
    {
        if (nextBlocksIndex.Count > 0)
        {
            activeBlock = Instantiate(prefBlocks[nextBlocksIndex[0]], transform.position, new Quaternion(0, 0, 0, 0));
            blockController.SetActiveBlock(activeBlock.transform);
            Destroy(nextBlock_0);
            Destroy(nextBlock_1);
            Destroy(nextBlock_2);
            nextBlocksIndex.RemoveAt(0);
            RefreshNextBlocks();
            isHolded = false;
        } 
    }

    public void Hold()
    {
        if (holdBlock == null)
        {
            onHold.Invoke();
            holdBlock = activeBlock;
            HoldBlockTransform();
            SpawnNewBlock();
            isHolded = true;
        }
        else if(!isHolded)
        {
            onHold.Invoke();
            var tempBlock = activeBlock;

            activeBlock = holdBlock;
            activeBlock.transform.position = transform.position;
            activeBlock.transform.localScale = tempBlock.transform.localScale;

            holdBlock = tempBlock;
            HoldBlockTransform();
            isHolded = true;

            blockController.SetActiveBlock(activeBlock.transform);
        }
    }

    private void HoldBlockTransform()
    {
        holdBlock.transform.position = holdPosition.position;
        holdBlock.transform.rotation = holdPosition.rotation;
        holdBlock.transform.localScale = holdPosition.localScale;
    }
}

