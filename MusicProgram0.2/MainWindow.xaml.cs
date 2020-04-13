using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
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

        float BN8 = 7902.133f;
        float AS8 = 7458.620f;
        float AN8 = 7040.000f;
        float GS8 = 6644.875f;
        float GN8 = 6271.927f;
        float FS8 = 5919.911f;
        float FN8 = 5587.652f;
        float EN8 = 5274.041f;
        float DS8 = 4978.032f;
        float DN8 = 4698.636f;
        float CS8 = 4434.922f;
        float CN8 = 4186.009f;
        float BN7 = 3951.066f;
        float AS7 = 3729.310f;
        float AN7 = 3520.000f;
        float GS7 = 3322.438f;
        float GN7 = 3135.963f;
        float FS7 = 2959.955f;
        float FN7 = 2793.826f;
        float EN7 = 2637.020f;
        float DS7 = 2489.016f;
        float DN7 = 2349.318f;
        float CS7 = 2217.461f;
        float CN7 = 2093.005f;
        float BN6 = 1975.533f;
        float AS6 = 1864.655f;
        float AN6 = 1760.000f;
        float GS6 = 1661.219f;
        float GN6 = 1567.982f;
        float FS6 = 1479.978f;
        float FN6 = 1396.913f;
        float EN6 = 1318.510f;
        float DS6 = 1244.508f;
        float DN6 = 1174.659f;
        float CS6 = 1108.731f;
        float CN6 = 1046.502f;
        float BN5 = 987.7666f;
        float AS5 = 932.3275f;
        float AN5 = 880.0000f;
        float GS5 = 830.6094f;
        float GN5 = 783.9909f;
        float FS5 = 739.9888f;
        float FN5 = 698.4565f;
        float EN5 = 659.2551f;
        float DS5 = 622.2540f;
        float DN5 = 587.3295f;
        float CS5 = 554.3653f;
        float CN5 = 523.2511f;
        float BN4 = 493.8833f;
        float AS4 = 466.1638f;
        float AN4 = 440.0000f;
        float GS4 = 415.3047f;
        float GN4 = 391.9954f;
        float FS4 = 369.9944f;
        float FN4 = 349.2282f;
        float EN4 = 329.6276f;
        float DS4 = 311.1270f;
        float DN4 = 293.6648f;
        float CS4 = 277.1826f;
        float CN4 = 261.6256f;
        float BN3 = 246.9417f;
        float AS3 = 233.0819f;
        float AN3 = 220.0000f;
        float GS3 = 207.6523f;
        float GN3 = 195.9977f;
        float FS3 = 184.9972f;
        float FN3 = 174.6141f;
        float EN3 = 164.8138f;
        float DS3 = 155.5635f;
        float DN3 = 146.8324f;
        float CS3 = 138.5913f;
        float CN3 = 130.8128f;
        float BN2 = 123.4708f;
        float AS2 = 116.5409f;
        float AN2 = 110.0000f;
        float GS2 = 103.8262f;
        float GN2 = 97.99886f;
        float FS2 = 92.49861f;
        float FN2 = 87.30706f;
        float EN2 = 82.40689f;
        float DS2 = 77.78175f;
        float DN2 = 73.41619f;
        float CS2 = 69.29566f;
        float CN2 = 65.40639f;
        float BN1 = 61.73541f;
        float AS1 = 58.27047f;
        float AN1 = 55.00000f;
        float GS1 = 51.91309f;
        float GN1 = 48.99943f;
        float FS1 = 46.24930f;
        float FN1 = 43.65353f;
        float EN1 = 41.20344f;
        float DS1 = 38.89087f;
        float DN1 = 36.70810f;
        float CS1 = 34.64783f;
        float CN1 = 32.70320f;
        float BN0 = 30.86771f;
        float AS0 = 29.13524f;
        float AN0 = 27.50000f;
        float GS0 = 25.95654f;
        float GN0 = 24.49971f;
        float FS0 = 23.12465f;
        float FN0 = 21.82676f;
        float EN0 = 20.60172f;
        float DS0 = 19.44544f;
        float DN0 = 18.35405f;
        float CS0 = 17.32391f;
        float CN0 = 16.35160f;

        static List<musicBar> barList = new List<musicBar>();
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        public musicBar publicBar;
        public Grid publicBarParent;
        public musicBar newestBar;
        public Grid newestBarParent;

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

                List<musicBar> deleteList = new List<musicBar>();
                foreach (musicBar mb in SortedBarList)
                {
                    if (mb.startTime != i) { break; }

                    audioPlayer a = new audioPlayer();
                    var thread = new Thread(
                    () => a.playSound(mb));
                    thread.Start();

                    deleteList.Add(mb);
                }

                foreach (musicBar mb in deleteList)
                {
                    SortedBarList.Remove(mb);
                }
                if (SortedBarList.Count == 0) { break; }

                Thread.Sleep(20);
            }

            // Note,,, wavetype - 1-5,,, length 50 = 1 sek,,, volume 0 - 100,,, vibrato 0 = null vibrato. 12 = holestep vibrato?,,, 
            //vibratoinetnsity = how often the vibrato peaks. must be higher for higher frequencies, the higher- the less frequent.
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

        private void RemoveBar(musicBar mb, Grid parent) {
            parent.Children.Remove(mb);
            barList.Remove(mb);
        }

        private void editBar(object sender, MouseButtonEventArgs e)
        {
            musicBar mb = ((musicBar)sender);
            DependencyObject parentObject = VisualTreeHelper.GetParent((musicBar)sender);
            Grid parent = (Grid)parentObject;
            deleteButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            volumeSlider.Value = mb.volume;
            vibratoSlider.Value = mb.vibrato;
            vibratoIntensitySlider.Value = mb.vibratoIntensity;
            waveCombo.SelectedIndex = mb.waveType-1;
            publicBar = mb;
            publicBarParent = parent;
            if (newestBar != null && newestBar != null) 
            {
                RemoveBar(newestBar, newestBarParent);
            }
            newestBar = null;
            newestBarParent = null;
        }

        
        private void Grid001_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid001); musicBar thisBar = createMusicBar((int)p.X, BN8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid002_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid002); musicBar thisBar = createMusicBar((int)p.X, AS8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid003_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid003); musicBar thisBar = createMusicBar((int)p.X, AN8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid004_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid004); musicBar thisBar = createMusicBar((int)p.X, GS8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid005_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid005); musicBar thisBar = createMusicBar((int)p.X, GN8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid006_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid006); musicBar thisBar = createMusicBar((int)p.X, FS8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid007_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid007); musicBar thisBar = createMusicBar((int)p.X, FN8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid008_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid008); musicBar thisBar = createMusicBar((int)p.X, EN8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid009_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid009); musicBar thisBar = createMusicBar((int)p.X, DS8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid010_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid010); musicBar thisBar = createMusicBar((int)p.X, DN8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid011_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid011); musicBar thisBar = createMusicBar((int)p.X, CS8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid012_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid012); musicBar thisBar = createMusicBar((int)p.X, CN8); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid013_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid013); musicBar thisBar = createMusicBar((int)p.X, BN7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid014_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid014); musicBar thisBar = createMusicBar((int)p.X, AS7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid015_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid015); musicBar thisBar = createMusicBar((int)p.X, AN7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid016_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid016); musicBar thisBar = createMusicBar((int)p.X, GS7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid017_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid017); musicBar thisBar = createMusicBar((int)p.X, GN7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid018_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid018); musicBar thisBar = createMusicBar((int)p.X, FS7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid019_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid019); musicBar thisBar = createMusicBar((int)p.X, FN7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid020_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid020); musicBar thisBar = createMusicBar((int)p.X, EN7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid021_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid021); musicBar thisBar = createMusicBar((int)p.X, DS7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid022_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid022); musicBar thisBar = createMusicBar((int)p.X, DN7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid023_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid023); musicBar thisBar = createMusicBar((int)p.X, CS7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid024_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid024); musicBar thisBar = createMusicBar((int)p.X, CN7); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid025_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid025); musicBar thisBar = createMusicBar((int)p.X, BN6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid026_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid026); musicBar thisBar = createMusicBar((int)p.X, AS6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid027_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid027); musicBar thisBar = createMusicBar((int)p.X, AN6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid028_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid028); musicBar thisBar = createMusicBar((int)p.X, GS6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid029_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid029); musicBar thisBar = createMusicBar((int)p.X, GN6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid030_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid030); musicBar thisBar = createMusicBar((int)p.X, FS6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid031_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid031); musicBar thisBar = createMusicBar((int)p.X, FN6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid032_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid032); musicBar thisBar = createMusicBar((int)p.X, EN6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid033_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid033); musicBar thisBar = createMusicBar((int)p.X, DS6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid034_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid034); musicBar thisBar = createMusicBar((int)p.X, DN6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid035_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid035); musicBar thisBar = createMusicBar((int)p.X, CS6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid036_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid036); musicBar thisBar = createMusicBar((int)p.X, CN6); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid037_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid037); musicBar thisBar = createMusicBar((int)p.X, BN5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid038_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid038); musicBar thisBar = createMusicBar((int)p.X, AS5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid039_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid039); musicBar thisBar = createMusicBar((int)p.X, AN5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid040_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid040); musicBar thisBar = createMusicBar((int)p.X, GS5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid041_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid041); musicBar thisBar = createMusicBar((int)p.X, GN5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid042_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid042); musicBar thisBar = createMusicBar((int)p.X, FS5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid043_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid043); musicBar thisBar = createMusicBar((int)p.X, FN5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid044_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid044); musicBar thisBar = createMusicBar((int)p.X, EN5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid045_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid045); musicBar thisBar = createMusicBar((int)p.X, DS5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid046_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid046); musicBar thisBar = createMusicBar((int)p.X, DN5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid047_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid047); musicBar thisBar = createMusicBar((int)p.X, CS5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid048_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid048); musicBar thisBar = createMusicBar((int)p.X, CN5); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid049_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid049); musicBar thisBar = createMusicBar((int)p.X, BN4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid050_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid050); musicBar thisBar = createMusicBar((int)p.X, AS4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid051_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid051); musicBar thisBar = createMusicBar((int)p.X, AN4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid052_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid052); musicBar thisBar = createMusicBar((int)p.X, GS4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid053_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid053); musicBar thisBar = createMusicBar((int)p.X, GN4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid054_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid054); musicBar thisBar = createMusicBar((int)p.X, FS4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid055_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid055); musicBar thisBar = createMusicBar((int)p.X, FN4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid056_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid056); musicBar thisBar = createMusicBar((int)p.X, EN4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid057_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid057); musicBar thisBar = createMusicBar((int)p.X, DS4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid058_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid058); musicBar thisBar = createMusicBar((int)p.X, DN4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid059_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid059); musicBar thisBar = createMusicBar((int)p.X, CS4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid060_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid060); musicBar thisBar = createMusicBar((int)p.X, CN4); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid061_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid061); musicBar thisBar = createMusicBar((int)p.X, BN3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid062_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid062); musicBar thisBar = createMusicBar((int)p.X, AS3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid063_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid063); musicBar thisBar = createMusicBar((int)p.X, AN3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid064_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid064); musicBar thisBar = createMusicBar((int)p.X, GS3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid065_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid065); musicBar thisBar = createMusicBar((int)p.X, GN3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid066_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid066); musicBar thisBar = createMusicBar((int)p.X, FS3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid067_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid067); musicBar thisBar = createMusicBar((int)p.X, FN3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid068_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid068); musicBar thisBar = createMusicBar((int)p.X, EN3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid069_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid069); musicBar thisBar = createMusicBar((int)p.X, DS3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid070_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid070); musicBar thisBar = createMusicBar((int)p.X, DN3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid071_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid071); musicBar thisBar = createMusicBar((int)p.X, CS3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid072_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid072); musicBar thisBar = createMusicBar((int)p.X, CN3); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid073_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid073); musicBar thisBar = createMusicBar((int)p.X, BN2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid074_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid074); musicBar thisBar = createMusicBar((int)p.X, AS2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid075_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid075); musicBar thisBar = createMusicBar((int)p.X, AN2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid076_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid076); musicBar thisBar = createMusicBar((int)p.X, GS2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid077_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid077); musicBar thisBar = createMusicBar((int)p.X, GN2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid078_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid078); musicBar thisBar = createMusicBar((int)p.X, FS2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid079_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid079); musicBar thisBar = createMusicBar((int)p.X, FN2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid080_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid080); musicBar thisBar = createMusicBar((int)p.X, EN2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid081_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid081); musicBar thisBar = createMusicBar((int)p.X, DS2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid082_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid082); musicBar thisBar = createMusicBar((int)p.X, DN2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid083_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid083); musicBar thisBar = createMusicBar((int)p.X, CS2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid084_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid084); musicBar thisBar = createMusicBar((int)p.X, CN2); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid085_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid085); musicBar thisBar = createMusicBar((int)p.X, BN1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid086_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid086); musicBar thisBar = createMusicBar((int)p.X, AS1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid087_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid087); musicBar thisBar = createMusicBar((int)p.X, AN1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid088_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid088); musicBar thisBar = createMusicBar((int)p.X, GS1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid089_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid089); musicBar thisBar = createMusicBar((int)p.X, GN1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid090_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid090); musicBar thisBar = createMusicBar((int)p.X, FS1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid091_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid091); musicBar thisBar = createMusicBar((int)p.X, FN1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid092_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid092); musicBar thisBar = createMusicBar((int)p.X, EN1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid093_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid093); musicBar thisBar = createMusicBar((int)p.X, DS1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid094_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid094); musicBar thisBar = createMusicBar((int)p.X, DN1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid095_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid095); musicBar thisBar = createMusicBar((int)p.X, CS1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid096_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid096); musicBar thisBar = createMusicBar((int)p.X, CN1); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid097_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid097); musicBar thisBar = createMusicBar((int)p.X, BN0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid098_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid098); musicBar thisBar = createMusicBar((int)p.X, AS0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid099_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid099); musicBar thisBar = createMusicBar((int)p.X, AN0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid100_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid100); musicBar thisBar = createMusicBar((int)p.X, GS0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid101_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid101); musicBar thisBar = createMusicBar((int)p.X, GN0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid102_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid102); musicBar thisBar = createMusicBar((int)p.X, FS0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid103_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid103); musicBar thisBar = createMusicBar((int)p.X, FN0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid104_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid104); musicBar thisBar = createMusicBar((int)p.X, EN0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid105_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid105); musicBar thisBar = createMusicBar((int)p.X, DS0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid106_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid106); musicBar thisBar = createMusicBar((int)p.X, DN0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid107_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid107); musicBar thisBar = createMusicBar((int)p.X, CS0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }
        private void Grid108_PreviewMouseDown(object sender, MouseButtonEventArgs e) { Point p = Mouse.GetPosition(Grid108); musicBar thisBar = createMusicBar((int)p.X, CN0); ((Grid)sender).Children.Add(thisBar); newestBar = thisBar; newestBarParent = (Grid)sender; thisBar.PreviewMouseDown += new MouseButtonEventHandler(editBar); barList.Add(thisBar); }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            publicBar.volume = (short)volumeSlider.Value;
            publicBar.vibrato = (short)vibratoSlider.Value;
            publicBar.vibratoIntensity = (short)vibratoIntensitySlider.Value;
            publicBar.setWaveType(waveCombo.SelectedIndex);
            deleteButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            RemoveBar(publicBar, publicBarParent);
            deleteButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;
        }
    }
}
