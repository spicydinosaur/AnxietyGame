using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesForTheGameManager : MonoBehaviour
{
    public GameObject ruinsEntrance;
    public GameObject ruinsStairs;
    public GameObject ruinsThreeDoor;
    public DungeonDoorSuccessfullyOpened ruinsDoorThreeScript;
    public Vector3 FlameSpellLootLoc;
    public GameObject flameSpellLoot;
    public GameObject flameSpellLootInst;
    public GameObject ruinsRoomThreeExit;
    public GameObject hero;
    public Player player;
    //more as we work ever towards completing the tutorial!

    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        player = hero.GetComponent<Player>();

        if (GameManager.tutorialHasRuinsKey)
        {
            //nothing needed here, the gate can be open and shut at will at this point and onwards.
        }
        if (GameManager.tutorialRuinsEntranceRevealed)
        {
            ruinsEntrance.GetComponent<SpriteRenderer>().color = new Vector4(115, 114, 114, 255);
            ruinsEntrance.GetComponent<Animator>().SetTrigger("alreadyLit");
        }
        if (GameManager.tutorialRuinsStairsRevealed)
        {
            ruinsStairs.GetComponent<SpriteRenderer>().color = new Vector4(ruinsStairs.GetComponent<SpriteRenderer>().color.r, ruinsStairs.GetComponent<SpriteRenderer>().color.g, ruinsStairs.GetComponent<SpriteRenderer>().color.b, 255);
            ruinsStairs.GetComponent<Animator>().SetTrigger("alreadyOpened");
        }
        if (GameManager.tutorialRuinsThreeDoorOpened)
        {
            ruinsThreeDoor.GetComponent<Collider2D>().enabled = false;
            ruinsThreeDoor.GetComponent<Animator>().SetTrigger("alreadyOpened");
            ruinsThreeDoor.GetComponent<SpriteRenderer>().sprite = ruinsThreeDoor.GetComponent<DungeonDoorSuccessfullyOpened>().dungeonDoorOpened;
        }
        if (GameManager.tutorialPixieDefeated)
        {
            if (GameManager.tutorialFlameSpellObtained)
            {
                player.selectedSpellMax = 3;
                ruinsRoomThreeExit.SetActive(false);
            }
            else
            {
                flameSpellLootInst = Instantiate(flameSpellLoot, FlameSpellLootLoc, Quaternion.identity);
                flameSpellLootInst.SetActive(true);
                flameSpellLootInst.GetComponent<Animator>().SetTrigger("hasAlreadyDropped");
                player.selectedSpellMax = 2;
                ruinsRoomThreeExit.SetActive(true);
            }
        }
    }


}
