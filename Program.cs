using _32DNoiseGen.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _32DNoiseGen.NoiseLayer;

namespace _32DNoiseGen
{
    internal static class Program
    {
        static NoiseForm form;
        static ExportForm exportForm;
        static Bitmap previewImage;

        const int previewResolution = 256;

        public static Dictionary<string, NoiseLayer> noiseLayers = new Dictionary<string, NoiseLayer>();

        public static Random random = new Random();

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
            form.tilingMode.SelectedValueChanged += TilingModechanged;
            form.strip_save.Click += Save;
            form.strip_load.Click += Load;
            form.strip_export.Click += Export;

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

            form.tilingMode.Items.AddRange(new string[]
            {
                "None",
                "Center Edge Blend",
                "Mirrored Edge (Round)",
                "Mirrored Edge (Square)",
                "Mirrored",
                //"Scattered Edges" //needs improved, bugged
            });

            form.tilingMode.SelectedIndex = 0;

            foreach (CombineType type in Enum.GetValues(typeof(CombineType)))
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

        private static void TilingModechanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private static void Export(object sender, EventArgs e)
        {
            exportForm?.Close();
            exportForm = new ExportForm();
            exportForm.Show();
        }

        private static void Load(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                InitialDirectory = Application.ExecutablePath,
                Title = "Load Layers",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "32D",
                Filter = "32D files (*.32D)|*.32D",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream stream = new FileStream(openDialog.FileName, FileMode.Open))
                    {
                        noiseLayers.Clear();
                        form.noiseList.Items.Clear();

                        BinaryFormatter bin = new BinaryFormatter();
                        SaveData data =  (SaveData)bin.Deserialize(stream);

                        for(int i = 0; i < data.settings.Length; i++)
                        {
                            string layerName = $"Layer {i}";
                            form.noiseList.Items.Add(layerName, true);
                            NoiseLayer layer = new NoiseLayer(data.settings[i].noiseType)
                            {
                                settings = data.settings[i]
                            };

                            noiseLayers.Add(layerName, layer);
                        }

                        for (int i = 0; i < data.settings.Length; i++)
                        {
                            SelectLayer(i);
                        }
                        UpdatePreview();
                    }
                }
                catch (IOException)
                {
                }
            }
        }

        private static void Save(object sender, EventArgs e)
        {
            string savePath = IOUtility.SaveDialog("32D");

            if (savePath != null)
            {
                try
                {
                    using(FileStream stream = new FileStream(savePath, FileMode.Create))
                    {
                        LayerSettings[] settings = new LayerSettings[noiseLayers.Count];

                        int i = 0;
                        foreach(KeyValuePair<string, NoiseLayer> v in noiseLayers)
                        {
                            settings[i++] = v.Value.settings;
                        }

                        SaveData data = new SaveData()
                        {
                            settings = settings
                        };

                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, data);
                    }
                } catch (IOException)
                {
                }
            }
        }

        [Serializable]
        struct SaveData
        {
            public LayerSettings[] settings;
        }

        private static void SeedChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.settings.seed = (int)form.seed.Value;
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

            currentLayer.settings.useFBM = form.FBM.Checked;
            currentLayer.SetNoiseType(currentLayer.settings.noiseType);
            UpdateNoiseControls(true);
            UpdatePreview();
        }

        private static void AbsoluteChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.settings.absolute = form.absolute.Checked;
            UpdatePreview();
        }

        private static void OneMinusChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.settings.oneMinus = form.oneMinus.Checked;
            UpdatePreview();
        }

        private static void InvertedChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.settings.inverted = form.inverted.Checked;
            UpdatePreview();
        }

        private static void FrequencyChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.settings.frequency = (float)form.frequency.Value;
            UpdatePreview();
        }

        private static void AmplitudeChanged(object sender, EventArgs e)
        {
            NoiseLayer currentLayer = GetActiveLayer();
            if (currentLayer == null)
                return;

            currentLayer.settings.amplitude = (float)form.amplitude.Value;
            UpdatePreview();
        }

        private static void CombineTypeChanged(object sender, EventArgs e)
        {
            int index = form.noiseList.SelectedIndex;

            if (index == -1)
                return;

            string layerName = form.noiseList.Items[index].ToString();

            noiseLayers[layerName].settings.combineType = (CombineType)form.combineType.SelectedItem;

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

            if (enabled)
            {
                NoiseLayer activeLayer = GetActiveLayer();
                form.noiseType.Text = activeLayer.settings.noiseType;
                form.combineType.Text = activeLayer.settings.combineType.ToString();
                form.amplitude.Value = (decimal)activeLayer.settings.amplitude;
                form.frequency.Value = (decimal)activeLayer.settings.frequency;
                form.inverted.Checked = activeLayer.settings.inverted;
                form.oneMinus.Checked = activeLayer.settings.oneMinus;
                form.absolute.Checked = activeLayer.settings.absolute;
                form.FBM.Checked = activeLayer.settings.useFBM;
                form.FBMGain.Value = (decimal)activeLayer.settings.FBMGain;
                form.FBMLacunarity.Value = (decimal)activeLayer.settings.FBMLacunarity;
                form.FBMOctaves.Value = activeLayer.settings.FBMOctaves;
                form.seed.Value = activeLayer.settings.seed;

                if (!form.FBM.Checked)
                {
                    form.FBMGain.Enabled = false;
                    form.FBMLacunarity.Enabled = false;
                    form.FBMOctaves.Enabled = false;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float[] ApplyTiling(float[] data, string tilingMode = "")
        {
            float[] result = new float[data.Length];
            int resolution = (int)Math.Sqrt(data.Length);
            int halfResolution = resolution / 2;

            if (tilingMode == "")
                tilingMode = form.tilingMode.SelectedItem.ToString();

            switch (tilingMode)
            {
                case "Center Edge Blend":
                    // Tile the center texture
                    float[] tiledData = ApplyTiling(data, "Mirrored Edge (Round)");

                    // Constants for blending
                    float blendRadius = halfResolution; // Radius for blending effect

                    Parallel.For(0, resolution, x =>
                    {
                        for (int y = 0; y < resolution; y++)
                        {
                            int currentIndex = y * resolution + x;

                            float edgeDistance = Math.Max(Math.Max((float)x / resolution, (float)y / resolution),
                                Math.Max(1.0f - ((float)x / resolution), 1.0f - ((float)y / resolution)));

                            // Calculate distance from the closest edge
                            int dx = Math.Min(x, resolution - 1 - x);
                            int dy = Math.Min(y, resolution - 1 - y);
                            int minDistance = Math.Min(dx, dy);

                            // Calculate blend factor based on distance from edge
                            float blendFactor = 1.0f - (minDistance / blendRadius);

                            int firstSampleX = x - halfResolution;
                            int firstSampleY = y - halfResolution;

                            if (firstSampleX < 0) firstSampleX += resolution;
                            if (firstSampleY < 0) firstSampleY += resolution;

                            float firstSample = tiledData[firstSampleX * resolution + firstSampleY];

                            int secondSampleX = x - halfResolution;
                            int secondSampleY = y - halfResolution;

                            if (secondSampleX < 0) secondSampleX += resolution;
                            if (secondSampleY < 0) secondSampleY += resolution;

                            float secondSample = tiledData[secondSampleY * resolution + secondSampleX];

                            float blendValue = (firstSample + secondSample) * 0.5f;

                            result[currentIndex] = Lerp(data[currentIndex], blendValue, (float)Math.Pow(blendFactor, 4));
                        }
                    });
                    break;


                case "Scattered Edges":
                    Random random = new Random();

                    Parallel.For(0, resolution, x =>
                    {
                        for (int y = 0; y < resolution; y++)
                        {
                            int currentIndex = y * resolution + x;

                            // Calculate distance from the closest edge
                            int dx = Math.Min(x, resolution - 1 - x);
                            int dy = Math.Min(y, resolution - 1 - y);
                            int minDistance = Math.Min(dx, dy);

                            // Calculate blend factor based on inverse distance from edge
                            float blendFactor = 1.0f - ((float)minDistance / halfResolution);

                            // Randomly sample a pixel from the edges
                            int edgeX = random.Next(resolution);
                            int edgeY;
                            if (random.Next(2) == 0)
                            {
                                edgeY = random.Next(2) == 0 ? 0 : resolution - 1;
                            }
                            else
                            {
                                edgeX = random.Next(2) == 0 ? 0 : resolution - 1;
                                edgeY = random.Next(resolution);
                            }

                            int edgeIndex = edgeY * resolution + edgeX;

                            // Blend the current pixel with the randomly sampled edge pixel
                            result[currentIndex] = Lerp(data[currentIndex], data[edgeIndex], blendFactor);
                        }
                    });
                    break;

                case "Mirrored Edge (Round)":
                    Parallel.For(0, resolution, x =>
                    {
                        for (int y = 0; y < resolution; y++)
                        {
                            int currentIndex = y * resolution + x;

                            // Calculate square distance from the center
                            float dx = x - halfResolution;
                            float dy = y - halfResolution;
                            float squareDistanceFromCenter = dx * dx + dy * dy;

                            // Normalize distance to range [0, 1]
                            float normalizedDistance = squareDistanceFromCenter / (halfResolution * halfResolution);

                            // Clamp normalized distance to range [0, 1]
                            float blendFactor = Math.Min(normalizedDistance, 1.0f);

                            int mx = x < halfResolution ? x : resolution - 1 - x;
                            int my = y < halfResolution ? y : resolution - 1 - y;

                            int oppositeIndex = my * resolution + mx;

                            result[currentIndex] = Lerp(data[currentIndex], data[oppositeIndex], blendFactor);
                        }
                    });
                    break;

                case "Mirrored Edge (Square)":
                    Parallel.For(0, resolution, x =>
                    {
                        for (int y = 0; y < resolution; y++)
                        {
                            int currentIndex = y * resolution + x;

                            float dx = x - halfResolution;
                            float dy = y - halfResolution;
                            float squareDistanceFromCenter = Math.Max(dx * dx, dy * dy);
                            float normalizedDistance = squareDistanceFromCenter / (halfResolution * halfResolution);

                            // Clamp normalized distance to range [0, 1]
                            float blendFactor = Math.Min(normalizedDistance, 1.0f);

                            int mx = x < halfResolution ? x : resolution - 1 - x;
                            int my = y < halfResolution ? y : resolution - 1 - y;

                            int oppositeIndex = my * resolution + mx;

                            result[currentIndex] = Lerp(data[currentIndex], data[oppositeIndex], blendFactor);
                        }
                    });
                    break;

                case "Mirrored":
                    Parallel.For(0, resolution, x =>
                    {
                        for (int y = 0; y < resolution; y++)
                        {
                            int mx = x < resolution / 2 ? x : resolution - 1 - x;
                            int my = y < resolution / 2 ? y : resolution - 1 - y;
                            result[y * resolution + x] = data[(my * 2) * resolution + (mx * 2)];
                        }
                    });
                    break;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float Lerp(float a, float b, float f)
        {
            return a * (1.0f - f) + (b * f);
        }

        /// <summary>
        /// Generates the noise for specified layer and combines it with the supplied array using the layer's settings.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="noiseLayer"></param>
        /// <param name="zStart"></param>
        /// <param name="zCount"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GetLayerNoise(ref float[] data, NoiseLayer noiseLayer, int resolution, int zStart)
        {
            float[] noiseData = new float[resolution * resolution];
            noiseLayer.GetNoise3D(ref noiseData, zStart, resolution);
            noiseLayer.CombineNoise(ref data, noiseData);
        }

        public static float[] GetTotalNoise(int zPos, int resolution)
        {
            float[] noiseTotal = new float[resolution * resolution];

            int index = 0;
            foreach (KeyValuePair<string, NoiseLayer> v in noiseLayers)
            {
                if (form.noiseList.GetItemChecked(index++))
                {
                    GetLayerNoise(ref noiseTotal, v.Value, resolution, zPos);
                }
            }

            if (form.tilingMode.SelectedItem.ToString() != "None")
                noiseTotal = ApplyTiling(noiseTotal);

            return noiseTotal;
        }

        /// <summary>
        /// Updates the preview image for the current noise setup.
        /// </summary>
        private static void UpdatePreview()
        {
            if(noiseLayers.Count == 0)
            {
                form.previewImage.Image = Properties.Resources.PreviewGrid;
                return;
            }

            float depthScrollFactor = (float)form.previewDepthBar.Value / form.previewDepthBar.Maximum;
            int zStart = (int)Math.Round(depthScrollFactor * previewResolution);
            float[] noiseTotal = GetTotalNoise(zStart, previewResolution);

            /*MessageBox.Show(noiseData[256 * 256].ToString());
            for(int x = 0; x < previewResolution; x++)
            {
                for(int y = 0; y < previewResolution; y++)
                {
                    int value = (int)Math.Max(Math.Round(noiseData[(x * previewResolution) + y] * 255), 1);
                    previewImage.SetPixel(x, y, Color.FromArgb(255, value, value, value));
                }
            }*/

            previewImage.SetGrayscaleBitmap(noiseTotal, 0, 0, previewResolution, previewResolution);
            form.previewImage.Image = previewImage;
            form.previewImage.Update();
        }
    }
}
