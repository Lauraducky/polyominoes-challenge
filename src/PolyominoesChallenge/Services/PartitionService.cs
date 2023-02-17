namespace PolyominoesChallenge.Services;

public class PartitionService : IPartitionService
{
    public int[][] GetPartitionsOfNumber(int integer)
    {
        var partitions = new List<List<int>>();
        // first partition is the number itself
        partitions.Add(new List<int>{integer});

        var currentPartition = partitions[0];
        while (currentPartition.Any(x => x > 1))
        {
            currentPartition = GetNextPartition(currentPartition);
            partitions.Add(currentPartition);
        }

        return partitions.Select(x => x.ToArray()).ToArray();
    }

    private List<int> GetNextPartition(List<int> currentPartition)
    {
        var output = new List<int>(currentPartition);
        var remaining = 0;
        for (var i = output.Count - 1; i >= 0; i--)
        {
            remaining++;
            if (output[i] == 1)
            {
                continue;
            }

            output[i]--;
            output.RemoveRange(i + 1, output.Count - i - 1);
            if (output[i] < remaining)
            {
                while (remaining > output[i])
                {
                    output.Add(output[i]);
                    remaining -= output[i];
                }
            }

            output.Add(remaining);
            break;
        }

        return output;
    }
}