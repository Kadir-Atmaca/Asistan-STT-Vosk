using System;
using System.Windows.Forms;
using Vosk;
using NAudio.Wave;

namespace Asistan_ceylin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            spk();
        }
        private WaveInEvent WaveIn;
        private Model model;
        private VoskRecognizer recognizer;

        public void spk()
        {
            var waveIn = new WaveInEvent();
            waveIn.DeviceNumber = 0;
            waveIn.StartRecording();
            spk2();
        }
        public void spk2()
        {
            model = new Model("model");
            recognizer = new VoskRecognizer(model, 16000.0f);
            WaveIn = new WaveInEvent();
            WaveIn.WaveFormat = new WaveFormat(16000, 16, 1);
            WaveIn.DataAvailable += spk3;
            WaveIn.StartRecording();

        }
        private void spk3(object sender, WaveInEventArgs e)
        {
            string spk = "";
            recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded);
            spk = MTEN.MTEN.mten(recognizer.PartialResult());
            label1.Invoke(new Action(() =>
            {
                label1.Text = spk;
            }));
        }
    }
}
