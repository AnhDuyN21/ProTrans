using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Application.Utils
{
    

    public class VNPayUtil
    {
   

        public const string VERSION = "2.1.0";
        private SortedList<String, String> _requestData = new SortedList<String, String>(new VnPayCompare());
        private SortedList<String, String> _responseData = new SortedList<String, String>(new VnPayCompare());
        private static readonly string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly string AllowedCode = "0123456789";
        private static readonly Random Random = new();
        public void ClearRequestData() 
        {
            _requestData.Clear();
        }
        public void AddRequestData(string key, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

        public void AddResponseData(string key, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            string retValue;
            if (_responseData.TryGetValue(key, out retValue))
            {
                return retValue;
            }
            else
            {
                return string.Empty;
            }
        }
     
        public string GenerateId()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                int index = Random.Next(AllowedCharacters.Length);
                sb.Append(AllowedCharacters[index]);
            }
            return sb.ToString();
        }
        public string GetIpAddress()
        {


            string ipAddress;
            try
            {
                HttpContextAccessor ipget = new HttpContextAccessor();
                ipAddress = ipget.HttpContext.Connection.RemoteIpAddress.ToString();
                //  _conte

                if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown") || ipAddress.Length > 45)
                    ipAddress = ipget.HttpContext.GetServerVariable("REMOTE_ADDR");
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }

            return ipAddress;
        }
        public string ExtractUrlParam(string url, string paramName)
        {
            var queryString = url.Split('?').Skip(1).FirstOrDefault();
            if (queryString == null)
            {
                return null; // Return null if no query string
            }

            var keyValuePairs = queryString.Split('&');

            foreach (var pair in keyValuePairs)
            {
                var parts = pair.Split('=');
                if (parts.Length == 2 && parts[0] == paramName)
                {
                    return parts[1];
                }
            }

            return null; // Return null if parameter not found
        }
        public string GetQueryString(string url)
        {
            // Find the first question mark (?), which marks the beginning of the query string
            int questionMarkIndex = url.IndexOf('?');

            // Check if a question mark exists
            if (questionMarkIndex < 0)
            {
                return null; // No query string found
            }

            // Extract the query string
            string queryString = url.Substring(questionMarkIndex + 1);

            // Find the last ampersand (&)
            int lastAmpersandIndex = queryString.LastIndexOf('&');

            // Handle single parameter or no ampersand
            if (lastAmpersandIndex < 0)
            {
                return queryString; // Only one parameter or no ampersand
            }

            // Return the query string without the last parameter
            return queryString.Substring(0, lastAmpersandIndex);
        }
        #region Request

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            baseUrl += "?" + queryString;
            String signData = queryString;
            if (signData.Length > 0)
            {

                signData = signData.Remove(data.Length - 1, 1);
            }
            string vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

            return baseUrl;
        }



        #endregion

        #region Response process

        public bool ValidateSignature(string baseQuery, string inputHash, string secretKey)
        {
            string rspRaw = baseQuery;
            string myChecksum = Utils.HmacSHA512(secretKey, rspRaw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
        private string GetResponseData()
        {

            StringBuilder data = new StringBuilder();
            if (_responseData.ContainsKey("vnp_SecureHashType"))
            {
                _responseData.Remove("vnp_SecureHashType");
            }
            if (_responseData.ContainsKey("vnp_SecureHash"))
            {
                _responseData.Remove("vnp_SecureHash");
            }
            foreach (KeyValuePair<string, string> kv in _responseData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            //remove last '&'
            if (data.Length > 0)
            {
                data.Remove(data.Length - 1, 1);
            }
            return data.ToString();
        }

        #endregion
    }

    public class Utils
    {
      
        public static String HmacSHA512(string key, String inputData)
        {

            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
       
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}
//public async Task<string> Payment(string TransactionId, int Price)
//{

//string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
//string returnUrl = "http://localhost:3000/CheckPayment.html"; // thay đổi đường link phù hợp, là đường link sẽ redirect tới sau khi thanh toán thành công ( hoặc thất bại )
//string tmnCode = "1OYKR3XB";
//string hashSecret = "2DOHB8KEZ4TC1JSDK4EPPT4B27DLC8SF";

//Đặt 4 biến trên ở trong service hoặc appsettings.json

//    string random = SomeTool.GenerateId();
//    string Tref = TransactionId + random;

//    pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
//    pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
//    pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
//    pay.AddRequestData("vnp_Amount", (Price * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
//    pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
//    pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
//    pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
//    pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
//    pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
//    pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
//    pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
//    pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
//    pay.AddRequestData("vnp_TxnRef", Tref); //mã hóa đơn

//    string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
//    return paymentUrl;
//}
//public async Task<(bool, string)> CheckPayment(string url)
//{

//    var query = Utils.GetQueryString(url);

//    string vnp_SecureHash = Utils.ExtractUrlParam(url, "vnp_SecureHash");
//    string _id = Utils.ExtractUrlParam(url, "vnp_TxnRef").Substring(0, Utils.ExtractUrlParam(url, "vnp_TxnRef").Length - 5);
//    string vnp_ResponseCode = Utils.ExtractUrlParam(url, "vnp_ResponseCode");
//    string vnp_TransactionStatus = Utils.ExtractUrlParam(url, "vnp_TransactionStatus");

//    bool checkSignature = pay.ValidateSignature(query, vnp_SecureHash, hashSecret);
//    if (checkSignature)
//    {
//        if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
//        {
//            //Thanh toan thanh cong
//            return (true, "Checkout Successfull");
//        }
//        else
//        {
//            return (false, "Failed");

//        }
//    }
//    else
//    {
//        return (false, "Invalid signature");
//    }
//}

