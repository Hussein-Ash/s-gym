using AutoMapper;
using EvaluationBackend.DATA.DTOs.Courses;
using EvaluationBackend.DATA.DTOs.Exercises;
using EvaluationBackend.DATA.DTOs.Home;
using EvaluationBackend.DATA.DTOs.Message;
using EvaluationBackend.DATA.DTOs.Muscles;
using EvaluationBackend.DATA.DTOs.Offer;
using EvaluationBackend.DATA.DTOs.Sections;
using EvaluationBackend.DATA.DTOs.Sets;
using EvaluationBackend.DATA.DTOs.Subscription;
using EvaluationBackend.DATA.DTOs.User;
using EvaluationBackend.Entities;


namespace EvaluationBackend.Helpers
{
  public class UserMappingProfile : Profile
  {
    public UserMappingProfile()
    {


      CreateMap<AppUser, UserDto>()
      .ForMember(r => r.Role, src => src.MapFrom(src => src.Role));
      CreateMap<RegisterForm, AppUser>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      CreateMap<UpdateUserForm, AppUser>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      CreateMap<Section, SectionDto>();
      CreateMap<SectionForm, Section>();
      CreateMap<SectionUpdate, Section>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      CreateMap<Set, SetsDto>();
      CreateMap<SetsForm, Set>();
      CreateMap<SetsUpdate, Set>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      CreateMap<Course, CourseDto>()
      .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
      // .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
      .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.SectionId))
      .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.SectionName!.Name));
      CreateMap<CourseForm, Course>();
      CreateMap<CourseUpdate, Course>()
     .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


      CreateMap<DayExercise, DayExerciseDto>()
     .ForMember(dest => dest.Exercise, opt => opt.MapFrom(src => src.ExerciseName != null ? src.ExerciseName.Name : null))
     .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.ExerciseId != null ? src.ExerciseId : null))
     
     .ForMember(dest => dest.Exercise2, opt => opt.MapFrom(src => src.ExerciseName!.Exercice != null ? src.ExerciseName.Exercice.Name : null))
     .ForMember(dest => dest.Exercise2Id, opt => opt.MapFrom(src => src.ExerciseName!.ExerciseId != null ? src.ExerciseName.ExerciseId : null))

     .ForMember(dest => dest.Sets, opt => opt.MapFrom(src => src.Sets != null ? src.Sets.Name : null))
     .ForMember(dest => dest.SetsId, opt => opt.MapFrom(src => src.SetsId != null ? src.SetsId : null))

     .ForMember(dest => dest.Sets2, opt => opt.MapFrom(src => src.Sets!.ExerciseSet != null ? src.Sets.ExerciseSet.Name : null))
     .ForMember(dest => dest.Sets2Id, opt => opt.MapFrom(src => src.Sets!.SetId != null ? src.Sets.SetId : null))
     .ForMember(dest => dest.MuscleId, opt => opt.MapFrom(src => src.MuscleId != null ? src.MuscleId : null))

     .ForMember(dest => dest.Muscle, opt => opt.MapFrom(src => src.MuscleName != null ? src.MuscleName.Name : null));

      CreateMap<DayExerciseForm, DayExercise>();
      CreateMap<DayExerciseUpdate, DayExercise>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



      CreateMap<Offer, OfferDto>();
      CreateMap<OfferForm, Offer>();
      CreateMap<OfferUpdate, Offer>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      CreateMap<Muscle, MuscleDto>();
      CreateMap<MuscleForm, Muscle>();
      CreateMap<MuscleUpdate, Muscle>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      CreateMap<Message, MessageDto>();
      CreateMap<MessageForm, Message>();
      CreateMap<MessageUpdate, Message>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


      CreateMap<Day, DayDto>()
            .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises));

      CreateMap<DayForm, Day>();
      CreateMap<DayUpdate, Day>()
     .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises))
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      CreateMap<Exercise, ExerciseDto>()
      .ForMember(dest => dest.MuscleName, opt => opt.MapFrom(src => src.MuscleName!.Name));
      CreateMap<ExerciseForm, Exercise>();
      CreateMap<ExerciseUpdate, Exercise>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


      CreateMap<Subscription, SubDto>()
      .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.SectionName != null ? src.SectionName.Name : null))
      .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null))
      .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseName != null ? src.CourseName.Name : null))
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
      .ForMember(dest => dest.SubInfo, opt => opt.MapFrom(src => src.SubInfo));

      CreateMap<SubInfoForm, SubscriptionInfo>();
      CreateMap<SubscriptionInfo, SubInfoDto>();


      CreateMap<SubForm, Subscription>();
      CreateMap<SubCourseForm, Subscription>();


      CreateMap<SubUpdate, Subscription>()
         .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

      CreateMap<SubscriptionInfo, SubInfoForm>();

      CreateMap<GoldForm, SubscriptionInfo>()
          .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      CreateMap<SilverForm, SubscriptionInfo>()
          .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      CreateMap<BronzeForm, SubscriptionInfo>()
          .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));





    }
  }
}