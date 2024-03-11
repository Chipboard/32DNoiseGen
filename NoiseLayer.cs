using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _32DNoiseGen
{
    public class NoiseLayer
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
        public float FBMGain { get; private set; }
        public float FBMLacunarity { get; private set; }
        public int FBMOctaves { get; private set; }
        public int seed;

        FastNoise noise;

        public NoiseLayer(string noiseType = "Perlin", float frequency = 0.01f, float amplitude = 1.0f, int seed = 777, bool enabled = true)
        {
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.seed = seed;
            this.enabled = enabled;

            FBMGain = 0.4f;
            FBMLacunarity = 3.0f;
            FBMOctaves = 8;

            SetNoiseType(noiseType);
        }

        public void SetGain(float gain)
        {
            FBMGain = gain;

            if(useFBM)
                noise.Set("Gain", FBMGain);
        }

        public void SetLacunarity(float lacunarity)
        {
            FBMLacunarity = lacunarity;

            if (useFBM)
                noise.Set("Lacunarity", FBMLacunarity);
        }

        public void SetOctaves(int octaves)
        {
            FBMOctaves = octaves;

            if (useFBM)
                noise.Set("Octaves", FBMOctaves);
        }

        public void SetNoiseType(string noiseType)
        {
            this.noiseType = noiseType;

            if (!useFBM)
            {
                noise = new FastNoise(noiseType);
            } else
            {
                noise = new FastNoise("FractalFBm");
                noise.Set("Source", new FastNoise(noiseType));
                noise.Set("Gain", FBMGain);
                noise.Set("Lacunarity", FBMLacunarity);
                noise.Set("Octaves", FBMOctaves);
            }
        }

        public void GetNoise3D(ref float[] array, int xStart, int yStart, int zStart, int xSize, int ySize, int zSize)
        {
            noise.GenUniformGrid3D(array, xStart, yStart, zStart, xSize, ySize, zSize, frequency, seed);
        }

        public void CombineNoise(ref float[] outArray, float[] combine)
        {
            for (int i = 0; i < outArray.Length; i++)
            {
                if(oneMinus)
                    combine[i] = 1.0f - combine[i];

                if(absolute)
                    combine[i] = Math.Abs(combine[i]);

                if (!inverted)
                    combine[i] *= amplitude;
                else
                    combine[i] *= -amplitude;

                switch (combineType)
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
            Modulus
        }
    }
}