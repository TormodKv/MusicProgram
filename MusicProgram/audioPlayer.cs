using System;
using System.IO;
using System.Media;

namespace MusicProgram
{
    public partial class MainWindow
    {
        public class audioPlayer
        {
            public void playSound(musicBar mb)
            {
                float frequency = mb.hz;
                int type = mb.waveType;
                int multi = mb.timeUnits;
                short volume = mb.volume;
                short vibrato = mb.vibrato;
                short vibratoIntensity = mb.vibratoIntensity;
                int divi = 50;
                int amplitude = ((short.MaxValue / 100) * volume);
                short[] wave = new short[(SAMPLE_RATE * multi) / divi];
                byte[] binarywave = new byte[(SAMPLE_RATE * multi) / divi * sizeof(short)];

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
                float step = frequency * vibrato / ((100 * vibratoIntensity) + 1);

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
                float step = frequency * vibrato / ((100 * vibratoIntensity) + 1);

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
                float step = frequency * vibrato / ((100 * vibratoIntensity) + 1);
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
            private short[] createSineToothReversed(short[] wave, int multi, int divi, int amplitude, float frequency, short vibrato, short vibratoIntensity)
            {
                float highestFreq = ((frequency * vibrato) / 100) + frequency;
                float lowestFreq = ((frequency * vibrato) / 100) + frequency;
                float step = frequency * vibrato / ((100 * vibratoIntensity) + 1);
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
        }

    }
}
