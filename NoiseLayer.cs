using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _32DNoiseGen.NoiseLayer;

namespace _32DNoiseGen
{
    public class NoiseLayer
    {
        public LayerSettings settings;
        FastNoise noise;

        public NoiseLayer(string noiseType = "Perlin", float frequency = 0.01f, float amplitude = 1.0f)
        {
            settings.amplitude = amplitude;
            settings.frequency = frequency;

            settings.seed = Program.random.Next(int.MinValue, int.MaxValue);
            settings.enabled = true;
            settings.FBMGain = 0.4f;
            settings.FBMLacunarity = 3.0f;
            settings.FBMOctaves = 8;

            SetNoiseType(noiseType);
        }

        public void SetGain(float gain)
        {
            settings.FBMGain = gain;

            if(settings.useFBM)
                noise.Set("Gain", settings.FBMGain);
        }

        public void SetLacunarity(float lacunarity)
        {
            settings.FBMLacunarity = lacunarity;

            if (settings.useFBM)
                noise.Set("Lacunarity", settings.FBMLacunarity);
        }

        public void SetOctaves(int octaves)
        {
            settings.FBMOctaves = octaves;

            if (settings.useFBM)
                noise.Set("Octaves", settings.FBMOctaves);
        }

        public void SetNoiseType(string noiseType)
        {
            settings.noiseType = noiseType;

            if (!settings.useFBM)
            {
                noise = new FastNoise(noiseType);
            } else
            {
                noise = new FastNoise("FractalFBm");
                noise.Set("Source", new FastNoise(noiseType));
                noise.Set("Gain", settings.FBMGain);
                noise.Set("Lacunarity", settings.FBMLacunarity);
                noise.Set("Octaves", settings.FBMOctaves);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetNoise3D(ref float[] array, int xStart, int yStart, int zStart, int xSize, int ySize, int zSize)
        {
            noise.GenUniformGrid3D(array, xStart, yStart, zStart, xSize, ySize, zSize, settings.frequency, settings.seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CombineNoise(ref float[] outArray, float[] combine)
        {
            for (int i = 0; i < outArray.Length; i++)
            {
                if(settings.oneMinus)
                    combine[i] = 1.0f - combine[i];

                if(settings.absolute)
                    combine[i] = Math.Abs(combine[i]);

                if (!settings.inverted)
                    combine[i] *= settings.amplitude;
                else
                    combine[i] *= -settings.amplitude;

                switch (settings.combineType)
                {
                    case CombineType.Add:
                        outArray[i] += combine[i];
                        break;

                    case CombineType.Additive:
                        if (combine[i] > 0)
                            outArray[i] += combine[i];
                        break;

                    case CombineType.Subtract:
                        outArray[i] -= combine[i];
                        break;

                    case CombineType.Subtractive:
                        if (combine[i] > 0)
                            outArray[i] -= combine[i];
                        break;

                    case CombineType.Multiply:
                        outArray[i] *= combine[i];
                        break;

                    case CombineType.Modulus:
                        outArray[i] %= combine[i];
                        break;

                    case CombineType.Min:
                        outArray[i] = Math.Min(outArray[i], combine[i]);
                        break;

                    case CombineType.Max:
                        outArray[i] = Math.Max(outArray[i], combine[i]);
                        break;
                }
            }
        }

        public enum CombineType
        {
            Add,
            Additive,
            Subtract,
            Subtractive,
            Multiply,
            Modulus,
            Min,
            Max
        }
    }

    [Serializable]
    public struct LayerSettings
    {
        public bool enabled;
        public bool inverted;
        public bool oneMinus;
        public bool absolute;
        public bool useFBM;

        public string noiseType;
        public CombineType combineType;
        public float amplitude;
        public float frequency;
        public float FBMGain;
        public float FBMLacunarity;
        public int FBMOctaves;
        public int seed;
    }
}