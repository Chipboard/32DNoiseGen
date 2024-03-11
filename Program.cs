using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _32DNoiseGen.NoiseLayer;

namespace _32DNoiseGen
{
    internal static class Program
    {
        static NoiseForm form;
        static Bitmap previewImage;

        const int previewResolution = 512;

        static Dictionary<string, NoiseLayer> noiseLayers = new Dictionary<string, NoiseLayer>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new NoiseForm();
            form.addLayerButton.Click += AddNoiseLayer;
            form.removeLayerButton.Click += RemoveNoiseLayer;
            form.noiseList.SelectedIndexChanged += SelectedLayerChanged;
            form.previewDepthBar.ValueChanged += PreviewIndexChanged;
            form.noiseType.SelectedValueChanged += NoiseTypeChanged;
            form.combineType.SelectedValueChanged += CombineTypeChanged;
            form.amplitude.ValueChanged += AmplitudeChanged;
            form.frequency.ValueChanged += FrequencyChanged;
            form.seed.ValueChanged += SeedChanged;
            form.inverted.CheckedChanged += InvertedChanged;
            form.oneMinus.CheckedChanged += OneMinusChanged;
            form.absolute.CheckedChanged += AbsoluteChanged;
            form.FBM.CheckedChanged += FBMChanged;
            form.FBMGain.ValueChanged += FBMGainChanged;
            form.FBMLacunarity.ValueChanged += FBMLacunarityChanged;
            form.FBMOctaves.ValueChanged += FBMOctavesChanged;

            form.previewDepthBar.Minimum = 0;
            form.previewDepthBar.Maximum = previewResolution;
            previewImage = new Bitmap(previewResolution, previewResolution);
            form.previewImage.Image = previewImage;

            form.noiseType.Items.AddRange(new string[]
            {
                "Perlin",
                "Simplex",
                "CellularDistance"
            });

            foreach(CombineType type in Enum.GetValues(typeof(CombineType)))
            {
                form.combineType.Items.Add(type);
            }

            form.amplitude.DecimalPlaces = 4;
            form.frequency.DecimalPlaces = 4;
            form.FBMGain.DecimalPlaces = 4;
            form.FBMLacunarity.DecimalPlaces = 4;

            PreviewIndexChanged(null, null);
            UpdateNoiseControls(false);

            Application.Run(form);
        }

