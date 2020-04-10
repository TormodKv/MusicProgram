using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        float B8 = 7902.133f;
        float AS8 = 7458.620f;
        float A8 = 7040.000f;
        float GS8 = 6644.875f;
        float G8 = 6271.927f;
        float FS8 = 5919.911f;
        float F8 = 5587.652f;
        float E8 = 5274.041f;
        float DS8 = 4978.032f;
        float D8 = 4698.636f;
        float CS8 = 4434.922f;
        float C8 = 4186.009f;
        float B7 = 3951.066f;
        float AS7 = 3729.310f;
        float A7 = 3520.000f;
        float GS7 = 3322.438f;
        float G7 = 3135.963f;
        float FS7 = 2959.955f;
        float F7 = 2793.826f;
        float E7 = 2637.020f;
        float DS7 = 2489.016f;
        float D7 = 2349.318f;
        float CS7 = 2217.461f;
        float C7 = 2093.005f;
        float B6 = 1975.533f;
        float AS6 = 1864.655f;
        float A6 = 1760.000f;
        float GS6 = 1661.219f;
        float G6 = 1567.982f;
        float FS6 = 1479.978f;
        float F6 = 1396.913f;
        float E6 = 1318.510f;
        float DS6 = 1244.508f;
        float D6 = 1174.659f;
        float CS6 = 1108.731f;
        float C6 = 1046.502f;
        float B5 = 987.7666f;
        float AS5 = 932.3275f;
        float A5 = 880.0000f;
        float GS5 = 830.6094f;
        float G5 = 783.9909f;
        float FS5 = 739.9888f;
        float F5 = 698.4565f;
        float E5 = 659.2551f;
        float DS5 = 622.2540f;
        float D5 = 587.3295f;
        float CS5 = 554.3653f;
        float C5 = 523.2511f;
        float B4 = 493.8833f;
        float AS4 = 466.1638f;
        float A4 = 440.0000f;
        float GS4 = 415.3047f;
        float G4 = 391.9954f;
        float FS4 = 369.9944f;
        float F4 = 349.2282f;
        float E4 = 329.6276f;
        float DS4 = 311.1270f;
        float D4 = 293.6648f;
        float CS4 = 277.1826f;
        float C4 = 261.6256f;
        float B3 = 246.9417f;
        float AS3 = 233.0819f;
        float A3 = 220.0000f;
        float GS3 = 207.6523f;
        float G3 = 195.9977f;
        float FS3 = 184.9972f;
        float F3 = 174.6141f;
        float E3 = 164.8138f;
        float DS3 = 155.5635f;
        float D3 = 146.8324f;
        float CS3 = 138.5913f;
        float C3 = 130.8128f;
        float B2 = 123.4708f;
        float AS2 = 116.5409f;
        float A2 = 110.0000f;
        float GS2 = 103.8262f;
        float G2 = 97.99886f;
        float FS2 = 92.49861f;
        float F2 = 87.30706f;
        float E2 = 82.40689f;
        float DS2 = 77.78175f;
        float D2 = 73.41619f;
        float CS2 = 69.29566f;
        float C2 = 65.40639f;
        float B1 = 61.73541f;
        float AS1 = 58.27047f;
        float A1 = 55.00000f;
        float GS1 = 51.91309f;
        float G1 = 48.99943f;
        float FS1 = 46.24930f;
        float F1 = 43.65353f;
        float E1 = 41.20344f;
        float DS1 = 38.89087f;
        float D1 = 36.70810f;
        float CS1 = 34.64783f;
        float C1 = 32.70320f;
        float B0 = 30.86771f;
        float AS0 = 29.13524f;
        float A0 = 27.50000f;
        float GS0 = 25.95654f;
        float G0 = 24.49971f;
        float FS0 = 23.12465f;
        float F0 = 21.82676f;
        float E0 = 20.60172f;
        float DS0 = 19.44544f;
        float D0 = 18.35405f;
        float CS0 = 17.32391f;
        float C0 = 16.35160f;

        MainWindow _main;
        static List<musicBar> barList = new List<musicBar>();
        private const int SAMPLE_RATE = 44100;
    private const short BITS_PER_SAMPLE = 16;

        public MainWindow()
        {
            MainWindow _main = this;
            InitializeComponent();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            List<musicBar> SortedBarList = barList.OrderBy(o => o.startTime).ToList();

            for (int i = 0; i < 12000; i++) 
            {
                foreach (musicBar mb in SortedBarList) 
                {
                    if (mb.startTime != i) { break; }

                    Thread t = new Thread(new ParameterizedThreadStart(this.playSound));
                    t.Start(mb.hz, mb.waveType, mb.timeUnits, mb.volume, mb.vibrato, mb.vibratoIntensity);


                }
                Thread.Sleep(20);
            }

            // Note,,, wavetype - 1-5,,, length 50 = 1 sek,,, volume 0 - 100,,, vibrato 0 = null vibrato. 12 = holestep vibrato,,, 
            //vibratoinetnsity = how often the vibrato peaks. must be higher for higher frequencies, the higher- the less frequent.
        }
        public void playSound(float hz, int type, int timeUnits, short volume, short vibrato, short vibratoIntensity)
        /// <summary>
        /// Frequency, Type of wave, timeUnits, volume percentage
        /// </summary>
        {
            int multi = timeUnits;
            int divi = 50;
            int amplitude = (int)((short.MaxValue / 100) * volume);
            short[] wave = new short[(SAMPLE_RATE * multi) / divi];
            byte[] binarywave = new byte[(SAMPLE_RATE * multi) / divi * sizeof(short)];
            float frequency = hz;

            switch (type)
            {
                case 1: // Sine
                    wave = createSine(wave, multi, divi, amplitude, frequency, vibrato, vibratoIntensity);
                    break;
                case 2: // Square 
                    wave = createSquare(wave, multi, divi, amplitude, frequency, vibrato, vibratoIntensity);
                    break;
                case 3: // sinetooth
                    wave = createSineTooth(wave, multi, divi, amplitude, frequency, vibrato, vibratoIntensity);
                    break;
                case 4: // inverted sinetooth
                    wave = createSineToothReversed(wave, multi, divi, amplitude, frequency, vibrato, vibratoIntensity);
                    break;
                case 5: //noise
                    wave = createNoise(wave, multi, divi, amplitude, frequency);
                    break;
                case 6: break;
                case 7: break;

                default: break;
            }

            Buffer.BlockCopy(wave, 0, binarywave, 0, wave.Length * sizeof(short));
            using (MemoryStream memorystream = new MemoryStream())
            using (BinaryWriter binarywriter = new BinaryWriter(memorystream))
            {
                short blockAlign = BITS_PER_SAMPLE / 8;
                int subChunkTwoSize = ((SAMPLE_RATE * multi) / divi) * blockAlign;
                binarywriter.Write(new[] { 'R', 'I', 'F', 'F' });
                binarywriter.Write(36 + subChunkTwoSize);
                binarywriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
                binarywriter.Write(16);
                binarywriter.Write((short)1);
                binarywriter.Write((short)1);
                binarywriter.Write(SAMPLE_RATE);
                binarywriter.Write(SAMPLE_RATE * blockAlign);
                binarywriter.Write(blockAlign);
                binarywriter.Write(BITS_PER_SAMPLE);
                binarywriter.Write(new[] { 'd', 'a', 't', 'a' });
                binarywriter.Write(subChunkTwoSize);
                binarywriter.Write(binarywave);
                memorystream.Position = 0;
                new SoundPlayer(memorystream).Play();
            }
        }
        private short[] createSine(short[] wave, int multi, int divi, int amplitude, float frequency, short vibrato, short vibratoIntensity) 
        {
            float highestFreq = (((frequency * vibrato) / 100) + frequency);
            float lowestFreq = (((frequency * vibrato) / 100) + frequency);
            float step = frequency * vibrato / ((100 * vibratoIntensity)+1);

            for (int i = 0; i < (SAMPLE_RATE * multi) / divi; i++)
            {
                wave[i] = Convert.ToInt16(amplitude * (Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i)));

                if (frequency > highestFreq || frequency < lowestFreq) { step = step * -1; }
                frequency = frequency + step;
            }
            return wave;
        }
        private short[] createSquare(short[] wave, int multi, int divi, int amplitude, float frequency, short vibrato, short vibratoIntensity)
        {
            float highestFreq = ((frequency * vibrato) / 100) + frequency;
            float lowestFreq = ((frequency * vibrato) / 100) + frequency;
            float step = frequency * vibrato / (100 * vibratoIntensity);

            for (int i = 0; i < (SAMPLE_RATE * multi) / divi; i++)
            {
                wave[i] = Convert.ToInt16(amplitude * Math.Sign(Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i)));

                if (frequency > highestFreq || frequency < lowestFreq) { step = step * -1; }
                frequency = frequency + step;
            }
            return wave;
        }
        private short[] createSineTooth(short[] wave, int multi, int divi, int amplitude, float frequency, short vibrato, short vibratoIntensity)
        {
            float highestFreq = ((frequency * vibrato) / 100) + frequency;
            float lowestFreq = ((frequency * vibrato) / 100) + frequency;
            float step = frequency * vibrato / (100 * vibratoIntensity);
            int invert = 1;

            for (int i = 0; i < (SAMPLE_RATE * multi) / divi; i++)
            {
                wave[i] = Convert.ToInt16((amplitude * (Math.Sin(((Math.PI * frequency) / SAMPLE_RATE) * i))) * invert);
                if (Convert.ToInt16((amplitude * (Math.Sin(((Math.PI * frequency) / SAMPLE_RATE) * (i+1)))) * invert) <= Convert.ToInt16((amplitude * (Math.Sin(((Math.PI * frequency) / SAMPLE_RATE) * (i+2)))) * invert)) { invert = invert * -1; }
                if (frequency > highestFreq || frequency < lowestFreq) { step = step * -1; }
                frequency = frequency + step;
            }

            return wave;
        }
        private short[] createSineToothReversed(short[] wave, int multi, int divi, int amplitude, float frequency, short vibrato, short vibratoIntensity)
        {
            float highestFreq = ((frequency * vibrato) / 100) + frequency;
            float lowestFreq = ((frequency * vibrato) / 100) + frequency;
            float step = frequency * vibrato / (100 * vibratoIntensity);
            int invert = 1;

            for (int i = 0; i < (SAMPLE_RATE * multi) / divi; i++)
            {
                wave[i] = Convert.ToInt16((amplitude * (Math.Sin(((Math.PI * frequency) / SAMPLE_RATE) * i))) * invert);
                if (Convert.ToInt16((amplitude * (Math.Sin(((Math.PI * frequency) / SAMPLE_RATE) * (i + 1)))) * invert) <= Convert.ToInt16((amplitude * (Math.Sin(((Math.PI * frequency) / SAMPLE_RATE) * (i + 2)))) * invert)) { invert = invert * -1; }
                if (frequency > highestFreq || frequency < lowestFreq) { step = step * -1; }
                frequency = frequency + step;
            }

            return wave;
        }
        private short[] createNoise(short[] wave, int multi, int divi, int amplitude, float frequency)
        {
            Random rnd = new Random();
            short randomValue = 0;
            for (int i = 0; i < (SAMPLE_RATE * multi) / divi; i++)
            {
                randomValue = Convert.ToInt16(rnd.Next(-amplitude, amplitude));
                wave[i] = randomValue;
            }

            return wave;
        }

        private musicBar createMusicBar(int x, float hz) 
        {
            musicBar thisBar = new musicBar();
            thisBar.Margin = new Thickness(x, 0, 0, 0);
            thisBar.startTime = x;
            thisBar.hz = hz;
            thisBar.vibrato = (short)vibratoSlider.Value;
            thisBar.setWaveType(waveCombo.SelectedIndex);
            thisBar.vibratoIntensity = (short)vibratoIntensitySlider.Value;
            thisBar.volume = (short)volumeSlider.Value;
            return thisBar;
        }

        private void Grid001_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(Grid001);
            musicBar thisBar = createMusicBar((int)p.X, A4);
            ((Grid)sender).Children.Add(thisBar);
            barList.Add(thisBar);
        }

    }
}
