using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCPool : MonoBehaviour
{
    public List<NPC> NPCs;
    public List<bool> ActiveNPCs;
    public List<GameObject> NPCVisuals;
    public Transform VisualsParent;

    public CharacterVisualsGenerator CVG;

    public UnityEvent NPCAnnihilatedEvent;

    private void Start()
    {
        NPCs = new List<NPC>();
        NPCVisuals = new List<GameObject>();
    }

    /// <summary>
    /// Registers the npc.
    /// </summary>
    /// <returns>The npc.</returns>
    /// <param name="npc">Npc.</param>
    public NPC RegisterNPC(NPC npc)
    {
        bool NPCSet = false;

        int id = 0;

        while(id < ActiveNPCs.Count && !NPCSet)
        {
            NPCSet = !ActiveNPCs[id];
            id++;
        }

        if (NPCSet)
        {
            npc.ID = id - 1;
            NPCs[id - 1] = npc;
            ActiveNPCs[id - 1] = true;
            NPCVisuals[NPCVisuals.Count - 1].GetComponent<NPCDisplayer>().GenerateVisuals();
        }
        else
        {
            npc.ID = id;
            NPCs.Add(npc);
            ActiveNPCs.Add(true);
            CVG.GeneratePrefab(VisualsParent);
            NPCVisuals.Add(VisualsParent.GetChild(VisualsParent.childCount - 1).gameObject);
            NPCVisuals[NPCVisuals.Count - 1].GetComponent<NPCDisplayer>().GenerateVisuals();
        }

        return npc;
    }

    public List<NPC> GetRevisitingNPCS()
    {
        var npcs = new List<NPC>();

        for(int i = 0; i < NPCs.Count; i++)
        {
            if (ActiveNPCs[i])
            {
                bool isAway = NPCs[i].StayDuration < 0;

                if (isAway)
                {
                    NPCs[i].StayDuration++;

                    if (NPCs[i].StayDuration == 0)
                    {
                        NPCs[i].StayDuration += NPCs[i].FutureStayDuration;
                        npcs.Add(NPCs[i]);
                    }
                }
            }
        }
        return npcs;
    }

    public List<NPC> GetLeavingNPCS()
    {
        var npcs = new List<NPC>();

        for (int i = 0; i < NPCs.Count; i++)
        {
            if (ActiveNPCs[i])
            {
                bool isStaying = NPCs[i].StayDuration > 0;

                if (isStaying)
                {
                    NPCs[i].StayDuration--;

                    if (NPCs[i].StayDuration == 0)
                    {
                        npcs.Add(NPCs[i]);
                    }
                }
            }
        }
        return npcs;
    }

    public void UpdateNPC(NPC npc)
    {
        NPCs[npc.ID] = npc;
    }

    public void AnnihilateNPC(int id)
    {
        ActiveNPCs[id] = false;
        NPCAnnihilatedEvent.Invoke();
    }

    public void ShowEnteringNPC(int id)
    {
        NPCVisuals[id].SetActive(true);
        NPCVisuals[id].gameObject.GetComponent<NPCMover>().MoveToTargetPosition();
    }
}
