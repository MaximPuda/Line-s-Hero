using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefBlocks;
    public GameObject[] prefSolidBlocks;
    static public GameObject activeBlock;
    private GameObject holdBlock;
    public Transform holdPosition;
    public List<int> nextBlocks;
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
        if (nextBlocks.Count > 0)
        {
            activeBlock = (Instantiate(prefBlocks[nextBlocks[0]], transform.position, new Quaternion(0, 0, 0, 0)));
            Destroy(NextBlock_0);
            Destroy(NextBlock_1);
            Destroy(NextBlock_2);
            nextBlocks.RemoveAt(0);
            NextBlocks();
        } 
    }

    public void NextBlocks()
    {
        while (nextBlocks.Count < 3)
            nextBlocks.Add(Random.Range(0, prefBlocks.Length));

        NextBlock_0 = Instantiate(prefBlocks[nextBlocks[0]], NextBlock_0.transform.position, new Quaternion(0, 0, 0, 0));
        NextBlock_0.GetComponent<BlockController>().enabled = false;
        NextBlock_0.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        foreach (Transform children in NextBlock_0.transform)
        {
            children.tag = "Next";
        }


        NextBlock_1 = Instantiate(prefBlocks[nextBlocks[1]], NextBlock_1.transform.position, new Quaternion(0, 0, 0, 0));
        NextBlock_1.GetComponent<BlockController>().enabled = false;
        NextBlock_1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        foreach (Transform children in NextBlock_1.transform)
        {
            children.tag = "Next";
        }


        NextBlock_2 = Instantiate(prefBlocks[nextBlocks[2]], NextBlock_2.transform.position, new Quaternion(0, 0, 0, 0));
        NextBlock_2.GetComponent<BlockController>().enabled = false;
        NextBlock_2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        foreach (Transform children in NextBlock_2.transform)
        {
            children.tag = "Next";
        }
    }

    public void NewNextBlocks()
    {
        while (nextBlocks.Count < 3)
            nextBlocks.Add(Random.Range(0, prefBlocks.Length));

    }



    public void Hold()
    {
        if (holdBlock == null)
        {
            holdBlock = activeBlock;
            holdBlock.SetActive(false);
            holdBlock.GetComponent<BlockController>().enabled = false;
            foreach (Transform children in holdBlock.transform)
            {
                children.tag = "Hold";
            }
            holdBlock.transform.position = holdPosition.position;
            holdBlock.SetActive(true);

            SpawnNewBlock();
            activeBlock.transform.position = holdBlock.transform.position;
        }
    }
}
