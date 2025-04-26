
public class RecruitmentProfile : Profile
{
    public RecruitmentProfile()
    {
        CreateMap<SubscriptionPackageDto, SubscriptionPackage>()
            .ForMember(dest => dest.Id, opt
                => opt.Condition(src => src.PackageId != default));

        CreateMap<SubscriptionPackage, SubscriptionPackageDto>()
            .ForMember(dest => dest.PackageId, opt
                => opt.MapFrom(x => x.Id));

        CreateMap<CompanyProfileUpdateDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.CompanyId))
            .ForMember(dest => dest.LegalName, opt => opt.MapFrom(x => x.LegalName))
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(x => x.DisplayName))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(x => x.EmailAddress))
            .ForMember(dest => dest.WebsiteLink, opt => opt.MapFrom(x => x.WebsiteLink))
            .ForMember(dest => dest.CompanyLogo, opt => opt.MapFrom(x => x.CompanyLogo))
            .ForMember(dest => dest.About, opt => opt.MapFrom(x => x.About))
            .ForMember(dest => dest.IsGetActiveJobNotification, opt => opt.MapFrom(x => x.IsGetActiveJobNotification))
            .ForMember(dest => dest.IsGetActionRequiredNotification, opt => opt.MapFrom(x => x.IsGetActionRequiredNotification))
            .ForMember(dest => dest.IsGetInvitationAcceptedNotification, opt => opt.MapFrom(x => x.IsGetInvitationAcceptedNotification))
            .ForMember(dest => dest.IsGetInvitationAcceptedNotification, opt => opt.MapFrom(x => x.IsGetInvitationAcceptedNotification))
            .ForMember(dest => dest.CompanyPointOfContact, opt => opt.MapFrom(x => x.CompanyPointOfContact))
            .ForMember(dest => dest.Serial, src => src.Ignore())
            .ForMember(dest => dest.DialCode, src => src.Ignore())
            .ForMember(dest => dest.ContactNumber, src => src.Ignore())
            .ForMember(dest => dest.CompanyCategoryId, src => src.Ignore())
            .ForMember(dest => dest.CompanyCategory, src => src.Ignore())
            .ForMember(dest => dest.TradeLicense, src => src.Ignore())
            .ForMember(dest => dest.ApplicationDate, src => src.Ignore())
            .ForMember(dest => dest.ApplicationStatus, src => src.Ignore())
            .ForMember(dest => dest.User, src => src.Ignore())
            .ForMember(dest => dest.UserId, src => src.Ignore())
            .ForMember(dest => dest.JobPosts, src => src.Ignore())
            .ForMember(dest => dest.CompanyJobPayments, src => src.Ignore());

        CreateMap<Company, CompanyProfileUpdateDto>()
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(x => x.Id));

        CreateMap<CompanyDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.CompanyId != default));

        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.CompanyId, opt
                => opt.MapFrom(x => x.Id));

        CreateMap<CompanyPointOfContactDto, CompanyPointOfContact>()
            .ForMember(dest => dest.Id, opt
                => opt.Condition(src => src.PointOfContactId != default));

        CreateMap<CompanyPointOfContact, CompanyPointOfContactDto>()
            .ForMember(dest => dest.PointOfContactId, opt
                => opt.MapFrom(x => x.Id));

        /*Candidate Pool Start*/
        CreateMap<PoolDto, Pool>()
            .ForMember(dest => dest.PoolApplications, src => src.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != default))
            .ReverseMap();

        CreateMap<PoolCourseDto, PoolCourse>().ReverseMap();

        CreateMap<CourseDto, Course>().ReverseMap();

        CreateMap<PoolApplicationDto, PoolApplication>()
            .ForMember(dest => dest.Pool, src => src.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != default))
            .ReverseMap();

        CreateMap<PoolApplicationEducationDto, PoolApplicationEducation>().ReverseMap();
        CreateMap<PoolApplicationTrainingDto, PoolApplicationTraining>().ReverseMap();
        CreateMap<PoolApplicationExperienceDto, PoolApplicationExperience>().ReverseMap();
        CreateMap<PoolApplicationSkillDto, PoolApplicationSkill>().ReverseMap();

        CreateMap<PoolApplication, PoolProfileDto>().ForMember(d => d.Id, src => src.Ignore());
        CreateMap<PoolApplicationEducation, EducationDto>().ForMember(d => d.Id, src => src.Ignore());
        CreateMap<PoolApplicationExperience, ExperienceDto>().ForMember(d => d.Id, src => src.Ignore());
        CreateMap<PoolApplicationTraining, TrainingDto>().ForMember(d => d.Id, src => src.Ignore());
        CreateMap<PoolApplicationSkill, SkillDto>().ForMember(d => d.Id, src => src.Ignore());
        /*Candidate Pool End*/

        CreateMap<JobPostDto, JobPost>()
            .ForMember(dest => dest.Id, opt
                => opt.Condition(src => src.Id != default));

        CreateMap<JobPostSkillTagDto, JobPostSkillTag>()
            .ReverseMap();

        CreateMap<JobPost, JobPostDto>();

        CreateMap<(IList<SubscriptionPackageDto>, SubscriptionPackageDto), (IList<SubscriptionPackage>, SubscriptionPackage)>()
            .ReverseMap();

        CreateMap<PackagePurchase, PackagePurchaseDto>()
          .ReverseMap();

        CreateMap<CandidateInvitation, CandidateInvitationDto>()
           .ReverseMap();

        CreateMap<JobApplication, JobApplicationDto>()
           .ReverseMap();

        CreateMap<JobInterviewInvitation, JobInterviewInvitationDto>()
            .ReverseMap();

        CreateMap<JobPostHireOffer, JobPostHireOfferDto>()
            .ReverseMap();

        CreateMap<SkillTypeDto, SkillType>()
            .ForMember(dest => dest.Id,
                opt => opt.Condition(x => x.Id != default))
            .ReverseMap();

        CreateMap<SkillTagDto, SkillTag>()
            .ForMember(dest => dest.Id,
                opt => opt.Condition(x => x.Id != default))
            .ReverseMap();

        CreateMap<PoolUser, PoolUserDto>().ReverseMap();


        #region Pool-Profile-Mapper-Configs
        CreateMap<PoolProfile, PoolProfileBO>();
        CreateMap<PoolProfileDto, PoolProfile>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CandidatePoolProfileDto, PoolProfile>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<CompanyFavoriteCandidate, CompanyFavoriteCandidateDto>();

        CreateMap<EducationDto, Education>().ReverseMap();
        CreateMap<ExperienceDto, Experience>().ReverseMap();
        CreateMap<TrainingDto, Training>().ReverseMap();
        CreateMap<SkillDto, Skill>().ReverseMap();
        CreateMap<ProfileReferenceDto, ProfileReference>().ReverseMap();
        CreateMap<PersonalProjectDto, PersonalProject>().ReverseMap();
        CreateMap<ApplicationUser, MemberProfileDto>();
        CreateMap<PoolProfileExpertiseDto, PoolProfileExpertise>().ReverseMap();
        CreateMap<AreaOfExpertise, AreasOfExpertiseDto>().ReverseMap();
        #endregion

        #region Job-Post-Mapper-Configs
        CreateMap<JobPost, JobPostBaseDto>()
            .ForMember(dest => dest.JobType, opt => opt.MapFrom(x => Enum.GetName(typeof(JobType), x.JobType)))
            .ForMember(dest => dest.JobLocationType, opt => opt.MapFrom(x => Enum.GetName(typeof(JobLocationType), x.JobLocationType)))
            .ForMember(dest => dest.JobResponsibilities,
                opt => opt.MapFrom(x => JsonConvert.DeserializeObject<List<object>>(x.JobResponsibilities)))
            .ForMember(dest => dest.JobRequirements,
                opt => opt.MapFrom(x => JsonConvert.DeserializeObject<List<string>>(x.JobRequirements)))
            .ForMember(dest => dest.Benefits, opt =>
                opt.MapFrom(x => JsonConvert.DeserializeObject<List<string>>(x.Benefits)));

        CreateMap<JobPostCreateDto, JobPost>()
            .ForMember(dest => dest.JobResponsibilities, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.JobResponsibilities)))
            .ForMember(dest => dest.JobRequirements, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.JobRequirements)))
            .ForMember(dest => dest.Benefits, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.Benefits)))
            .ForMember(dest => dest.JobPostNumber, opt => opt.Ignore())
            .ForMember(dest => dest.PublishingDate, opt => opt.Condition(x => x.PublishingDate != DateTime.MinValue))
            .ForMember(dest => dest.Deadline, opt => opt.Condition(x => x.Deadline != DateTime.MinValue))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<JobPost, MemberJobPostDto>()
            .ForMember(dest => dest.FavoriteJobs, opt => opt.MapFrom(x => x.FavouriteJobs))
            .ForMember(dest => dest.JobType, opt => opt.MapFrom(x => Enum.GetName(typeof(JobType), x.JobType)))
            .ForMember(dest => dest.JobLocation, opt => opt.MapFrom(x => x.JobLocationType))
            .ForMember(dest => dest.JobLocationType, opt => opt.MapFrom(x => Enum.GetName(typeof(JobLocationType), x.JobLocationType)))
            .ForMember(dest => dest.JobResponsibilities,
                opt => opt.MapFrom(x => JsonConvert.DeserializeObject<List<object>>(x.JobResponsibilities)))
            .ForMember(dest => dest.JobRequirements,
                opt => opt.MapFrom(x => JsonConvert.DeserializeObject<List<string>>(x.JobRequirements)))
            .ForMember(dest => dest.Benefits, opt =>
                opt.MapFrom(x => JsonConvert.DeserializeObject<List<string>>(x.Benefits)));

        CreateMap<ScheduledJobPostDto, ScheduledJobPost>();
        CreateMap<FavouriteJob, FavoriteJobsDto>();
        #endregion
    }
}