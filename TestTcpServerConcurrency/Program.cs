using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Suyeong.Lib.Net.Lib;
using Suyeong.Lib.Net.Tcp;
using Newtonsoft.Json;

namespace TestTcpServerConcurrency
{
    class Program
    {
        const int SERVER_PORT = 10003;
        //static TcpListenerConcurrencySync listener;
        static TcpListenerConcurrencyAsync listener;

        //static TcpListener listener;
        //static HandlerDicGroup handlerDicGroup;

        static void Main(string[] args)
        {
            //listener = new TcpListenerConcurrencySync(portNum: SERVER_PORT, userEnterCallback: UserEnter, userExitCallback: UserExit, responseCallbak: Receive);
            //listener.Start();

            Task task = Task.Run(async () =>
            {
                listener = new TcpListenerConcurrencyAsync(portNum: SERVER_PORT, userEnterCallbackAsync: UserEnter, userExitCallbackAsync: UserExit, responseCallbakAsync: Receive);
                await listener.StartAsync();
            });

            task.Wait();



            //listener = new TcpListener(new IPEndPoint(address: IPAddress.Any, port: SERVER_PORT));
            //handlerDicGroup = new HandlerDicGroup();

            //listener.Start();

            //TcpClient client;
            //NetworkStream stream;
            //Handler handler;
            //PacketValue connectPacket;
            //int nbytes, receiveDataLength;
            //string stageID, userID;
            //byte[] receiveHeader, receiveData, decompressData;

            //Task task = Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
            //            stream = client.GetStream();

            //            // 사용자가 접속하면 우선 사용자로부터 입장할 채널 정보와 사용자 정보를 받는다. 
            //            // 1. 요청 헤더를 받는다.
            //            receiveHeader = new byte[Consts.SIZE_HEADER];
            //            nbytes = await stream.ReadAsync(buffer: receiveHeader, offset: 0, count: receiveHeader.Length);

            //            // 2. 요청 데이터를 받는다.
            //            receiveDataLength = BitConverter.ToInt32(value: receiveHeader, startIndex: 0);
            //            receiveData = await TcpUtil.ReceiveDataAsync(networkStream: stream, dataLength: receiveDataLength);

            //            await stream.FlushAsync();

            //            // 3. 받은 요청은 압축되어 있으므로 푼다.
            //            decompressData = await NetUtil.DecompressAsync(data: receiveData);
            //            connectPacket = NetUtil.DeserializeObject(data: decompressData) as PacketValue;

            //            // protocol에 입장하려는 stage의 id를 넣고, value에 user id를 넣는다.
            //            stageID = connectPacket.Protocol;
            //            userID = connectPacket.Value.ToString();

            //            // 사용자 정보를 이용해서 handler를 추가한다.

            //            // 사용자 정보를 이용해서 handler를 만든다.
            //            handler = new Handler(stageID: stageID, userID: userID, client: client);
            //            handler.Disconnect += DisconnectAsync;
            //            handler.Receive += ReceiveAsync;

            //            AddStage(handler: handler, stageID: stageID, userID: userID);

            //            // hander를 시작한다.
            //            await handler.StartAsync();

            //            // 사용자가 입장한 정보를 broadcast 한다.
            //            IPacket sendPacket = await UserEnter(stageID, userID);
            //            await BroadcastToStageAsync(stageID: stageID, sendPacket: sendPacket);
            //        }
            //        catch (SocketException ex)
            //        {
            //            Console.WriteLine(ex);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex);
            //        }
            //    }
            //});

            //task.Wait();

        }

        //async static public Task DisconnectAsync(string stageID, string userID)
        //{
        //    HandlerDic handlerDic;
        //    Handler handler;

        //    if (handlerDicGroup.TryGetValue(stageID, out handlerDic))
        //    {
        //        if (handlerDic.TryGetValue(userID, out handler))
        //        {
        //            handler.Dispose();
        //        }

        //        handlerDic.Remove(userID);
        //    }

        //    IPacket sendPacket = await UserExit(stageID, userID);
        //    await BroadcastToStageAsync(stageID: stageID, sendPacket: sendPacket);
        //}

        //async static public Task MoveStageAsync(string oldStageID, string newStageID, string userID)
        //{
        //    // 1. 기존 stage에서 제거
        //    Handler handler = RemoveStage(stageID: oldStageID, userID: userID);
        //    handler.SetStageID(stageID: newStageID);

        //    // 2. 기존 stage에 퇴장 알림
        //    IPacket exitPacket = await UserExit(oldStageID, userID);
        //    await BroadcastToStageAsync(stageID: oldStageID, sendPacket: exitPacket);

        //    // 3. 새로운 stage로 입장
        //    AddStage(handler: handler, stageID: newStageID, userID: userID);

        //    // 4. 새로운 stage에 입장 알림
        //    IPacket enterPacket = await UserEnter(newStageID, userID);
        //    await BroadcastToStageAsync(stageID: newStageID, sendPacket: enterPacket);
        //}

        //async static public Task BroadcastToServerAsync(IPacket sendPacket)
        //{
        //    foreach (KeyValuePair<string, HandlerDic> kvp in handlerDicGroup)
        //    {
        //        foreach (KeyValuePair<string, Handler> kvp2 in kvp.Value)
        //        {
        //            await kvp2.Value.SendAsync(packet: sendPacket);
        //        }
        //    }
        //}

        //static void AddStage(Handler handler, string stageID, string userID)
        //{
        //    HandlerDic handlerDic;

