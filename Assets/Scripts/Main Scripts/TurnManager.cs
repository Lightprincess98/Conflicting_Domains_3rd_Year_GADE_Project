using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    //Variables
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
	}

    //Queue's each unit by peeking at the top of the Queue without removing it and adds it based on each unit and their tag in the teamlist. NOTE: The last unit to be added will be first to go.
    static void InitTeamTurnQueue()
    {
        List<TacticsMove> teamList = units[turnKey.Peek()];

        foreach (TacticsMove unit in teamList)
        {
            turnTeam.Enqueue(unit);
            Debug.Log(unit);
        }

        StartTurn();
    }

    //Peaks at the top of the Queue and starts the begin turn function for the unit in TacticsMove, which sets its turn to True.
    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    //Checks the unit that went and dequeue the unit, sending it back to the beginning of the list, runs the EndTurn function in TacticsMove,
    //sets Turn to False, if there is still more people in the teamTurn Queue, start the next units turn in the queue
    // otherwise, dequeue the entire team, changes the turnKey to the next team and restart the process
    public static void EndTurn()
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    //Searches for all units based on the tag "Player" or "AI", adds them to the list. NOTE: It adds the units first based on the last added NPC or Player.
    public static void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }
}