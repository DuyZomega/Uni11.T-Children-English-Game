using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Parent;
using CEG_BAL.ViewModels.Teacher.Transaction;
using CEG_BAL.ViewModels.Transaction;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
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
            CreateMap<Role, RoleViewModel>()
                .ReverseMap();
            CreateMap<Student, StudentViewModel>()
                .ReverseMap();
            CreateMap<Course, CourseViewModel>()
                .ReverseMap();
            CreateMap<Session, SessionViewModel>()
                .ReverseMap();
            CreateMap<Homework, HomeworkViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.QuestionsAmount = src.HomeworkQuestions != null ? src.HomeworkQuestions.Count : 0;
                })
                .ReverseMap();
            CreateMap<HomeworkQuestion, HomeworkQuestionViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.AnswersAmount = src.HomeworkAnswers != null ? src.HomeworkAnswers.Count : 0;
                })
                .ReverseMap();
            CreateMap<HomeworkAnswer, HomeworkAnswerViewModel>()
                .ReverseMap();
            CreateMap<Game, GameViewModel>()
                .ReverseMap();
            CreateMap<GameLevel, GameLevelViewModel>()
                .ReverseMap();

            // Account
            CreateMap<CreateNewAccount, Account>();
            CreateMap<Account, AccountViewModel>()
                .ReverseMap();

            // Teacher
            CreateMap<Teacher, TeacherViewModel>()
                .ReverseMap();
            CreateMap<CreateNewTeacher, Teacher>()
                .AfterMap((src, dest) =>
                {
                    dest.Account.CreatedDate = DateTime.Now;
                    dest.Account.Status = CEGConstants.ACCOUNT_STATUS_ACTIVE;
                    dest.Certificate = "not submited yet";
                    dest.Image = "anonymous";
                });
            CreateMap<Teacher, GetTeacherNameOption>()
                .AfterMap((src,dest) =>
                {
                    dest.TeacherName = src.Account.Fullname;
                });

            // Parent
            CreateMap<Parent, ParentViewModel>()
                .ReverseMap();
            CreateMap<Parent, GetParentNameOption>()
                .AfterMap((src, dest) =>
                {
                    dest.ParentName = src.Account.Fullname;
                });

            // Class
            CreateMapforDefaultViewCreateUpdateModel<Class, ClassViewModel, CreateNewClass, UpdateClass>();
            CreateMap<Class, GetClassForTransaction>();

            // Schedule
            CreateMap<Schedule, ScheduleViewModel>();
            CreateMap<CreateNewSchedule, Schedule>()
                .AfterMap((src, dest) =>
                {
                    dest.Status = CEGConstants.SCHEDULE_STATUS_DRAFT;
                    dest.StartTime = src.ScheduleDate.HasValue ? TimeOnly.FromDateTime(src.ScheduleDate.Value) : default;
                });
            CreateMap<UpdateSchedule, Schedule>()
                .AfterMap((src, dest) =>
                {
                    dest.StartTime = src.ScheduleDate.HasValue ? TimeOnly.FromDateTime(src.ScheduleDate.Value) : default;
                });

            // Attendance
            CreateMap<Attendance, AttendanceViewModel>();
            CreateMap<Attendance, GetStudentActivity>();

            // Enroll
            CreateMap<Enroll, EnrollViewModel>()
                .ReverseMap();
            CreateMap<CreateNewEnroll, Enroll>()
                .AfterMap((src, dest) =>
                {
                    dest.RegistrationDate = DateTime.Now;
                    dest.EnrolledDate = DateTime.Now;
                    dest.Status = CEGConstants.ENROLLMENT_STATUS_ENROLLED;
                });

            // Transaction
            CreateMap<Transaction, TransactionViewModel>();
            CreateMap<Transaction, EarningViewModel>()
                .AfterMap((src, dest) =>
                {
                    if (src.Description != null)
                    {
                        var desStr = src.Description.Split(',').ToList();
                        dest.PayerFullname = desStr[0].Substring(CEGConstants.TRANSACTION_PAYER_LABEL.Length);
                        dest.PaymentMethod = desStr[1].Substring(CEGConstants.TRANSACTION_METHOD_LABEL.Length);
                        dest.ReceiverFullname = desStr[2].Substring(CEGConstants.TRANSACTION_RECEIVER_LABEL.Length);
                        dest.ClassName = desStr[4].Substring(CEGConstants.TRANSACTION_DESCRIPTION_ASSIGNED_CLASS_NAME_LABEL.Length);
                        // dest.Description = desStr[5];
                        dest.Description = String.Empty;
                        desStr.RemoveRange(0, 5);
                        foreach (var str in desStr)
                        {
                            dest.Description += "," + str;
                        }
                    }
                });
            CreateMap<CreateTransaction, Transaction>()
                .AfterMap((src, dest) =>
                {
                    dest.TransactionDate = DateTime.Now;
                    dest.ConfirmDate = DateTime.Now;
                    dest.TransactionStatus = CEGConstants.TRANSACTION_STATUS_COMPLETED;
                });

            // Game Config
            CreateMapforDefaultViewCreateUpdateModel<GameConfig, GameConfigViewModel, CreateNewGameConfig, UpdateGameConfig>();

            // Student Answer
            CreateMapforDefaultViewCreateUpdateModel<StudentAnswer, StudentAnswerViewModel, CreateNewStudentAnswer, UpdateStudentAnswer>();

            // Student Progress
            CreateMapforDefaultViewCreateUpdateModel<StudentProgress, StudentProgressViewModel, CreateNewStudentProgress, UpdateStudentProgress>();

            // Student Homework
            CreateMapforDefaultViewCreateUpdateModel<StudentHomework, StudentHomeworkViewModel, CreateNewStudentHomework, UpdateStudentHomework>();

            // Homework Result
            CreateMapforDefaultViewCreateUpdateModel<HomeworkResult, HomeworkResultViewModel, CreateNewHomeworkResult, UpdateHomeworkResult>();
        }
        /// <summary>
        /// Configures mappings between four types for creating and updating models.
        /// The function sets up the following mappings:
        /// 1. **T1 to T2**: Maps the default model to the view model.
        /// 2. **T3 to T1**: Maps the create model to the default model (used for creating new records).
        /// 3. **T4 to T1**: Maps the update model to the default model (used for updating existing records).
        /// </summary>
        /// <typeparam name="T1">The default model type (used for CRUD operations).</typeparam>
        /// <typeparam name="T2">The view model type (used for displaying data).</typeparam>
        /// <typeparam name="T3">The create model type (used for creating new records).</typeparam>
        /// <typeparam name="T4">The update model type (used for updating existing records).</typeparam>
        private void CreateMapforDefaultViewCreateUpdateModel<T1, T2, T3, T4>()
        {
            // DefaultModel to ViewModel: Maps the default model (T1) to the view model (T2)
            CreateMap<T1, T2>();

            // CreateModel to DefaultModel: Maps the create model (T3) to the default model (T1)
            CreateMap<T3, T1>();

            // UpdateModel to DefaultModel: Maps the update model (T4) to the default model (T1)
            CreateMap<T4, T1>();
        }
    }
}
