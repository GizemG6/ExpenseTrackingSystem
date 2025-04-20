using AutoMapper;
using ExpenseTrackingSystem.Application.Dtos.AuditLog;
using ExpenseTrackingSystem.Application.Dtos.Expense;
using ExpenseTrackingSystem.Application.Dtos.ExpenseCategory;
using ExpenseTrackingSystem.Application.Dtos.PaymentSimulation;
using ExpenseTrackingSystem.Application.Dtos.User;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Mapper
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<AppUser, UserDto>().ReverseMap();
			CreateMap<UserCreateDto, AppUser>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.IBAN, opt => opt.MapFrom(src => src.IBAN))
				.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

			CreateMap<Expense, ExpenseDto>()
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
			.ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

			CreateMap<ExpenseCreateDto, Expense>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => ExpenseStatus.Pending))
				.ForMember(dest => dest.ReceiptFilePath, opt => opt.Ignore())
				.ForMember(dest => dest.User, opt => opt.Ignore())
				.ForMember(dest => dest.Category, opt => opt.Ignore());

			CreateMap<ExpenseCategory, ExpenseCategoryDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

			CreateMap<ExpenseCategoryCreateDto, ExpenseCategory>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

			CreateMap<AuditLog, AuditLogDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
				.ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
				.ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.Entity))
				.ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId))
				.ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.ActionDate));

			CreateMap<PaymentSimulation, PaymentSimulationDto>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
			.ForMember(dest => dest.BankReferenceNo, opt => opt.MapFrom(src => src.BankReferenceNo))
			.ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(src => src.PaidAmount))
			.ForMember(dest => dest.IBAN, opt => opt.MapFrom(src => src.IBAN));

			CreateMap<PaymentSimulationCreateDto, PaymentSimulation>()
				.ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
				.ForMember(dest => dest.BankReferenceNo, opt => opt.MapFrom(src => src.BankReferenceNo))
				.ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(src => src.PaidAmount))
				.ForMember(dest => dest.IBAN, opt => opt.MapFrom(src => src.IBAN));
		}
	}
}
