using System;
using System.Threading.Tasks;
using Suyeong.Core.Net.Lib;
using Suyeong.Core.Net.Tcp;
using Suyeong.Core.Util;
using Test.Docker.Type;
using Test.Docker.Variable;

namespace Test.Docker.Server.Login
{
    public static class TcpManager
    {
        static TcpListenerSimpleAsync _listener;

        public static void Init(int portNum)
        {
            _listener = new TcpListenerSimpleAsync(portNum: portNum);
        }

        async public static Task Listening()
        {
            await _listener.StartAsync(callback: ReturnRequest).ConfigureAwait(false);
        }

        async static Task<IPacket> ReturnRequest(IPacket packet)
        {
            //Console.WriteLine($"{packet.Protocol} /{DateTime.Now.ToString(Formats.DATE_LOG)}");

            switch (packet.Protocol)
            {
                case Protocol.USER_LOGIN:
                    return await CreateUserInfo(packet: packet as PacketSerialized).ConfigureAwait(false);

                default:
                    return new PacketValue(protocol: string.Empty, value: 0);
            }
        }

        async static Task<PacketSerialized> CreateUserInfo(PacketSerialized packet)
        {
            UserInfo userInfo = (UserInfo)StreamUtil.DeserializeObject(packet.SerializedData);

            UserInfo result = await DbManager.GetUserInfo(userID: userInfo.UserID, cryptedPassword: userInfo.Name, state: (int)DbState.Enable).ConfigureAwait(false);

            return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(result));
        }

        //async static Task<PacketSerialized> GetUserInfo(PacketJson packet)
        //{
        //    // 일단은 바로 로그인을 시켜주지만 차후에는 아래와 같이 해야 한다.

        //    // 사용자가 로그인을 시도하면, db에서 사용자 id를 가져온다.
        //    // 가져온 id로 현재 접속 중인지 체크한다.
        //    // 동일 id로 접속중인 사용자가 있으면 새로 접속한 사용자에게 기존 사용자를 끊을지 물어보고 끊으면 기존 사용자를 끊고, 아니면 만다.
        //    // db를 볼 필요도 없이 여기서 관리 가능.

        //    Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(packet.Json);
        //    string id = dic[ColumnName.ID];
        //    string password = dic[ColumnName.PASSWORD];

        //    UserInfo userInfo = await Database.GetUserInfo(id: id, cryptedPassword: password, state: (int)DbState.Enable);

