using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Docker.Database;

namespace Test.Docker.Server.Chatting
{
    public static class DbManager
    {
        static MariaDbConnector _database;

        public static void Init(string serverIP, string databaseName, string uid, string password)
        {
            _database = new MariaDbConnector(serverIP: serverIP, databaseName: databaseName, uid: uid, password: password);
        }

        //public static DatabaseIdTypeDic DatabaseTypeIdDic { get { return _database.DatabaseTypeIdDic; } }
        //public static DatabaseTypeGroupDic DatabaseTypeGroupDic { get { return _database.DatabaseTypeGroupDic; } }
        //public static Containers Containers { get { return _database.Containers; } }

        //public static Task<uint> GetLastID()
        //{
        //    return _database.GetLastID();
        //}

        //public static Task<bool> AddUser(UserInfo userInfo, int state)
        //{
        //    return _database.AddUser(userInfo: userInfo, state: state);
        //}

        //public static Task<UserInfo> GetUserInfo(string userID, string cryptedPassword, int state)
        //{
        //    return _database.GetUserInfo(userID: userID, cryptedPassword: cryptedPassword, state: state);
        //}

        //public static Task<FolderTrees> GetFolderTrees(int state)
        //{
        //    return _database.GetFolderTrees(state: state);
        //}

        //public static Task<bool> AddFolder(uint userID, string folderName, int state)
        //{
        //    return _database.AddFolder(userID: userID, folderName: folderName, state: state);
        //}

        //public static Task<bool> RenameFolder(uint folderID, string folderName)
        //{
        //    return _database.RenameFolder(folderID: folderID, folderName: folderName);
        //}

        //public static Task<bool> DeleteFolder(uint folderID, int state)
        //{
        //    return _database.DeleteFolder(folderID: folderID, state: state);
        //}

        //public static Task<uint> GetFileIdByChecksum(string checksum)
        //{
        //    return _database.GetFileIdByChecksum(checksum: checksum);
        //}

        //public static Task<bool> AddFileDoc(string fileName, string checksum)
        //{
        //    return _database.AddFileDoc(fileName: fileName, checksum: checksum);
        //}

        //public static Task<bool> AddDoc(uint userID, uint folderID, uint fileID, string docName, int state)
        //{
        //    return _database.AddDoc(userID: userID, folderID: folderID, fileID: fileID, docName: docName, state: state);
        //}

        //public static Task<bool> AddFilePages(PdfImages pdfImages, uint fileDocID, uint imageTypeID, uint thumbnailTypeID)
        //{
        //    return _database.AddFilePages(pdfImages: pdfImages, fileDocID: fileDocID, imageTypeID: imageTypeID, thumbnailTypeID: thumbnailTypeID);
        //}

        //public static Task<bool> UpdateDocPageCount(uint docID, int pageCount)
        //{
        //    return _database.UpdateDocPageCount(docID: docID, pageCount: pageCount);
        //}

        //public static Task<int> GetDocPageCount(uint docID)
        //{
        //    return _database.GetDocPageCount(docID: docID);
        //}

        //public static Task<bool> AddRawTexts(Words rawTexts, uint pageID)
        //{
        //    return _database.AddRawTexts(rawTexts: rawTexts, pageID: pageID);
        //}

        //public static Task<bool> UpdateDocState(uint docID, int state)
        //{
        //    return _database.UpdateDocState(docID: docID, state: state);
        //}

        //public static Task<bool> RenameDoc(uint docID, string docName)
        //{
        //    return _database.RenameDoc(docID: docID, docName: docName);
        //}

        //public static Task<bool> DeleteDoc(uint docID, int state)
        //{
        //    return _database.DeleteDoc(docID: docID, state: state);
        //}

        //public static Task<Dictionary<string, uint>> GetFilePageChecksumIdDic(uint fileDocID)
        //{
        //    return _database.GetFilePageChecksumIdDic(fileDocID: fileDocID);
        //}

        //public static Task<WordRaws> GetDocWords(uint docID)
        //{
        //    return _database.GetDocWords(docID: docID);
        //}

        //public static Task<bool> AddParagraph(Paragraphs paragraphs, uint docID)
        //{
        //    return _database.AddParagraph(paragraphs: paragraphs, docID: docID);
        //}

        //public static Task<Dictionary<int, uint>> GetParagraphIndexIdDic(uint docID)
        //{
        //    return _database.GetParagraphIndexIdDic(docID: docID);
        //}

        //public static Task<bool> AddSentence(Sentences sentences, uint paragraphID)
        //{
        //    return _database.AddSentence(sentences: sentences, paragraphID: paragraphID);
        //}

        //public static Task<Dictionary<uint, Dictionary<int, uint>>> GetSentenceIndexIdDic(uint docID)
        //{
        //    return _database.GetSentenceIndexIdDic(docID: docID);
        //}

        //public static Task<bool> AddLinkTextSentence(Sentences sentences, Dictionary<int, uint> sentenceIndexIdDic)
        //{
        //    return _database.AddLinkTextSentence(sentences: sentences, sentenceIndexIdDic: sentenceIndexIdDic);
        //}

        //public static Task<Paragraphs> GetDocParagraph(uint docID)
        //{
        //    return _database.GetDocParagraphs(docID: docID);
        //}

        //public static Task<SimilarResults> GetSimilarResult(uint paragraphID)
        //{
        //    return _database.GetSimilarResults(paragraphID: paragraphID);
        //}
    }
}
