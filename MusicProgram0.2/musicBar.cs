using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace MusicProgram
{
    public class musicBar : Canvas
    {
        public musicBar() {
            this.Height = 18;
            this.Width = 50;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.timeUnits = (int)this.Width;
        }
        public short volume { get; set; }
        public short waveType { get; set; }
        public short vibrato { get; set; }
        public short vibratoIntensity { get; set; }
        public float hz { get; set; }
        public int timeUnits { get; set; }
        public int startTime { get; set; }

        public void setWaveType(int i) {
            switch (i) {
                case 0: this.Background = Brushes.Red; break;
                case 1: this.Background = Brushes.Aqua; break;
                case 2: this.Background = Brushes.Lime; break;
                case 3: this.Background = Brushes.Yellow; break;
                case 4: this.Background = Brushes.HotPink; break;
                default: break;
            }
            this.waveType = (short)(i+1);
        }
    }
}
