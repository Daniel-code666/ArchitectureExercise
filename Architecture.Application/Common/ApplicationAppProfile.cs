using Architecture.Application.Invoices.Dtos;
using Architecture.Application.Materials.Dtos;
using Architecture.Domain.Entities;
using AutoMapper;

namespace Architecture.Application.Common
{
    public class ApplicationAppProfile : Profile
    {
        public ApplicationAppProfile()
        {
            MapInvoices();
            MapInvoiceDetails();
            MapMaterials();
        }

        private void MapInvoices()
        {
            CreateMap<InvoicesEntity, InvoiceReadDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<InvoiceDto, InvoicesEntity>()
                .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));
        }

        private void MapInvoiceDetails()
        {
            // READ
            CreateMap<InvoiceDetailsEntity, InvoiceDetailReadDto>();

            // CREATE/UPDATE
            CreateMap<InvoiceDetailDto, InvoiceDetailsEntity>()
                .ForMember(dest => dest.InvoiceDetailId, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
                .ForMember(dest => dest.Invoice, opt => opt.Ignore())
                .ForMember(dest => dest.Material, opt => opt.Ignore());
        }

        private void MapMaterials()
        {
            CreateMap<MaterialsEntity, MaterialReadDto>();

            CreateMap<MaterialDto, MaterialsEntity>()
                .ForMember(dest => dest.MaterialId, opt => opt.Ignore());
        }
    }
}
