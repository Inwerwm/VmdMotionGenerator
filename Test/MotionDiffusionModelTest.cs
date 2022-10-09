using VmdMotionGenerator.Core.Models;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IOTest()
    {
        MotionDiffusionModel.ConvertToVmd(
            @"\\wsl.localhost\Ubuntu\home\owner\motion-diffusion-model\save\humanml_trans_enc_512\samples_humanml_trans_enc_512_000200000_seed10_the_girl_is_walking_on_the_catwalk\results.npy",
            1,
            @"C:\MMD\_Motion-Diffution\cw.vmd");
    }
}