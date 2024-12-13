using Application.Commons;
using Application.ViewModels.SendMail;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Application.Interfaces;
using System.Net.Mail;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class SendMail : ISendMail
    {
        private readonly IUnitOfWork _unitOfWork;
        public SendMail(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendEmailAsync(MessageDTO messageDTO)
        {
            //var message = new MessageDTO
            //{
            //    To = messageDTO.To,
            //    Subject = messageDTO.Subject,
            //    Body = messageDTO.Body,
            //    ImageUrl = messageDTO.ImageUrl
            //};
            await _unitOfWork.SendMailRepository.SendEmailAsync(messageDTO);
        }

        public async Task<ServiceResponse<bool>> SendBill(MessageDTO message, Guid orderId, string shipperName, string shipperPhone, string imageUrl)
        {

            var response = new ServiceResponse<bool>();

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            var document = await _unitOfWork.DocumentRepository.GetByOrderIdAsync(orderId);
            var request = await _unitOfWork.RequestRepository.GetAllAsync(x => x.Id.Equals(order.RequestId));
            DocumentPrice[] priceArray;
            List<DocumentPrice> priceList = new List<DocumentPrice>();
            foreach (var documentId in document)
            {
                var price = await _unitOfWork.DocumentPriceRepository.GetAllAsync(x => x.DocumentId.Equals(documentId.Id));
                var result = price.FirstOrDefault();
                if (result != null)
                {
                    priceList.Add(result);
                }

            }
            priceArray = priceList.ToArray();
            var documentArray = document.ToArray();


            //if (order == null)
            //{
            //    response.Success = false;
            //    response.Message = "Delete fail.";
            //    return response;
            //}
            try
            {
                var pdf = _unitOfWork.SendMailRepository.CreatePDF(order, documentArray, priceArray, shipperName, shipperPhone, request.FirstOrDefault().PickUpRequest);
                await _unitOfWork.SendMailRepository.SendEmailWithPDFAsync(message, pdf, imageUrl, order.FullName);


                response.Success = true;
                response.Message = "Send email successfully.";


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Send email failed.";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;
        }
    }
}
