using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AgencyDTOs;
using Application.ViewModels.AssignmentNotarizationDTOs;
using Application.ViewModels.AssignmentShippingDTOs;
using Application.ViewModels.AssignmentTranslationDTOs;
using Application.ViewModels.DistanceDTOs;
using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.DocumentTypeDTOs;
using Application.ViewModels.FeedbackDTOs;
using Application.ViewModels.ImageShippingDTOs;
using Application.ViewModels.LanguageDTOs;
using Application.ViewModels.NotarizationDTOs;
using Application.ViewModels.NotificationDTOs;
using Application.ViewModels.OrderDTOs;
using Application.ViewModels.QuotePriceDTOs;
using Application.ViewModels.RequestDTOs;
using Application.ViewModels.RoleDTOs;
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
            //Accounts
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, CreateAccountDTO>().ReverseMap();
            CreateMap<Account, RegisterDTO>().ReverseMap();
            CreateMap<Account, CreateTranslatorDTO>().ReverseMap();
            CreateMap<Account, TranslatorAccountDTO>().ReverseMap();

            //Agencys
            CreateMap<Agency, AgencyDTO>().ReverseMap();
            CreateMap<Agency, CUAgencyDTO>().ReverseMap();

            //Distances
            CreateMap<Distance, DistanceDTO>().ReverseMap();
            CreateMap<Distance, CreateUpdateDistanceDTO>().ReverseMap();

            //Requests
            CreateMap<Request, RequestDTO>().ReverseMap();
            CreateMap<Request, CreateRequestDTO>().ReverseMap();
            CreateMap<Request, UpdateRequestDTO>().ReverseMap();
            CreateMap<Request, CustomerUpdateRequestDTO>().ReverseMap();
            CreateMap<Request, RequestCustomerDTO>().ReverseMap();

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
            CreateMap<TranslationSkill, TranslatorSkillDTO>().ReverseMap();
            CreateMap<TranslationSkill, CUTranslatorSkillDTO>().ReverseMap();
            CreateMap<TranslationSkill, CreateTranslatorSkillDTO>().ReverseMap();

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
            CreateMap<Document, UpdateDocumentFromRequestDTO>().ReverseMap();

            //DocumentType
            CreateMap<DocumentType, DocumentTypeDTO>().ReverseMap();
            CreateMap<DocumentType, CUDocumentTypeDTO>().ReverseMap();

            //DocumentHistory
            CreateMap<DocumentHistory, DocumentHistoryDTO>().ReverseMap();
            CreateMap<DocumentHistory, CreateDocumentHistoryDTO>().ReverseMap();

            //DocumentPrice
            CreateMap<DocumentPrice, DocumentPriceDTO>().ReverseMap();
            CreateMap<DocumentPrice, CreateDocumentPriceDTO>().ReverseMap();
            CreateMap<DocumentPrice, UpdateDocumentPriceDTO>().ReverseMap();

            //Orders
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();

            //AssignmentNotarization
            CreateMap<AssignmentNotarization, CUAssignmentNotarizationDTO>().ReverseMap();
            CreateMap<AssignmentNotarization, AssignmentNotarizationDTO>().ReverseMap();

            //Shippings
            CreateMap<AssignmentShipping, AssignmentShippingDTO>().ReverseMap();
            CreateMap<AssignmentShipping, UpdateAssignmentShippingDTO>().ReverseMap();
            CreateMap<AssignmentShipping, CreateAssignmentShippingDTO>().ReverseMap();
            //Transactions
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<Transaction, CUTransactionDTO>().ReverseMap();
            //Role
            CreateMap<Role,RoleDTO>().ReverseMap();

            //NotarizationDetail
            CreateMap<NotarizationDetail, NotarizationDetailDTO>().ReverseMap();
            //ImageShippings
            CreateMap<ImageShipping, ImageShippingDTO>().ReverseMap();
            CreateMap<ImageShipping, CreateImageShippingDTO>().ReverseMap();
            CreateMap<ImageShipping, UpdateImageShippingDTO>().ReverseMap();
            

        }
    }
}
