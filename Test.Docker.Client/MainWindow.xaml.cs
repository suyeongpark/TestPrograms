using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
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

            this.client = new TcpClientSimpleAsync(serverIP: "localhost", serverPort: Connection.PORT_NUM_SERVER_LOGIN);
        }

        async private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(ColumnName.ID, tbAddUserID.Text);
            dic.Add(ColumnName.PASSWORD, tbAddUserPassword.Text);
            dic.Add(ColumnName.NAME, tbAddUserName.Text);

            PacketJson packet = new PacketJson(protocol: Protocol.CREATE_USER, json: JsonConvert.SerializeObject(dic));

            tbAddUserID.Text = string.Empty;
            tbAddUserPassword.Text = string.Empty;
            tbAddUserName.Text = string.Empty;

            PacketValue result = await this.client.Send(packet).ConfigureAwait(false) as PacketValue;
        }

        async private void BtnGetUserInfo_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(ColumnName.ID, tbGetUserID.Text);
            dic.Add(ColumnName.PASSWORD, tbGetUserPassword.Text);

            PacketJson packet = new PacketJson(protocol: Protocol.GET_USER, json: JsonConvert.SerializeObject(dic));

            PacketSerialized result = await this.client.Send(packet).ConfigureAwait(false) as PacketSerialized;
            UserInfo userInfo = (UserInfo)StreamUtil.DeserializeObject(result.SerializedData);

            this.Dispatcher.Invoke(() =>
            {
                tbGetID.Text = userInfo.ID.ToString();
                tbGetUserName.Text = userInfo.Name;
            });
        }
    }
}
