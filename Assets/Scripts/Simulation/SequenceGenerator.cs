using System.Collections.Generic;

using Random = UnityEngine.Random;

public class SequenceGenerator
{
    public static Queue<Process> GenerateSequence()
    {
        Queue<Process> queue = new Queue<Process>();

        for (int i = 0; i < SimulationManager.Instance.simulationSettings.sequenceLength; i++)
        {
            queue.Enqueue(new Process
            {
                id = Random.Range(0, SimulationManager.Instance.simulationSettings.processCount),
                index = i
            });
        }

        return queue;
    }
}
