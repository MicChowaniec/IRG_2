using Unity.VisualScripting;
using UnityEngine;
using System;

public class RootSkill :AbstractSkill
{
    public Vector3 currentPosition;

    public PlayerManager pm;

    public static event Action UpdateRootables;

    public void LateUpdate()
    {
        if (activePlayerObject == null&&activePlayer!=null)
        {
            FindPlayerTransform();
        }
    }
    public void FindPlayerTransform()
    {
        activePlayerObject = pm.GetGameObjectFromSO(activePlayer);

    }
    public override void Do()
    {


        tileWherePlayerStands = activePlayerObject.GetComponent<PlayerScript>().tile;
        if (tileWherePlayerStands.rootable == true)
        {
            Vector3 rememberLocalScale = activePlayerObject.transform.localScale;
            GameObject tree = Instantiate(activePlayer.TreePrefab, activePlayerObject.transform.position, Quaternion.identity, tileWherePlayerStands.representation.transform);
            tree.transform.localScale = rememberLocalScale / 5;

            tileWherePlayerStands.rootable = false;
            tileWherePlayerStands.passable = false;
            tileWherePlayerStands.SetOwner( activePlayer);
            foreach (var tile in tileWherePlayerStands.neighbours)
            {
                tile.rootable = false;
                tile.SetOwner(activePlayer);
            }
        }
        else
        {
            return;
        }

        UpdateRootables?.Invoke();
        StatisticChange();

        Confirm();

        DisableFunction();

    }
}
