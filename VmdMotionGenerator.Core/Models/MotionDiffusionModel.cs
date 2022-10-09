using MikuMikuMethods.Vmd;
using Numpy;
using Python.Runtime;

namespace VmdMotionGenerator.Core.Models;
public static class MotionDiffusionModel
{
    public static int GetRepetitionsCount(string npyPath) => LoadMotions(npyPath).shape[0];

    public static void ConvertToVmd(string npyPath, int repId, string savePath)
    {
        var motions = LoadMotions(npyPath);

        var vmd = new VocaloidMotionData()
        {
            ModelName = "22balls"
        };
        float scale = 10;
        for (var nodeId = 0; nodeId < motions.shape[1]; nodeId++)
        {
            for (var frameId = 0; frameId < motions.shape[3]; frameId++)
            {
                vmd.MotionFrames.Add(new($"Bone.{nodeId:000}", (uint)frameId)
                {
                    Position = new(
                        ((float)motions[repId, nodeId, 0, frameId]) * scale,
                        ((float)motions[repId, nodeId, 1, frameId]) * scale,
                        ((float)motions[repId, nodeId, 2, frameId]) * scale)
                });
            }
        }

        vmd.Write(savePath);
    }

    private static NDarray<float> LoadMotions(string npyPath)
    {
        var result = np.load(npyPath, allow_pickle: true);
        return new NDarray<float>(result.flat[0]["motion"]);
    }
}
