using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPool : MonoBehaviour
{
    public List<NPC> NPCs;
    public List<bool> ActiveNPCs;

    private void Start()
    {
        NPCs = new List<NPC>();
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
            NPCSet = ActiveNPCs[id];
            id++;
        }

        if (NPCSet)
        {
            npc.id = id - 1;
            NPCs[id - 1] = npc;
        }
        else
        {
            npc.id = id;
            NPCs.Add(npc);
            ActiveNPCs.Add(true);
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
        
    }
}