        //    if (handlerDicGroup.TryGetValue(stageID, out handlerDic))
        //    {
        //        handlerDic.Add(userID, handler);

        //        handlerDicGroup[stageID] = handlerDic;
        //    }
        //    else
        //    {
        //        handlerDic = new HandlerDic();
        //        handlerDic.Add(userID, handler);

        //        handlerDicGroup.Add(stageID, handlerDic);
        //    }
        //}

        //static Handler RemoveStage(string stageID, string userID)
        //{
        //    HandlerDic handlerDic;
        //    Handler handler;

        //    if (handlerDicGroup.TryGetValue(stageID, out handlerDic))
        //    {
        //        if (handlerDic.TryGetValue(userID, out handler))
        //        {
        //            handlerDic.Remove(userID);
        //            handlerDicGroup[stageID] = handlerDic;

        //            return handler;
        //        }
        //    }

        //    return null;
        //}


        //async static Task ReceiveAsync(string stageID, IPacket receivePacket)
        //{
        //    // 클라이언트가 발생시킨 요청을 처리하고 그 결과를 채널 내 사용자들에게 모두 broadcasting 한다. 당사자 포함.
        //    IPacket sendPacket = await Receive(receivePacket);

        //    // disconnect나 stage 이동시에는 null이 나올 수 있음
        //    if (sendPacket != null)
        //    {
        //        await BroadcastToStageAsync(stageID: stageID, sendPacket: sendPacket);
        //    }
        //}

        //async static Task BroadcastToStageAsync(string stageID, IPacket sendPacket)
        //{
        //    HandlerDic handlerDic;

        //    if (handlerDicGroup.TryGetValue(stageID, out handlerDic))
        //    {
        //        foreach (KeyValuePair<string, Handler> kvp in handlerDic)
        //        {
        //            await kvp.Value.SendAsync(packet: sendPacket);
        //        }
        //    }
        //}

        async static Task<IPacket> UserEnter(string stageID, string userID)
        {
            string protocol = "User Enter";
            string value = $"{userID} 입장: {stageID}";
            Console.WriteLine($"[{protocol}] {value}");
            return new PacketValue(protocol: protocol, value: value);
        }

        async static Task<IPacket> UserExit(string stageID, string userID)
        {
            string protocol = "User Exit";
            string value = $"{userID} 퇴장: {stageID}";
            Console.WriteLine($"[{protocol}] {value}");
            return new PacketValue(protocol: protocol, value: value);
        }

        async static Task<IPacket> Receive(IPacket packet)
        {
            if (string.Equals(packet.Protocol, "0"))
            {
                PacketJson json = packet as PacketJson;

                Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.Json);
                string oldStageID = dic["OldStageID"];
                string newStageID = dic["NewStageID"];
                string userID = dic["UserID"];

                Console.WriteLine($"[{json.Protocol}] oldStageID: {oldStageID}, newStageID: {newStageID}, userID: {userID}");

                await listener.MoveStageAsync(oldStageID: oldStageID, newStageID: newStageID, userID: userID);

                return null;
            }
            else if (string.Equals(packet.Protocol, "-1"))
            {
                PacketJson json = packet as PacketJson;

                Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.Json);
                string stageID = dic["StageID"];
                string userID = dic["UserID"];

                Console.WriteLine($"[{json.Protocol}] StageID: {stageID}, userID: {userID}");

                await listener.DisconnectAsync(stageID: stageID, userID: userID);

                return null;
            }
            else
            {
                PacketValue receive = packet as PacketValue;
                Console.WriteLine($"[{receive.Protocol}] {receive.Value}");
                return new PacketValue(protocol: receive.Protocol, value: receive.Value);
            }
        }

        //static IPacket UserEnter(string stageID, string userID)
        //{
        //    string protocol = "User Enter";
        //    string value = $"{userID} 입장: {stageID}";
        //    Console.WriteLine($"[{protocol}] {value}");
        //    return new PacketValue(protocol: protocol, value: value);
        //}

        //static IPacket UserExit(string stageID, string userID)
        //{
        //    string protocol = "User Exit";
        //    string value = $"{userID} 퇴장: {stageID}";
        //    Console.WriteLine($"[{protocol}] {value}");
        //    return new PacketValue(protocol: protocol, value: value);
        //}

        //static IPacket Receive(IPacket packet)
        //{
        //    if (string.Equals(packet.Protocol, "0"))
        //    {
        //        PacketJson json = packet as PacketJson;

        //        Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.Json);
        //        string oldStageID = dic["OldStageID"];
        //        string newStageID = dic["NewStageID"];
        //        string userID = dic["UserID"];

        //        Console.WriteLine($"[{json.Protocol}] oldStageID: {oldStageID}, newStageID: {newStageID}, userID: {userID}");

        //        listener.MoveStage(oldStageID: oldStageID, newStageID: newStageID, userID: userID);

        //        return null;
        //    }
        //    else if (string.Equals(packet.Protocol, "-1"))
        //    {
        //        PacketJson json = packet as PacketJson;

        //        Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.Json);
        //        string stageID = dic["StageID"];
        //        string userID = dic["UserID"];

        //        Console.WriteLine($"[{json.Protocol}] StageID: {stageID}, userID: {userID}");

        //        listener.Disconnect(stageID: stageID, userID: userID);

        //        return null;
        //    }
        //    else
        //    {
        //        PacketValue receive = packet as PacketValue;
        //        Console.WriteLine($"[{receive.Protocol}] {receive.Value}");
        //        return new PacketValue(protocol: receive.Protocol, value: receive.Value);
        //    }
        //}
    }
}
