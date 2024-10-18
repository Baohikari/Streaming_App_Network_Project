using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Threading;
using System.IO;
using System.Net;
namespace Streaming_App_Network_Project
{
    public partial class Client_Form : Form
    {
        private UdpClient udpAudioClient;
        private UdpClient UdpClient;
        private BufferedWaveProvider waveProvider;
        private WaveOut waveOut;
        private bool isConnected = false;
        private bool isStreaming = false;
        private Thread videoThread;
        private Thread audioThread;

        public Client_Form()
        {
            InitializeComponent();
            UdpClient = new UdpClient(5000); //Video port
            udpAudioClient = new UdpClient(5001); //Audio port
        }


        private void start_btn_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Bạn cần kết nối tới server trước.");
                return;
            }

            if (isStreaming)
            {
                MessageBox.Show("Stream đã đang chạy.");
                return;
            }

            isStreaming = true;

            // Khởi tạo lại đối tượng WaveOut cho âm thanh mới
            waveProvider = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
            waveOut = new WaveOut();
            waveOut.Init(waveProvider);
            waveOut.Play();

            //Bắt đầu nhận video
            videoThread = new Thread(ReceiveVideo);
            videoThread.Start();

            //Bắt đầu nhận audio
            audioThread = new Thread(ReceiveAudio);
            audioThread.Start();
        }

        private void ReceiveVideo()
        {
            try
            {
                while (isStreaming)
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedData = UdpClient.Receive(ref remoteEP);

                    using (MemoryStream ms = new MemoryStream(receivedData))
                    {
                        Bitmap bitmap = new Bitmap(ms);
                        streaming_screen.Image = bitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhận video: {ex.Message}");
            }
        }

        private void ReceiveAudio()
        {
            try
            {
                waveProvider = new BufferedWaveProvider(new WaveFormat(8000, 16, 1)); // Định dạng audio
                waveOut = new WaveOut();
                waveOut.Init(waveProvider);
                waveOut.Play();

                while (isStreaming)
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedData = udpAudioClient.Receive(ref remoteEP);

                    waveProvider.AddSamples(receivedData, 0, receivedData.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhận audio: {ex.Message}");
            }
        }
        private void stop_btn_Click(object sender, EventArgs e)
        {
            if (!isStreaming)
            {
                MessageBox.Show("Stream đã dừng.");
                return;
            }

            isStreaming = false;

            // Đợi các thread hoàn thành
            videoThread?.Join();
            audioThread?.Join();

            waveOut?.Stop();
            waveOut?.Dispose();

            MessageBox.Show("Dừng stream.");
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            string serverIP = serverIpTextBox.Text.Trim();
            if (string.IsNullOrEmpty(serverIP))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ IP của server.");
                return;
            }

            // Kiểm tra định dạng địa chỉ IP
            if (!IPAddress.TryParse(serverIP, out IPAddress ipAddress))
            {
                MessageBox.Show("Địa chỉ IP không hợp lệ. Vui lòng nhập đúng định dạng.");
                return;
            }

            try
            {
                // Kết nối với server cho video nếu chưa kết nối
                if (!isConnected)
                {
                    UdpClient.Connect(serverIP, 5000); // Kết nối cho video
                    udpAudioClient.Connect(serverIP, 5001); // Kết nối cho audio

                    isConnected = true;
                    MessageBox.Show("Kết nối thành công tới server.");
                }
                else
                {
                    MessageBox.Show("Đã kết nối tới server.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối tới server: {ex.Message}");
                isConnected = false; // Cập nhật lại trạng thái nếu không thể kết nối
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float volume = trackBar1.Value / 100.0f;
            waveOut.Volume = volume;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}