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
    }
}
