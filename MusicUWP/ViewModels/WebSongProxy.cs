using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using MusicUWP.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using Windows.Storage;

namespace MusicUWP.ViewModels
{
    public class WebSongProxy
    {
        private const string appid = "15763";
        private const string app_sign = "1ef463b86f3b4a64a9fa58beadeff6a8";
        private const string topListUri = "https://route.showapi.com/213-4";
        private const string searchSongByIdUri = "https://route.showapi.com/213-2";
        private const string searchSongByNameUri = "https://route.showapi.com/213-1";

        public static async Task<SongResponseBandList>GetBandListAsync(int topId)
        {
            //1. Get full request url
            var timeStamp = GetTimeStamp();
            string fullUrl = string.Format("{0}?showapi_appid={1}&showapi_timestamp={2}&topid={3}&showapi_sign={4}"
                , topListUri,  appid, timeStamp, topId, app_sign);

            //2. wait http response
            string Json = await GetJsonResponseAsync(fullUrl);

            //3. serialize the Json message
            var serializer = new DataContractJsonSerializer(typeof(SongResponseBandList));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(Json));
            try
            {
                var result = (SongResponseBandList)serializer.ReadObject(stream);
                return result;
            }
            catch(InvalidCastException ex)
            {
                throw new Exception("网络请求错误"+ex.Message);
            }

        }

        public static async Task<SongResponseByName>GetSongByNameAsync(string name , int page = 1)
        {
            //1. Get full request url
            var timeStamp = GetTimeStamp();
            string fullUrl = string.Format("{0}?keyword={1}&page={2}&showapi_appid={3}&showapi_timestamp={4}&showapi_sign={5}"
                , searchSongByNameUri, name, page, appid, timeStamp, app_sign);

            //2. wait http response
            string Json = await GetJsonResponseAsync(fullUrl);

            //3. serialize the Json message
            var serializer = new DataContractJsonSerializer(typeof(SongResponseByName));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(Json));
            try
            {
                var result = (SongResponseByName)serializer.ReadObject(stream);
                return result;
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("网络请求错误" + ex.Message);
            }
        }

        private static async Task<byte []> DownloadSongStream(string url)
        { 
            HttpClient http = new HttpClient();
            var res = await http.GetAsync(url);
            var buffer = await res.Content.ReadAsByteArrayAsync();
            return buffer;
        }

        public static async Task DownloadSong(string url, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter br = new BinaryWriter(fs);
            byte[] buffer = await DownloadSongStream(url);
            br.Write(buffer);

            fs.Dispose();
            br.Dispose();
        }

        public static async Task<SongResponseById>GetSongByIdAsync(string songId)
        {
            //1. Get full request url
            var timeStamp = GetTimeStamp();
            string fullUrl = string.Format("{0}?musicid={1}&showapi_appid={2}&showapi_timestamp={3}&showapi_sign={4}"
                , searchSongByIdUri, songId, appid, timeStamp, app_sign);

            //2. wait http response
            string Json = await GetJsonResponseAsync(fullUrl);

            //3. serialize the Json message
            var serializer = new DataContractJsonSerializer(typeof(SongResponseById));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(Json));
            try
            {
                var result = (SongResponseById)serializer.ReadObject(stream);
                return result;
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("网络请求错误" + ex.Message);
            }
        }

        public static async Task<string>GetJsonResponseAsync(string url)
        {
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        private static string CreateHash(string timeStamp)
        {

            var toBeHashed = timeStamp + app_sign + appid;
            var hashedMessage = ComputeMD5(toBeHashed);
            return hashedMessage;
        }

        private static string ComputeMD5(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }

        private static string GetTimeStamp()
        {
            var time = DateTime.Now;
            string str = time.Year.ToString() + time.Month.ToString().PadLeft(2, '0') + time.Day.ToString().PadLeft(2, '0')
                + time.Hour.ToString().PadLeft(2, '0') + time.Minute.ToString().PadLeft(2, '0') + time.Second.ToString().PadLeft(2, '0');
            return str;
        }
    }
}
