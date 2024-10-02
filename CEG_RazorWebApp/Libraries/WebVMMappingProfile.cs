using AutoMapper;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin;
using CEG_RazorWebApp.Models.Account.Create;
using CEG_RazorWebApp.Models.Course.Create;
using CEG_RazorWebApp.Models.Course.Get;
using CEG_RazorWebApp.Models.Account.Get;
using CEG_RazorWebApp.Models.Course.Update;
using CEG_RazorWebApp.Models.Homework.Create;
using CEG_RazorWebApp.Models.Homework.Get;
using CEG_RazorWebApp.Models.Session.Create;
using CEG_RazorWebApp.Models.Session.Get;
using CEG_RazorWebApp.Models.Session.Update;
using CEG_RazorWebApp.Models.Class.Get;

namespace CEG_RazorWebApp.Libraries
{
    public class WebVMMappingProfile : Profile
    {
        public WebVMMappingProfile()
        {
            /*CreateMap<User, UserViewModel>()
                *//*.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Member.FullName))*//*
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.AllowNull();
                    opt.MapFrom(src => src.MemberDetail != null ? src.MemberDetail.Email : "");
                })
                .ReverseMap();
            CreateMap<Member, MemberViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.UserId = src.UserDetail.UserId;
                    if (src.UserDetail != null && src.UserDetail.ImagePath != null)
                    {
                        dest.ImagePath = src.UserDetail.ImagePath;
                    }
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.UserDetail = new();
                    dest.UserDetail.ImagePath = src.ImagePath;
                })
                ;
            CreateMap<Member, GetMemberStatus>().ReverseMap();
            CreateMap<Member, GetMembershipExpire>().ReverseMap();
            CreateMap<MeetingParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.MeetingId;
                    dest.EventIdFull = "meeting" + src.MeetingId;
                    dest.EventName = src.MeetingDetail.MeetingName;
                    dest.EventType = "Meeting";
                    dest.EventHost = src.MeetingDetail.Host;
                    dest.EventStaff = src.MeetingDetail.Incharge;
                    dest.StartDate = src.MeetingDetail.StartDate;
                    dest.EndDate = src.MeetingDetail.EndDate;
                    dest.Fee = 0;
                    dest.RegistrationDeadline = src.MeetingDetail.RegistrationDeadline;
                    dest.Status = src.MeetingDetail.Status;
                    dest.ParticipationNo = int.Parse(src.ParticipantNo);
                    dest.CheckInStatus = src.CheckInStatus;
                })
                .ReverseMap();
            *//*.AfterMap((src, dest) =>
            {
                dest.MeetingId = src.MeetingId;
                dest.MeetingName = src.Meeting.MeetingName;
                dest.StartDate = src.Meeting.StartDate;
                dest.EndDate = src.Meeting.EndDate;
                dest.RegistrationDeadline = src.Meeting.RegistrationDeadline;
                dest.Status = src.Meeting.Status == 0 ? "Inactive" : "Active";
                dest.ParticipationNo = Int32.Parse(src.ParticipantNo);
                dest.Incharge = src.Meeting.Incharge;
            })*//*
            ;
            CreateMap<FieldTripParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.TripId;
                    dest.EventIdFull = "fieldtrip" + src.TripId;
                    dest.EventName = src.Trip.TripName;
                    dest.EventType = "FieldTrip";
                    dest.EventHost = src.Trip.Host;
                    dest.EventStaff = src.Trip.InCharge;
                    dest.StartDate = src.Trip.StartDate;
                    dest.EndDate = src.Trip.EndDate;
                    dest.RegistrationDeadline = src.Trip.RegistrationDeadline;
                    dest.Status = src.Trip.Status;
                    dest.Fee = src.Trip.Fee;
                    dest.ParticipationNo = int.Parse(src.ParticipantNo);
                    dest.CheckInStatus = src.CheckInStatus;
                })
                .ReverseMap();
            CreateMap<ContestParticipant, GetEventParticipation>()
                .AfterMap((src, dest) =>
                {
                    dest.EventId = src.ContestId;
                    dest.EventIdFull = "contest" + src.ContestId;
                    dest.EventName = src.ContestDetail.ContestName;
                    dest.EventType = "Contest";
                    dest.EventHost = src.ContestDetail.Host;
                    dest.EventStaff = src.ContestDetail.Incharge;
                    dest.StartDate = src.ContestDetail.StartDate;
                    dest.EndDate = src.ContestDetail.EndDate;
                    dest.RegistrationDeadline = src.ContestDetail.RegistrationDeadline;
                    dest.Status = src.ContestDetail.Status;
                    dest.Fee = src.ContestDetail.Fee;
                    dest.ParticipationNo = int.Parse(src.ParticipantNo);
                    dest.CheckInStatus = src.CheckInStatus;
                })
                .ReverseMap();
            CreateMap<MeetingParticipant, MeetingParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.MemberDetail.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberDetail = new();
                    dest.MemberDetail.FullName = src.MemberName;
                })
                ;
            CreateMap<FieldTripParticipant, FieldTripParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.MemberName = src.MemberDetail.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberDetail = new();
                    dest.MemberDetail.FullName = src.MemberName;
                });
            CreateMap<ContestParticipant, ContestParticipantViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.ContestElo = src.Elo;
                    if (src.BirdDetails != null)
                    {
                        dest.ParticipantElo = src.BirdDetails.Elo;
                    }
                    dest.MemberName = src.MemberDetail.FullName;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.MemberDetail = new();
                    if (src.ContestElo != null)
                    {
                        dest.Elo = src.ContestElo.Value;
                    }
                    if (dest.BirdDetails != null)
                    {
                        dest.BirdDetails.Elo = src.ParticipantElo;
                    }
                    dest.MemberDetail.FullName = src.MemberName;
                });
            CreateMap<Meeting, MeetingViewModel>()
                .ReverseMap();
            CreateMap<MeetingMedia, MeetingMediaViewModel>()
                .ReverseMap();
            CreateMap<FieldTrip, FieldTripViewModel>()
                .ReverseMap();
            CreateMap<FieldtripDaybyDay, FieldtripDaybyDayViewModel>()
                .ReverseMap();
            CreateMap<FieldtripInclusion, FieldtripInclusionViewModel>()
                .ReverseMap();
            CreateMap<FieldtripGettingThere, FieldtripGettingThereViewModel>()
                .ReverseMap();
            CreateMap<FieldtripAdditionalDetail, FieldTripAdditionalDetailViewModel>()
                .ReverseMap();
            CreateMap<FieldtripMedia, FieldtripMediaViewModel>()
                .ReverseMap();
            CreateMap<Contest, ContestViewModel>()
                .ReverseMap();
            CreateMap<ContestMedia, ContestMediaViewModel>()
                .ReverseMap();
            CreateMap<Location, LocationViewModel>()
                .AfterMap((src, dest) =>
                {
                    string[] address = src.LocationName.Split(',');
                    dest.AreaNumber = address[0];
                    dest.Street = address[1];
                    dest.District = address[2];
                    dest.City = address[3];
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.LocationName = src.AreaNumber + "," + src.Street + "," + src.District + "," + src.City;
                });
            CreateMap<Transaction, TransactionViewModel>()
                .ReverseMap();
            CreateMap<Bird, BirdViewModel>().ReverseMap();
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();*/
            CreateMap<CreateAccountVM, CreateNewAccount>()
                .ReverseMap();
            CreateMap<CreateParentVM, CreateNewParent>()
                .AfterMap((src, dest) =>
                {
                    dest.Account.Role = "Parent";
                })
                .ReverseMap();
            CreateMap<CreateTeacherVM, CreateNewTeacher>()
                .AfterMap((src, dest) =>
                {
                    dest.Account.Role = "Teacher";
                })
                .ReverseMap();
            CreateMap<CreateStudentVM, CreateNewStudent>()
                .AfterMap((src, dest) =>
                {
                    dest.Highscore = 0;
                    dest.Account.Role = "Student";
                })
                .ReverseMap();
            CreateMap<AccountStatusVM, AccountViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.Role.RoleName = src.Role;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.Role = src.Role.RoleName;
                });
            CreateMap<IndexCourseInfoVM, CourseViewModel>()
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.SessionsAmount = src.Sessions.Count;
                    dest.ClassesAmount = src.Classes.Count;
                });
            CreateMap<CreateCourseVM, CreateNewCourse>()
                .AfterMap((src, dest) =>
                {
                    dest.Image = src.CourseImageHeader;
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.CourseImageHeader = src.Image;
                });
            CreateMap<CourseInfoVM, CourseViewModel>()
                .AfterMap((src, dest) =>
                {
                })
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.SessionsAmount = (src.Sessions != null || src.Sessions.Any()) ? src.Sessions.Count : 0;
                    dest.ClassesAmount = (src.Classes != null || src.Classes.Any()) ? src.Classes.Count : 0;
                });
            CreateMap<UpdateCourseVM, CourseViewModel>()
                .ReverseMap();
            CreateMap<UpdateSessionVM, SessionViewModel>()
                .ReverseMap();
            CreateMap<SessionInfoVM, SessionViewModel>()
                .ReverseMap().AfterMap((src, dest) =>
                {
                    dest.HomeworksAmount = (src.Homeworks != null || src.Homeworks.Any()) ? src.Homeworks.Count : 0;
                });
            CreateMap<HomeworkInfoVM, HomeworkViewModel>()
                .ReverseMap();
            CreateMap<CreateSessionVM, CreateNewSession>()
                .ReverseMap();
            CreateMap<CreateHomeworkVM, CreateNewHomework>()
                .ReverseMap();
            CreateMap<IndexClassInfoVM, ClassViewModel>()
                .ReverseMap();
                //.AfterMap((src, dest) =>
                //{
                    //dest.TeacherName = src.Teacher.Account.Fullname;
                    //dest.CourseName = src.Course.CourseName;
                //});
        }
    }
}
