using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using NAudio.Wave;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Threading;
using System.Text;

namespace Streaming_App_Network_Project
{
    public partial class Server_Form : Form
    {
        private UdpClient udpAudioServer;
        private WaveInEvent waveIn;
        private IPEndPoint clientAudioEndPoint;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private UdpClient udpServer;
        private IPEndPoint clientEndPoint;
        private Thread connectionListenerThread;
        public Server_Form()
        {
            InitializeComponent();

            IPAddress localAddress = GetLocalIPAddress(); // Lấy địa chỉ IP
            udpServer = new UdpClient(); // UDP cho video
            udpAudioServer = new UdpClient(); // UDP cho âm thanh

            clientEndPoint = new IPEndPoint(localAddress, 5000); // Điểm cuối cho video
            clientAudioEndPoint = new IPEndPoint(localAddress, 5001); // Điểm cuối cho âm thanh

            // Khởi động thread lắng nghe kết nối
            connectionListenerThread = new Thread(ListenForConnections);
            connectionListenerThread.Start();

            Console.WriteLine(localAddress.ToString());
        }

        private void start_streaming_btn_Click(object sender, EventArgs e)
        {
            // Khởi động microphone
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.StartRecording();

            // Khởi động camera
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("Không tìm thấy camera.");
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] audioData = e.Buffer;
            udpAudioServer.Send(audioData, audioData.Length, clientAudioEndPoint);
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs e)
        {
            Bitmap bitmap = (Bitmap)e.Frame.Clone();

            // Chuyển đổi hình ảnh thành byte[]
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.ToArray();

                // Chia nhỏ dữ liệu (để tránh lỗi do kích thước gói lớn)
                const int maxUdpPacketSize = 65000;
                for (int offset = 0; offset < data.Length; offset += maxUdpPacketSize)
                {
                    int size = Math.Min(maxUdpPacketSize, data.Length - offset);
                    byte[] packet = new byte[size];
                    Array.Copy(data, offset, packet, 0, size);
                    udpServer.Send(packet, packet.Length, clientEndPoint);
                }
            }

            // Hiển thị video lên PictureBox
            streaming_screen.Image = bitmap;
        }

        private IPAddress GetLocalIPAddress()
        {
            var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var networkInterface in networkInterfaces)
            {
                if (networkInterface.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211 &&
                    networkInterface.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    var ipProperties = networkInterface.GetIPProperties();
                    foreach (var ip in ipProperties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.Address;
                        }
                    }
                }
            }
            throw new Exception("Không tìm thấy địa chỉ IP Wi-Fi.");
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dừng camera
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            // Dừng microphone
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
            }

            //Dừng lắng nghe kết nối
            connectionListenerThread?.Abort();
        }

        private void ListenForConnections()
        {
            UdpClient connectionListener = new UdpClient(5002); // Cổng dành riêng cho kiểm tra kết nối
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                try
                {
                    // Nhận dữ liệu từ client
                    byte[] receivedData = connectionListener.Receive(ref remoteEP);
                    string message = Encoding.ASCII.GetString(receivedData);

                    if (message == "Ping")
                    {
                        // Phản hồi lại "Pong" để xác nhận kết nối
                        byte[] responseData = Encoding.ASCII.GetBytes("Pong");
                        connectionListener.Send(responseData, responseData.Length, remoteEP);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lắng nghe kết nối: {ex.Message}");
                }
            }
        }

    }
}
