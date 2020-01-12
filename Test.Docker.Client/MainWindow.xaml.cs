using System.IO;
using System.Windows;
using Microsoft.Win32;
using Suyeong.Core.Net.Lib;
using Suyeong.Core.Net.Tcp;
using Suyeong.Core.Util;
using Test.Docker.Variable;
using Test.Docker.Type;

namespace Test.Docker.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClientSimpleAsync client;

        public MainWindow()
        {
            InitializeComponent();

            this.client = new TcpClientSimpleAsync(serverIP: "127.0.0.1", serverPort: Connections.PORT_NUM_SERVER_MAIN);
        }

        async private void BtnAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] data = File.ReadAllBytes(openFileDialog.FileName);
                PacketFile request = new PacketFile(protocol: Protocols.UPLOAD_FILE, desc: Path.GetFileName(openFileDialog.FileName), fileData: data);
                PacketSerialized result = await this.client.Send(sendPacket: request).ConfigureAwait(false) as PacketSerialized;
                FileInfoCollection fileInfos = (FileInfoCollection)StreamUtil.DeserializeObject(result.SerializedData);

                MessageBox.Show("Upload를 완료했습니다.");

                UpdateGrid(fileInfos: fileInfos);
            }
        }

        async private void BtnGetFileInfo_Click(object sender, RoutedEventArgs e)
        {
            PacketValue packet = new PacketValue(protocol: Protocols.GET_FILE_LIST, value: 1);

            PacketSerialized result = await this.client.Send(packet).ConfigureAwait(false) as PacketSerialized;
            FileInfoCollection fileInfos = (FileInfoCollection)StreamUtil.DeserializeObject(result.SerializedData);

            UpdateGrid(fileInfos: fileInfos);
        }

        void UpdateGrid(FileInfoCollection fileInfos)
        {
            this.Dispatcher.Invoke(() =>
            {
                gridFileList.ItemsSource = fileInfos;
            });
        }
    }
}
