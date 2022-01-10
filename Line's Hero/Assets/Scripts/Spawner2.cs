using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public BlockController blockController;
    
    public GameObject[] prefBlocks;
    public GameObject[] prefSolidBlocks;
    static public GameObject activeBlock;
    
    private GameObject holdBlock;
    public Transform holdPosition;
    
    public List<int> nextBlocksIndex;
    public GameObject NextBlock_0;
    public GameObject NextBlock_1;
    public GameObject NextBlock_2;

    private void Start()
    {
        NextBlocks();
        SpawnNewBlock();
    }

    public void SpawnNewBlock()
    {
        if (nextBlocksIndex.Count > 0)
        {
            activeBlock = Instantiate(prefBlocks[nextBlocksIndex[0]], transform.position, new Quaternion(0, 0, 0, 0));
            blockController.blockTransform = activeBlock.transform;
            blockController.rotationPoint = activeBlock.transform.GetChild(4);
            Destroy(NextBlock_0);
            Destroy(NextBlock_1);
            Destroy(NextBlock_2);
            nextBlocksIndex.RemoveAt(0);
            NextBlocks();
        } 
    }

    public void NextBlocks()
    {
        while (nextBlocksIndex.Count < 3)
            nextBlocksIndex.Add(Random.Range(0, prefBlocks.Length));

        var nextBlockQuaternion = Quaternion.Euler(-90, 0, 0);
        NextBlock_0 = Instantiate(prefSolidBlocks[nextBlocksIndex[0]], NextBlock_0.transform.position, nextBlockQuaternion);
        NextBlock_1 = Instantiate(prefSolidBlocks[nextBlocksIndex[1]], NextBlock_1.transform.position, nextBlockQuaternion);
        NextBlock_2 = Instantiate(prefSolidBlocks[nextBlocksIndex[2]], NextBlock_2.transform.position, nextBlockQuaternion);
    }

    public void Hold()
    {
        if (holdBlock == null)
        {
            holdBlock = activeBlock;

            holdBlock.transform.position = holdPosition.position;
            holdBlock.transform.rotation = holdPosition.rotation;
            holdBlock.transform.localScale = holdPosition.localScale;

            SpawnNewBlock();
        }
        else
        {
            var tempBlock = activeBlock;

            activeBlock = holdBlock;
            activeBlock.transform.position = tempBlock.transform.position;
            activeBlock.transform.localScale = tempBlock.transform.localScale;

            blockController.blockTransform = activeBlock.transform;
            blockController.rotationPoint = activeBlock.transform.GetChild(4);
        }
    }
}