        //    // 여기서 가져온 user info로 현재 서버에 접속중인 user를 확인한다. - 다만 현재는 현재 접속 중인 사용자 정보가 없기 때문에 사용 안 함.
        //    if (userInfo.ID > 0 && !_currentUserDic.ContainsKey(userInfo.ID))
        //    {
        //        _currentUserDic.Add(userInfo.ID, userInfo);
        //    }
        //    else
        //    {
        //        // 기존 사용자의 접속을 끊어야 함.
        //    }

        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(userInfo));
        //}

        //async static Task<PacketSerialized> GetFolderTree(PacketValue packet)
        //{
        //    FolderTrees FolderTrees = await Database.GetFolderTrees(state: (int)DbState.Enable);
        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(FolderTrees));
        //}

        //async static Task<PacketSerialized> AddFolder(PacketJson packet)
        //{
        //    Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Json);
        //    uint userID = ConvertUtil.StringToUint(dic[ColumnName.ID].ToString());
        //    string FolderName = dic[ColumnName.NAME].ToString();

        //    bool result = await Database.AddFolder(userID: userID, FolderName: FolderName);
        //    FolderTrees FolderTrees = await Database.GetFolderTrees(state: (int)DbState.Enable);
        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(FolderTrees));
        //}

        //async static Task<PacketSerialized> RenameFolder(PacketJson packet)
        //{
        //    Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Json);
        //    uint FolderID = ConvertUtil.StringToUint(dic[ColumnName.ID].ToString());
        //    string FolderName = dic[ColumnName.NAME].ToString();

        //    bool result = await Database.RenameFolder(FolderID: FolderID, FolderName: FolderName);
        //    FolderTrees FolderTrees = await Database.GetFolderTrees(state: (int)DbState.Enable);
        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(FolderTrees));
        //}

        //async static Task<PacketSerialized> DeleteFolder(PacketValue packet)
        //{
        //    uint FolderID = ConvertUtil.StringToUint(packet.Value.ToString());

        //    bool result = await Database.DeleteFolder(FolderID: FolderID, state: (int)DbState.Disable);
        //    FolderTrees FolderTrees = await Database.GetFolderTrees(state: (int)DbState.Enable);
        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(FolderTrees));
        //}

        //async static Task<PacketValue> AddDoc(PacketFile packet)
        //{
        //    Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Desc);
        //    uint userID = ConvertUtil.StringToUint(dic[ColumnName.USER_ID].ToString());
        //    uint folderID = ConvertUtil.StringToUint(dic[ColumnName.FOLDER_ID].ToString());
        //    string fileName = dic[ColumnName.NAME].ToString();

        //    await Database.AddDoc(userID: userID, folderID: folderID, docName: fileName, state: (int)DocState.Waiting);

        //    uint docID = await Database.GetLastID();

        //    FileManager.SetFileToWaitingPath(fileName: $"{docID}_{fileName}", fileData: packet.FileData);

        //    await Database.UpdateDocState(docID: docID, state: (int)DocState.Waiting);

        //    return new PacketValue(protocol: packet.Protocol, value: 1);
        //}

        //async static Task<PacketSerialized> RenameDoc(PacketJson packet)
        //{
        //    Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Json);
        //    uint docID = ConvertUtil.StringToUint(dic[ColumnName.ID].ToString());
        //    string docName = dic[ColumnName.NAME].ToString();

        //    bool result = await Database.RenameDoc(docID: docID, docName: docName);

        //    FolderTrees FolderTrees = await Database.GetFolderTrees(state: (int)DbState.Enable);
        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(FolderTrees));
        //}

        //async static Task<PacketSerialized> DeleteDoc(PacketValue packet)
        //{
        //    uint docID = ConvertUtil.StringToUint(packet.Value.ToString());

        //    bool result = await Database.DeleteDoc(docID: docID, state: (int)DbState.Disable);

        //    FolderTrees FolderTrees = await Database.GetFolderTrees(state: (int)DbState.Enable);
        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(FolderTrees));
        //}

        //async static Task<PacketFile> DownloadPageImage(PacketJson packet)
        //{
        //    Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Json);
        //    uint docID = ConvertUtil.StringToUint(dic[ColumnName.DOC_ID].ToString());
        //    int pageIndex = ConvertUtil.StringToInt(dic[ColumnName.PAGE_INDEX].ToString());

        //    uint filePageID = await Database.GetFilePageID(docID: docID, pageIndex: pageIndex);
        //    byte[] data = FileManager.GetImage(filePageID: filePageID);

        //    return new PacketFile(protocol: packet.Protocol, desc: filePageID.ToString(), fileData: data);
        //}

        //async static Task<PacketSerialized> GetDocTagItems(PacketValue packet)
        //{
        //    uint docID = ConvertUtil.StringToUint(packet.Value.ToString());

        //    uint versionID = await Database.GetDocVersionID(docID: docID);
        //    TagItems tagItems = await Database.GetTagItems(versionID: versionID);
        //    PartItemsDic partItemsDic = await Database.GetPartItemsDic(versionID: versionID);
        //    TagItems tagItemNews = MergeItem(tagItems: tagItems, partItemsDic: partItemsDic);

        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(tagItemNews));
        //}

        //async static Task<PacketSerialized> GetDocRawTexts(PacketValue packet)
        //{
        //    uint docID = ConvertUtil.StringToUint(packet.Value.ToString());

        //    PageRawTexts pageRawTexts = await Database.GetPageRawTexts(docID: docID);
        //    return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(pageRawTexts));
        //}

        //async static Task<PacketValue> RetryParseDoc(PacketValue packet)
        //{
        //    uint docID = ConvertUtil.StringToUint(packet.Value.ToString());

        //    bool result = await ParsingManager.SendToParser(docID: docID);

        //    return new PacketValue(protocol: packet.Protocol, value: result ? 1 : 0);
        //}

        //static TagItems MergeItem(TagItems tagItems, PartItemsDic partItemsDic)
        //{
        //    TagItems tagItemNews = new TagItems();

        //    PartItems partItems;

        //    foreach (TagItem tagItem in tagItems)
        //    {
        //        if (partItemsDic.TryGetValue(tagItem.ID, out partItems))
        //        {
        //            tagItemNews.Add(new TagItem(id: tagItem.ID, pageID: tagItem.PageID, pageIndex: tagItem.PageIndex, tagNo: tagItem.TagNo, uniqueName: tagItem.UniqueName, x: tagItem.X, y: tagItem.Y, width: tagItem.Width, height: tagItem.Height, attributes: tagItem.Attributes, partItems: partItems));
        //        }
        //    }

        //    return tagItemNews;
        //}
    }
}
