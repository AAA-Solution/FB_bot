using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Common
{
    public static class Extension
    {
        private static class Environment
        {
            public const string Development = "dev";
            public const string Staging = "stg";
            public const string Production = "prod";
        }

        public enum Status
        {
            ACTIVE,
            INACTIVE,
            PENDING,
            INPROGRESS,
            DELETED,
        }
        public enum RequestType
        {
            FRIEND,
            UNFRIEND,
            BLOCK,
            UNBLOCK
        }

        public enum MsgType
        {
            MESSAGE, //Other people in group chat
            SYSTEM, //System message 
            BOT, //Bot message
            MINE, //My message
            ADMIN //Admin/CS message
        }

        public enum Relationship
        {
            FRIEND,
            BLOCKED,
            STRANGER,
            WAITINGFORACCEPT
        }
        public enum NotificationType
        {
            MESSAGE,
            FRIEND
        }

        public enum Gender
        {
            MALE,
            FEMALE,
            OTHER
        }

        public enum RoomType
        {
            PRIVATE,
            GROUP
        }

        public static string RootFolder
        {
            get
            {
                string di = AppDomain.CurrentDomain.BaseDirectory;
                return di;
            }
        }

        public static int ToInt(this object str)
        {
            try
            {
                return Int32.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static int? ToNullAbleInt(this object str)
        {
            try
            {
                return Int32.Parse(str.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static long ToLong(this object str)
        {
            try
            {
                return long.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static long Tolong(this object str)
        {
            try
            {
                return long.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static long? ToNullAbleLong(this object str)
        {
            try
            {
                return long.Parse(str.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static decimal ToDecimal(this object str)
        {
            try
            {
                return decimal.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static decimal? ToNullAbleDecimal(this object str)
        {
            try
            {
                return decimal.Parse(str.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static float ToFloat(this object str)
        {
            try
            {
                return float.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static double ToDouble(this object str)
        {
            try
            {
                return double.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static double? ToNullableDouble(this object str)
        {
            try
            {
                return double.Parse(str.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static Single ToSingle(this object str)
        {
            try
            {
                return Single.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static Single? ToNullAbleSingle(this object str)
        {
            try
            {
                return Single.Parse(str.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static float? ToNullAbleFloat(this object str)
        {
            try
            {
                return float.Parse(str.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static double? ToNullAbleDouble(this object str)
        {
            try
            {
                return double.Parse(str.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static bool ToBoolean(this object str)
        {
            try
            {
                return str.ToString().ToLower() == "true" || str.ToString() == "1" ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public static bool? ToNullAbleBoolean(this object str)
        {
            try
            {
                if (str.ToString().ToLower() == "true" || str.ToString() == "1")
                {
                    return true;
                }
                if (str.ToString().ToLower() == "false" || str.ToString() == "0")
                {
                    return false;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToDate(this string strDate, string strFormat = "dd/MM/yyyy")
        {
            try
            {
                return DateTime.ParseExact(strDate, strFormat, null);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime? ToNullAbleDate(this string strDate, string strFormat = "dd/MM/yyyy")
        {
            try
            {
                return DateTime.ParseExact(strDate, strFormat, null);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToDateTime(this string strDate, string strFormat = "dd/MM/yyyy HH:mm:ss")
        {
            try
            {
                return DateTime.ParseExact(strDate, strFormat, null);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime? ToNullAbleDateTime(this string strDate, string strFormat = "dd/MM/yyyy HH:mm:ss")
        {
            try
            {
                return DateTime.ParseExact(strDate, strFormat, null);
            }
            catch
            {
                return null;
            }
        }

        public static string ToDateVN(this DateTime dtDate)
        {
            try
            {
                return dtDate.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        public static string ToDateVN(this DateTime? dtDate)
        {
            try
            {
                DateTime dt = (DateTime)dtDate;
                return dt.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        public static string ToDateTimeVN(this DateTime dtDate)
        {
            try
            {
                return dtDate.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                return "";
            }
        }

        public static string ToDateTimeVN(this DateTime? dtDate)
        {
            try
            {
                DateTime dt = (DateTime)dtDate;
                return dt.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                return "";
            }
        }

        public static string ToSerialize(this object obj)
        {
            try
            {
                string res = JsonConvert.SerializeObject(obj);
                return res;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ToSerializeCamel(this object obj)
        {
            try
            {
                string res = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                return res;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static T ToDeserialize<T>(this string context)
        {
            try
            {
                var o = (T)JsonConvert.DeserializeObject<T>(context);
                return o;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T ToDeserializeCamels<T>(this string context)
        {
            try
            {
                var o = (T)System.Text.Json.JsonSerializer.Deserialize<T>(context, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                return o;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static string ToEncryptCryptoTransform(this string text, string key)
        {
            try
            {
                byte[] iv = new byte[16];
                byte[] array;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(text);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }

                string res = Convert.ToBase64String(array);
                return res;

            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ToDecryptCryptoTransform(this string cipherText, string key)
        {
            try
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ToEncrypt(this string TextToBeEncrypted)
        {
            try
            {
                RijndaelManaged RijndaelCipher = new RijndaelManaged();
                string key = "N1109-A7109-M2310-D1993-V2808";
                byte[] PlainText = Encoding.Unicode.GetBytes(TextToBeEncrypted.ToString());
                byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
                //Creates a symmetric encryptor object. 
                ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream();
                //Defines a stream that links data streams to cryptographic transformations
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(PlainText, 0, PlainText.Length);
                //Writes the final state and clears the buffer
                cryptoStream.FlushFinalBlock();
                byte[] CipherBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                string EncryptedData = Convert.ToBase64String(CipherBytes);

                return EncryptedData;
            }
            catch
            {
                return "";
            }
        }

        public static string To3DESEncrypt(this string value, string key)
        {
            try
            {
                TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();

                byte[] byteHash;
                byte[] byteBuff;

                byteHash = Encoding.UTF8.GetBytes(key);
                desCryptoProvider.Key = byteHash;
                desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
                desCryptoProvider.Padding = PaddingMode.PKCS7;
                byteBuff = Encoding.UTF8.GetBytes(value);

                byte[] encoded = desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length);
                string plaintext = BitConverter.ToString(encoded).Replace("-", "");

                return plaintext;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static byte[] ToByteByHex(this string hex)
        {
            try
            {
                return Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                 .ToArray();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string To3DESDecrypt(this string encodedText, string key)
        {
            try
            {
                TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
                //MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

                byte[] byteHash;
                byte[] byteBuff;

                //byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                byteHash = Encoding.UTF8.GetBytes(key);
                desCryptoProvider.Key = byteHash;
                desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
                desCryptoProvider.Padding = PaddingMode.PKCS7;
                byteBuff = encodedText.ToByteByHex();

                byte[] res = desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length);

                string plaintext = Encoding.UTF8.GetString(res);
                return plaintext;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ToSHA256(this string text)
        {
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public static string ToSHA256(this string text, string key)
        {
            try
            {
                using (HMACSHA256 sha256Hash = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                    // Convert byte array to a string   
                    string res = Convert.ToBase64String(bytes);
                    res = res.Split('=')[0]; // Remove any trailing '='s
                    res = res.Replace('+', '-'); // 62nd char of encoding
                    res = res.Replace('/', '_'); // 63rd char of encoding

                    return res;
                }
            }
            catch
            {
                return "";
            }
        }

        public static string ToEncrypt(this string TextToBeEncrypted, string key)
        {
            try
            {
                RijndaelManaged RijndaelCipher = new RijndaelManaged();
                byte[] PlainText = Encoding.Unicode.GetBytes(TextToBeEncrypted.ToString());
                byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
                //Creates a symmetric encryptor object. 
                ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream();
                //Defines a stream that links data streams to cryptographic transformations
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(PlainText, 0, PlainText.Length);
                //Writes the final state and clears the buffer
                cryptoStream.FlushFinalBlock();
                byte[] CipherBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                string EncryptedData = Convert.ToBase64String(CipherBytes);

                return EncryptedData;

            }
            catch
            {
                return "";
            }
        }

        public static string ToBase64(this string text)
        {
            try
            {
                string req = text;
                var plainTextBytes = Encoding.UTF8.GetBytes(req);
                string res = Convert.ToBase64String(plainTextBytes);

                return res;
            }
            catch
            {
                return "";
            }
        }

        public static string ToStringByBase64(this string text)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(text);
                string res = Encoding.UTF8.GetString(base64EncodedBytes);

                return res;
            }
            catch
            {
                return "";
            }
        }

        public static string ToDecrypt(this string TextToBeDecrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            string key = "N1109-A7109-M2310-D1993-V2808";
            string DecryptedData;

            try
            {
                byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted.ToString());

                byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());
                //Making of the key for decryption
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
                //Creates a symmetric Rijndael decryptor object.
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();

                //Converting to string
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = "";
            }
            return DecryptedData;
        }

        public static string ToDecrypt(this string TextToBeDecrypted, string key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            string DecryptedData;

            try
            {
                byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted.ToString());

                byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());
                //Making of the key for decryption
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
                //Creates a symmetric Rijndael decryptor object.
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();

                //Converting to string
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = "";
            }
            return DecryptedData;
        }

        public static string RandomString(this int size, bool lowerCase)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToChar((Math.Floor(26 * random.NextDouble() + 65)).ToInt());
                    builder.Append(ch);
                }
                if (lowerCase)
                    return builder.ToString().ToLower();
                return builder.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string RandomNumber(this int size)
        {
            try
            {
                List<int> myValues = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                Random r = new Random();
                IEnumerable<int> sizeRandom = myValues.OrderBy(x => r.Next()).Take(size);
                string res = string.Join("", sizeRandom);
                return res;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetAuthorization(this HttpRequestHeaders header)
        {
            try
            {
                if (header.Contains("Authorization"))
                {
                    string token = header.GetValues("Authorization").First();
                    return token;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ToHTMLEncode(this string url)
        {
            try
            {
                string e = HttpUtility.HtmlEncode(url).ToString();
                return e;
            }
            catch
            {
                return url;
            }
        }

        public static string ToHTMLDecode(this string url)
        {
            try
            {
                string d = HttpUtility.HtmlDecode(url).ToString();
                return d;
            }
            catch
            {
                return url;
            }
        }

        public static string ToEmptyString(this object obj)
        {
            try
            {
                var a = obj.ToString();
                return a;
            }
            catch
            {
                return "";
            }
        }

        public static long ToTimestamp(this DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }

        public static byte[] ToByteByBase64(this string str)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(str);

                return base64EncodedBytes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ToBase64UrlEncode(this string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }

        public static string GetRootFolder()
        {
            try
            {
                string di = AppDomain.CurrentDomain.BaseDirectory;
                return di;
            }
            catch
            {
                return "/";
            }
        }        
    }
}
