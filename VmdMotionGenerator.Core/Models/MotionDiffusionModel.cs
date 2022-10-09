using MikuMikuMethods.Vmd;
using Numpy;
using Python.Runtime;

namespace VmdMotionGenerator.Core.Models;
public class MotionDiffusionModel
{
    public static void ConvertToVmd(string npyPath, int repId, string savePath)
    {
        var result = np.load(npyPath, allow_pickle: true);
        var motions = new NDarray<float>(result.flat[0]["motion"]);

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
}
