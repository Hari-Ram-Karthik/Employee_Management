using Amazon.Auth.AccessControlPolicy;
using AutoMapper;
using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo.Entity;
using EmployeeManagement.WebApi.Model.API.Request;
using EmployeeManagement.WebApi.Model.API.Responses;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EmployeeManagement.Unit.Tests")]
namespace EmployeeManagement.WebApi.Infrastructure.Mappers
{
    /// <summary>
    /// Specific implementation of <see cref="IMappingCoordinator"/> for the Employee Management service.
    /// </summary>
    internal class MappingCoordinator : IMappingCoordinator
    {
        private readonly IMapper _mapper;
        protected IMapper Mapper => _mapper;

        public MappingCoordinator()
        {
            var configuration = InitializeMapping();
            _mapper = configuration.CreateMapper();
        }

        private MapperConfiguration InitializeMapping()
        {
            return new MapperConfiguration(cfg =>
            {
                //cfg.AddExpressionMapping();
                MapControllerToDomain(cfg);
                MapDomainToController(cfg);
                MapDomainToDomain(cfg);
                MapDomainToEntity(cfg);
                MapEntityToDomain(cfg);
                //AddMapperProfiles(cfg);
            });
        }
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

        private void MapControllerToDomain(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CreateEmployeeRequestObject, CreateEmployeeRequestModel>();
            cfg.CreateMap<EditEmployeeRequest, EditEmployeeRequestModel>()
                .ForMember(x => x.CreatedAt, option => option.Ignore())
                .ForMember(x => x.UpdatedAt, option => option.Ignore());
            cfg.CreateMap<DeleteEmployeeRequest, DeleteEmployeeRequestModel>();
        }
        private void MapDomainToController(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CreateEmployeeRequestModel, CreateEmployeeRequestObject>();
            cfg.CreateMap<EmployeeModel, CreateEmployeeResponseObject>();
            cfg.CreateMap<EmployeeModel, GetEmployeeResponseObject>()
                .ForMember(x=>x.UpdateAt,option=>option.Ignore());
            cfg.CreateMap<EmployeeModel, EditEmployeeRespose>();
            cfg.CreateMap<EmployeeModel, DeleteEmployeeResponse>()
                .ForMember(x => x.UpdateAt, option => option.Ignore());
        }

        private void MapDomainToEntity(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EmployeeModel, EmployeeEntity>()
                .ForMember(x => x._id, option => option.Ignore());
        }

        private void MapEntityToDomain(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EmployeeEntity, EmployeeModel>();
        }
        private void MapDomainToDomain(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CreateEmployeeRequestModel, EmployeeModel>()
                .ForMember(x => x.CreatedAt, option => option.Ignore())
                .ForMember(x => x.UpdatedAt, option => option.Ignore());
            cfg.CreateMap<EditEmployeeRequestModel, EmployeeModel>();
        }
    }
}
