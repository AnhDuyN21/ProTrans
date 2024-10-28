using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AgencyDTOs;
using Application.ViewModels.AssignmentNotarizationDTOs;
using Application.ViewModels.AssignmentTranslationDTOs;
using Application.ViewModels.AttachmentDTOs;
using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.DocumentTypeDTOs;
using Application.ViewModels.FeedbackDTOs;
using Application.ViewModels.ImageDTOs;
using Application.ViewModels.LanguageDTOs;
using Application.ViewModels.NotarizationDTOs;
using Application.ViewModels.NotificationDTOs;
using Application.ViewModels.OrderDTOs;
using Application.ViewModels.PaymentMethodDTOs;
using Application.ViewModels.QuotePriceDTOs;
using Application.ViewModels.RequestDTOs;
using Application.ViewModels.ShippingDTOs;
using Application.ViewModels.TransactionDTOs;
using Application.ViewModels.TranslatorSkillDTOs;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            //Attachments
            CreateMap<Attachment, AttachmentDTO>().ReverseMap();
            CreateMap<Attachment, CreateAttachmentDTO>().ReverseMap();

            //Accounts
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, CreateAccountDTO>().ReverseMap();
            CreateMap<Account, RegisterDTO>().ReverseMap();
            CreateMap<Account, CreateTranslatorDTO>().ReverseMap();

            //Agencys
            CreateMap<Agency, AgencyDTO>().ReverseMap();
            CreateMap<Agency, CUAgencyDTO>().ReverseMap();

            //Images
            CreateMap<Image, ImageDTO>().ReverseMap();

            //Requests
            CreateMap<Request, RequestDTO>().ReverseMap();
            CreateMap<Request, CreateRequestDTO>().ReverseMap();
            CreateMap<Request, UpdateRequestDTO>().ReverseMap();

            //Notarizations
            CreateMap<Notarization, NotarizationDTO>().ReverseMap();
            CreateMap<Notarization, CreateNotarizationDTO>().ReverseMap();

            //QuotePrices
            CreateMap<QuotePrice, QuotePriceDTO>().ReverseMap();
            CreateMap<QuotePrice, CUQuotePriceDTO>().ReverseMap();

            //Languages
            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<Language, CULanguageDTO>().ReverseMap();

            //TranslatorSkills
            CreateMap<TranslatorSkill, TranslatorSkillDTO>().ReverseMap();
            CreateMap<TranslatorSkill, CUTranslatorSkillDTO>().ReverseMap();
            CreateMap<TranslatorSkill, CreateTranslatorSkillDTO>().ReverseMap();

            //Notificaitons
            CreateMap<Notification, SendNotificationDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();

            //AssignmentTranslation
            CreateMap<AssignmentTranslation, CUAssignmentTranslationDTO>().ReverseMap();
            CreateMap<AssignmentTranslation, AssignmentTranslationDTO>().ReverseMap();

            //Feedbacks
            CreateMap<Feedback, FeedbackDTO>().ReverseMap();
            CreateMap<Feedback, CUFeedbackDTO>().ReverseMap();

            //Documents
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<Document, CreateDocumentDTO>().ReverseMap();
            CreateMap<Document, UpdateDocumentDTO>().ReverseMap();

            //DocumentType
            CreateMap<DocumentType, DocumentTypeDTO>().ReverseMap();
            CreateMap<DocumentType, CUDocumentTypeDTO>().ReverseMap();

            //Orders
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();

            //AssignmentNotarization
            CreateMap<AssignmentNotarization, CUAssignmentNotarizationDTO>().ReverseMap();
            CreateMap<AssignmentNotarization, AssignmentNotarizationDTO>().ReverseMap();

            //Shippings
            CreateMap<Shipping, ShippingDTO>().ReverseMap();
            CreateMap<Shipping, UpdateShippingDTO>().ReverseMap();
            CreateMap<Shipping, CreateShippingDTO>().ReverseMap();
            //PaymentMethods
            CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
            CreateMap<PaymentMethod, CUPaymentMethodDTO>().ReverseMap();
            //Transactions
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<Transaction, CUTransactionDTO>().ReverseMap();

        }
    }
}
