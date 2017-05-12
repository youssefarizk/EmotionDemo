using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
  

using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace EmotionDemo
{
    public partial class Form1 : Form
    {
        int counter = 0;
        const string APIKey = "e0d17a705f3647f9b451cebc8bcfb8a2";

        EmotionServiceClient emotionClient = new EmotionServiceClient(APIKey);
        Emotion[] emotionResult;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureButton_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private async void emotionButton_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Stream imageFileStream = File.OpenRead(pictureBox1.ImageLocation);
                emotionResult = await emotionClient.RecognizeAsync(imageFileStream);

                if (emotionResult != null)
                {
                    var score = emotionResult[0].Scores;
                    counter++;
                    output.Text = "The emotions for the first face detected are:" +
                        "\r\n Anger: " + score.Anger +
                        "\r\n Contempt: " + score.Contempt +
                        "\r\n Disgust: " + score.Disgust +
                        "\r\n Fear: " + score.Fear +
                        "\r\n Happiness: " + score.Happiness +
                        "\r\n Neutral: " + score.Neutral +
                        "\r\n Sadness: " + score.Sadness +
                        "\r\n Surprise: " + score.Surprise
                        + "\n This is the " +counter + "th iteration"
                        ;
                }
            }
            catch
            {
                output.Text += "Error accessing the Picture at location: " + pictureBox1.ImageLocation;
            }
        }
    }
}
