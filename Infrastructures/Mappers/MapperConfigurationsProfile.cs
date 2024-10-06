using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AssignmentNotarizationDTOs;
using Application.ViewModels.AssignmentTranslationDTOs;
using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.FeedbackDTOs;
using Application.ViewModels.LanguageDTOs;
using Application.ViewModels.NotarizationDTOs;
using Application.ViewModels.NotificationDTOs;
using Application.ViewModels.OrderDTOs;
using Application.ViewModels.PaymentMethodDTOs;
using Application.ViewModels.QuotePriceDTOs;
using Application.ViewModels.ShippingDTOs;
using Application.ViewModels.TranslatorSkillDTOs;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            //Accounts
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, CreateAccountDTO>().ReverseMap();
            CreateMap<Account, RegisterDTO>().ReverseMap();

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
            //Notificaitons
            CreateMap<Notification, SendNotificationDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            //AssignmentTranslation
            CreateMap<AssignmentTranslation,CUAssignmentTranslationDTO>().ReverseMap();
            CreateMap<AssignmentTranslation, AssignmentTranslationDTO>().ReverseMap();
            //Feedbacks
            CreateMap<FeedBack, FeedbackDTO>().ReverseMap();
            CreateMap<FeedBack, CUFeedbackDTO>().ReverseMap();
            //Documents
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<Document, CUDocumentDTO>().ReverseMap();
            //Orders
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, CUOrderDTO>().ReverseMap();
            //AssignmentNotarization
            CreateMap<AssignmentNotarization, CUAssignmentNotarizationDTO>().ReverseMap();
            CreateMap<AssignmentNotarization, AssignmentNotarizationDTO>().ReverseMap();
            //Shippings
            CreateMap<Shipping, ShippingDTO>().ReverseMap();
            CreateMap<Shipping, CUShippingDTO>().ReverseMap();
            //PaymentMethods
            CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
            CreateMap<PaymentMethod, CUPaymentMethodDTO>().ReverseMap();

        }
    }
}