        private static void SeedChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.seed = (int)form.seed.Value;
            UpdatePreview();
        }

        private static void FBMOctavesChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.SetOctaves((int)form.FBMOctaves.Value);
            UpdatePreview();
        }

        private static void FBMLacunarityChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.SetLacunarity((float)form.FBMLacunarity.Value);
            UpdatePreview();
        }

        private static void FBMGainChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.SetGain((float)form.FBMGain.Value);
            UpdatePreview();
        }

        private static void FBMChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.useFBM = form.FBM.Checked;
            currentLayer.SetNoiseType(currentLayer.noiseType);
            UpdateNoiseControls(true);
            UpdatePreview();
        }

        private static void AbsoluteChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.absolute = form.absolute.Checked;
            UpdatePreview();
        }

        private static void OneMinusChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.oneMinus = form.oneMinus.Checked;
            UpdatePreview();
        }

        private static void InvertedChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.inverted = form.inverted.Checked;
            UpdatePreview();
        }

        private static void FrequencyChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.frequency = (float)form.frequency.Value;
            UpdatePreview();
        }

        private static void AmplitudeChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.amplitude = (float)form.amplitude.Value;
            UpdatePreview();
        }

        private static void CombineTypeChanged(object sender, EventArgs e)
        {
            int index = form.noiseList.SelectedIndex;

            if (index == -1)
                return;

            string layerName = form.noiseList.Items[index].ToString();

            noiseLayers[layerName].combineType = (CombineType)form.combineType.SelectedItem;

            UpdatePreview();
        }

        private static void NoiseTypeChanged(object sender, EventArgs e)
        {
            int index = form.noiseList.SelectedIndex;

            if (index == -1)
                return;

            string layerName = form.noiseList.Items[index].ToString();

            noiseLayers[layerName].SetNoiseType(form.noiseType.GetItemText(form.noiseType.SelectedItem));

            UpdatePreview();
        }

        private static void PreviewIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private static void SelectLayer(int index)
        {
            form.noiseList.SetItemChecked(index, true);
            form.noiseList.SetSelected(index, true);
            form.noiseList.SelectedIndex = index;
            SelectedLayerChanged(null, null);
        }

        private static void SelectedLayerChanged(object sender, EventArgs e)
        {
            int index = form.noiseList.SelectedIndex;

            if (index >= 0 && noiseLayers.Count > 0 && index < noiseLayers.Count)
                UpdateNoiseControls(true);
            else
                UpdateNoiseControls(false);
        }

        private static NoiseLayer GetActiveLayer()
        {
            int index = form.noiseList.SelectedIndex;
            string layerName = form.noiseList.Items[index].ToString();
            return noiseLayers[layerName];
        }

        private static void UpdateNoiseControls(bool enabled)
        {
            form.removeLayerButton.Enabled = enabled;
            form.noiseType.Enabled = enabled;
            form.combineType.Enabled = enabled;
            form.amplitude.Enabled = enabled;
            form.frequency.Enabled = enabled;
            form.inverted.Enabled = enabled;
            form.oneMinus.Enabled = enabled;
            form.absolute.Enabled = enabled;
            form.FBM.Enabled = enabled;
            form.FBMGain.Enabled = enabled;
            form.FBMLacunarity.Enabled = enabled;
            form.FBMOctaves.Enabled = enabled;
            form.seed.Enabled = enabled;

            if(enabled)
            {
                NoiseLayer activeLayer = GetActiveLayer();
                form.noiseType.Text = activeLayer.noiseType;
                form.combineType.Text = activeLayer.combineType.ToString();
                form.amplitude.Value = (decimal)activeLayer.amplitude;
                form.frequency.Value = (decimal)activeLayer.frequency;
                form.inverted.Checked = activeLayer.inverted;
                form.oneMinus.Checked = activeLayer.oneMinus;
                form.absolute.Checked = activeLayer.absolute;
                form.FBM.Checked = activeLayer.useFBM;
                form.FBMGain.Value = (decimal)activeLayer.FBMGain;
                form.FBMLacunarity.Value = (decimal)activeLayer.FBMLacunarity;
                form.FBMOctaves.Value = activeLayer.FBMOctaves;
                form.seed.Value = activeLayer.seed;

                if(!form.FBM.Checked)
                {
                    form.FBMGain.Enabled = false;
                    form.FBMLacunarity.Enabled= false;
                    form.FBMOctaves.Enabled= false;
                }
            }
        }

        private static void RemoveNoiseLayer(object sender, EventArgs e)
        {
            int index = form.noiseList.SelectedIndex;

            if (index == -1)
                return;

            string layerName = form.noiseList.Items[index].ToString();

            form.noiseList.Items.RemoveAt(index);
            noiseLayers.Remove(layerName);

            if (index < noiseLayers.Count)
                SelectLayer(index);
            else if (index > 0)
                SelectLayer(index - 1);

            UpdatePreview();
        }

        private static void AddNoiseLayer(object sender, EventArgs e)
        {
            int itemIndex = form.noiseList.Items.Count;
            string layerName = $"Layer {form.noiseList.Items.Count}";
            form.noiseList.Items.Add(layerName, true);
            noiseLayers.Add(layerName, new NoiseLayer("Perlin"));
            SelectLayer(itemIndex);
            UpdatePreview();
        }

        private static void UpdatePreview()
        {
            float depthScrollFactor = (float)form.previewDepthBar.Value / form.previewDepthBar.Maximum;
            int zStart = (int)Math.Round(depthScrollFactor * previewResolution);

            float[] noiseTotal = new float[previewResolution * previewResolution];
            int index = 0;
            foreach (KeyValuePair<string, NoiseLayer> v in noiseLayers)
            {
                if (form.noiseList.GetItemChecked(index++))
                {
                    float[] noiseData = new float[previewResolution * previewResolution];
                    v.Value.GetNoise3D(ref noiseData, 0, 0, zStart, previewResolution, previewResolution, 1);
                    v.Value.CombineNoise(ref noiseTotal, noiseData);
                }
            }

            /*MessageBox.Show(noiseData[256 * 256].ToString());
            for(int x = 0; x < previewResolution; x++)
            {
                for(int y = 0; y < previewResolution; y++)
                {
                    int value = (int)Math.Max(Math.Round(noiseData[(x * previewResolution) + y] * 255), 1);
                    previewImage.SetPixel(x, y, Color.FromArgb(255, value, value, value));
                }
            }*/

            SetGrayscaleBitmap(noiseTotal, previewImage);
            form.previewImage.Image = previewImage;
            form.previewImage.Update();
        }

        static void SetGrayscaleBitmap(float[] grayscaleData, Bitmap bitmap)
        {
            // Lock the bitmap bits for direct access
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            // Calculate bytes per pixel
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

            // Get the stride
            int stride = bmpData.Stride;

            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0;

                // Iterate over each row and column
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        float grayscaleValue = grayscaleData[y * bitmap.Width + x];

                        if(grayscaleValue < 0)
                            grayscaleValue = 0;
                        if (grayscaleValue > 1.0f)
                            grayscaleValue = 1.0f;

                        byte gray = (byte)(grayscaleValue * 255); // Convert float value to byte

                        // Set pixel values
                        ptr[x * bytesPerPixel] = gray; // Blue channel
                        ptr[x * bytesPerPixel + 1] = gray; // Green channel
                        ptr[x * bytesPerPixel + 2] = gray; // Red channel
                        ptr[x * bytesPerPixel + 3] = 255; // Alpha channel
                    }

                    // Move to the next row
                    ptr += stride;
                }
            }

            // Unlock the bitmap
            bitmap.UnlockBits(bmpData);
        }
    }
}
