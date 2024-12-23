using Application.Commons;
using Application.Interfaces.InterfaceServices.VNPay;
using Application.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.VNPay
{
    public class VNPayService : IVNPayService
    {

        private readonly IHttpContextAccessor ipget;
        string url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        string returnUrl = "https://protrans-five.vercel.app/"; // thay đổi đường link phù hợp, là đường link sẽ redirect tới sau khi thanh toán thành công ( hoặc thất bại )
        string tmnCode = "3ZFKCVRM";
        string hashSecret = "JF5SQO62ML6ZJI9H4F1V1GUYH11QYTRF";
        VNPayUtil pay = new VNPayUtil();
        public VNPayService(IHttpContextAccessor _ipget) 
        {
            ipget = _ipget;
        }
        public async Task<ServiceResponse<string>> CreatePaymentlink( Guid shipperId, decimal totalPrice)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            string random = pay.GenerateId();
            string Tref = shipperId.ToString() + random;

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", totalPrice.ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", Tref); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            response.Success = true;
            response.Data = paymentUrl;
            response.Message = string.Empty;
            return response;
        }

        public async Task<ServiceResponse<string>> VerifyPayment(string url)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var query = pay.GetQueryString(url);

            string vnp_SecureHash = pay.ExtractUrlParam(url, "vnp_SecureHash");
            string _id = pay.ExtractUrlParam(url, "vnp_TxnRef").Substring(0, pay.ExtractUrlParam(url, "vnp_TxnRef").Length - 5);
            string vnp_ResponseCode = pay.ExtractUrlParam(url, "vnp_ResponseCode");
            string vnp_TransactionStatus = pay.ExtractUrlParam(url, "vnp_TransactionStatus");

            bool checkSignature = pay.ValidateSignature(query, vnp_SecureHash, hashSecret);
            if (checkSignature)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    //Thanh toan thanh cong
                    response.Data = _id;
                    response.Message = "Thanh toán thành công";
                    response.Success = true;
                    return response;
                }
                else
                {
                    response.Data = _id;
                    response.Message = "Thanh toán thất bại";
                    response.Success = true;
                    return response;

                }
            }
            else
            {
                response.Data = _id;
                response.Message = "Đã xảy ra lỗi khi thanh toán";
                response.Success = false;
                return response;
            }
        }
    }
}
