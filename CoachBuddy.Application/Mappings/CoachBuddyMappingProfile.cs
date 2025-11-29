using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Application.Client;
using CoachBuddy.Application.Client.Commands.DeleteClient;
using CoachBuddy.Application.Client.Commands.EditClient;
using CoachBuddy.Application.ClientGroup;
using CoachBuddy.Application.ClientTraining;
using CoachBuddy.Application.Exercise;
using CoachBuddy.Application.Exercise.Commands.CreateExercise;
using CoachBuddy.Application.Exercise.Commands.EditExercise;
using CoachBuddy.Application.Group;
using CoachBuddy.Application.Group.Commands.CreateGroup;
using CoachBuddy.Application.Group.Commands.EditGroup;
using CoachBuddy.Application.TrainingPlan;
using CoachBuddy.Application.TrainingPlan.Commands.CreateTrainingPlan;
using CoachBuddy.Application.TrainingPlan.Commands.DeleteTrainingPlan;
using CoachBuddy.Application.TrainingPlan.Commands.EditTrainingPlan;
using CoachBuddy.Domain.Entities.Client;

namespace CoachBuddy.Application.Mappings
{
    public class CoachBuddyMappingProfile : Profile
    {
        public CoachBuddyMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<ClientDto, Domain.Entities.Client.Client>()
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new ClientContactDetails()
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));

            CreateMap<Domain.Entities.Client.Client, ClientDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && user.IsInRole("Admin")))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber));

            CreateMap<ClientDto, EditClientCommand>();

            CreateMap<ClientTrainingDto, Domain.Entities.Client.ClientTraining>()
                .ReverseMap();

            CreateMap<ClientDto, DeleteClientCommand>();

            CreateMap<Domain.Entities.Group.Group, GroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src => src.EncodedName))
                .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById))
                .ForMember(dest => dest.IsEditable, opt => opt.MapFrom(src => user != null && user.IsInRole("Admin")));

            CreateMap<GroupDto, Domain.Entities.Group.Group>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ClientGroups, opt => opt.Ignore());

            CreateMap<CreateGroupCommand, Domain.Entities.Group.Group>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EncodedName, opt => opt.Ignore());

            CreateMap<GroupDto, EditGroupCommand>();

            CreateMap<Domain.Entities.Group.ClientGroup, ClientGroupDto>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Client.Name} {src.Client.LastName}"))
                .ForMember(dest => dest.AssignedAt, opt => opt.MapFrom(src => src.AssignedAt));

            CreateMap<Domain.Entities.Client.Client, AvailableClientDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"));

            CreateMap<Domain.Entities.Group.Group, GroupDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src => src.EncodedName))
                .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById))
                .ForMember(dest => dest.IsEditable, opt => opt.MapFrom(src => user != null && user.IsInRole("Admin")))
                .ForMember(dest => dest.ClientGroups, opt => opt.MapFrom(src => src.ClientGroups))
                .ForMember(dest => dest.AvailableClients, opt => opt.MapFrom(src => src.ClientGroups.Select(cg => cg.Client)));
           
            CreateMap<Domain.Entities.Group.ClientGroup, AvailableClientDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Client.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Client.Name} {src.Client.LastName}"));

            CreateMap<Domain.Entities.Exercise.Exercise, ExerciseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.MuscleGroup, opt => opt.MapFrom(src => src.MuscleGroup))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src => src.EncodedName))
                .ForMember(dest => dest.IsEditable, opt => opt.MapFrom(src => user != null && user.IsInRole("Admin")));

            CreateMap<CreateExerciseCommand, Domain.Entities.Exercise.Exercise>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.MuscleGroup, opt => opt.MapFrom(src => src.MuscleGroup))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src => src.EncodedName));

            CreateMap<ExerciseDto, EditExerciseCommand>();

            CreateMap<Domain.Entities.TrainingPlan.TrainingPlan, TrainingPlanDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src => src.EncodedName))
                .ForMember(dest => dest.TrainingPlanExercises, opt => opt.MapFrom(src => src.TrainingPlanExercises))
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups))
                .ForMember(dest => dest.IsEditable, opt => opt.MapFrom(src => user != null && user.IsInRole("Admin")))
                .ForMember(dest => dest.AvailableExercises, opt => opt.Ignore()); ;

            CreateMap<CreateTrainingPlanCommand, Domain.Entities.TrainingPlan.TrainingPlan>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src => src.EncodedName));

            CreateMap<TrainingPlanDto, EditTrainingPlanCommand>();

            CreateMap<TrainingPlanDto, DeleteTrainingPlanCommand>();
        }
    }
}