using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CRUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> PlayImgs;
    [SerializeField]
    private List<Button> stageButtons;
    [SerializeField]
    private List<Button> MaxPlayButtons;

    private CreateGameRoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        roomData = new CreateGameRoomData() { StageCount = 1, MaxPlayerCount = 4 };
        UpdateStageImages();

    }

    private void UpdateStageImages()
    {
        int StageCount = roomData.StageCount;
        int idx = 0;
        while (StageCount != 0)
        {
            if (idx >= roomData.MaxPlayerCount)
            {
                idx = 0;
            }

            if (PlayImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
            {
                PlayImgs[idx].material.SetColor("_PlayerColor", Color.red);
                StageCount--;
            }
            idx++;
        }
        for (int i = 0; i < PlayImgs.Count; i++)
        {
            if (i < roomData.MaxPlayerCount)
            {
                PlayImgs[i].gameObject.SetActive(true);
            }
            else
            {
                PlayImgs[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateRoom()
    {

    }
}
public class CreateGameRoomData
{
    public int StageCount;
    public int MaxPlayerCount;
}

