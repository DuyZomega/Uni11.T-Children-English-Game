﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CEG_WebMVC.Libraries
{
    public static class Constants
    {
        public readonly static string ADMIN_URL = "/Admin/Index";
        public readonly static string STUDENT_URL = "/Student/Index";
        public readonly static string TEACHER_URL = "/Teacher/Index";
        public readonly static string PARENT_URL = "/Parent/Index";
        public readonly static string NOTFOUND_URL = "/Auth/NotFound";
        public readonly static string LOGIN_URL = "/Auth/Login";
        //public readonly static string NEW_MEMBER_CONFIRM_REGISTRATION_URL = "/Auth/SignUp";

        /*public readonly static string NEW_MEMBER_REGISTRATION_COOKIE = "New-Membership-Registration-Cookie";
        public readonly static string NEW_MEMBER_REGISTRATION_TRANSACTION_COOKIE = "New-Membership-Registration-Transaction-Cookie";
        public readonly static string MEMBER_FIELDTRIP_REGISTRATION_COOKIE = "Member-FieldTrip-Registration-Cookie";
        public readonly static string MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_COOKIE = "Member-FieldTrip-Registration-Transaction-Cookie";
        public readonly static string MEMBER_CONTEST_REGISTRATION_COOKIE = "Member-Contest-Registration-Cookie";
        public readonly static string MEMBER_CONTEST_BIRD_REGISTRATION_COOKIE = "Member-Contest-Bird-Registration-Cookie";
        public readonly static string MEMBER_CONTEST_REGISTRATION_TRANSACTION_COOKIE = "Member-Contest-Registration-Transaction-Cookie";
        public readonly static string MEMBERSHIP_RENEWAL_TRANSACTION_COOKIE = "Membership-Renewal-Transaction-Cookie";*/

        /*public readonly static string NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE = "New-Membership-Registration";
        public readonly static string MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_TYPE = "Member-FieldTrip-Registration";
        public readonly static string MEMBER_CONTEST_REGISTRATION_TRANSACTION_TYPE = "Member-Contest-Registration";
        public readonly static string MEMBERSHIP_RENEWAL_TRANSACTION_TYPE = "Membership-Renewal-Transaction-Type";*/

        public readonly static string ALERT_MEMBER_LOGIN_SUCCESS_NAME = "MemberLogin";
        public readonly static string ALERT_MEMBER_LOGIN_SUCCESS = "Login as Member successfully!";
        //public readonly static string ALERT_USER_AVATAR_IMAGE_CHANGE_SUCCESS_NAME = "UserAvatarImageChange";
        public readonly static string ALERT_USER_AVATAR_IMAGE_UPDATE_SUCCESS = "Successfully changed profile pic!";
        public readonly static string ALERT_USER_AVATAR_IMAGE_UPDATE_ERROR = "Change Profile avatar failed!";
        public readonly static string ALERT_USER_PASSWORD_UPDATE_SUCCESS = "Successfully changed user password!";
        public readonly static string ALERT_USER_PASSWORD_UPDATE_ERROR = "Change user password failed!";
        public readonly static string ALERT_MANAGER_CREATE_NEWS_SUCCESS = "Successfully create news!";
        public readonly static string ALERT_MANAGER_CREATE_NEWS_ERROR = "Create news failed!";
        public readonly static string ALERT_MEMBER_CREATE_BLOG_SUCCESS = "Successfully create your blog!";
        public readonly static string ALERT_MEMBER_CREATE_BLOG_ERROR = "Create your blog failed!";
        public readonly static string ALERT_DEFAULT_ERROR_NAME = "Error";
        public readonly static string ALERT_DEFAULT_SUCCESS_NAME = "Success";
        public readonly static string ALERT_DEFAULT_ERROR_CHECK = "ErrorCheck";
        public readonly static string ALERT_DEFAULT_SUCCESS_CHECK = "SuccessCheck";

        public readonly static string ACCOUNT_STATUS_TITLE = "Status";
        public readonly static string ACCOUNT_STATUS_INACTIVE = "Inactive";
        public readonly static string ACCOUNT_STATUS_ACTIVE = "Active";
        public readonly static string ACCOUNT_STATUS_EXPIRED = "Expired";
        public readonly static string ACCOUNT_STATUS_DENIED = "Denied";
        public readonly static string ACCOUNT_STATUS_SUSPENDED = "Suspended";

        public readonly static string COURSE_DIFFICULTY_TITLE = "Difficulty";
        public readonly static string COURSE_DIFFICULTY_BEGINNER = "Beginner";
        public readonly static string COURSE_DIFFICULTY_INTERMEDIATE = "Intermediate";
        public readonly static string COURSE_DIFFICULTY_ADVANCED = "Advanced";

        public readonly static string COURSE_CATEGORY_TITLE = "Category";
        public readonly static string COURSE_CATEGORY_PRONUNCIATION = "Pronunciation";
        public readonly static string COURSE_CATEGORY_GRAMMAR = "Grammar";
        public readonly static string COURSE_CATEGORY_VOCABULARY = "Vocabulary";
        public readonly static string COURSE_CATEGORY_SPELLING = "Spelling";

        /*public readonly static string NEWS_STATUS_DRAFT = "Draft";
        public readonly static string NEWS_STATUS_ACTIVE = "Active";
        public readonly static string NEWS_STATUS_HIDDEN = "Hidden";
        public readonly static string NEWS_STATUS_ARCHIVED = "Archived";
        public readonly static string NEWS_STATUS_REPORTED = "Reported";
        public readonly static string NEWS_STATUS_DISABLED = "Disabled";

        public readonly static string NEWS_CATEGORY_ANNOUNCEMENT = "Announcement";
        public readonly static string NEWS_CATEGORY_MEETING = "Meeting";
        public readonly static string NEWS_CATEGORY_FIELDTRIP = "Fieldtrip";
        public readonly static string NEWS_CATEGORY_CONTEST = "Contest";
        public readonly static string NEWS_CATEGORY_OTHERS = "Others";

        public readonly static string BLOG_STATUS_DRAFT = "Draft";
        public readonly static string BLOG_STATUS_ACTIVE = "Active";
        public readonly static string BLOG_STATUS_HIDDEN = "Hidden";
        public readonly static string BLOG_STATUS_ARCHIVED = "Archived";
        public readonly static string BLOG_STATUS_REPORTED = "Reported";
        public readonly static string BLOG_STATUS_DISABLED = "Disabled";

        public readonly static string BLOG_CATEGORY_ANNOUNCEMENT = "Announcement";
        public readonly static string BLOG_CATEGORY_MEETING = "Meeting";
        public readonly static string BLOG_CATEGORY_FIELDTRIP = "Fieldtrip";
        public readonly static string BLOG_CATEGORY_CONTEST = "Contest";
        public readonly static string BLOG_CATEGORY_OTHERS = "Others";*/

        /*public readonly static string CREATE_BIRD_VALID = "CBirdValid";
        public readonly static string CREATE_OR_UPDATE_BIRD_PROFILE_PICTURE_VALID = "CBirdProfilePicValid";
        public readonly static string UPDATE_BIRD_VALID = "UBirdValid";
        public readonly static string CREATE_MEETING_VALID = "CMeetingValid";
        public readonly static string UPDATE_MEETING_VALID = "UMeetingValid";
        public readonly static string UPDATE_MEETING_STATUS_VALID = "UMeetingStatusValid";
        public readonly static string CREATE_MEETING_MEDIA_VALID = "CMeetingMediaValid";
        public readonly static string UPDATE_MEETING_MEDIA_VALID = "UMeetingMediaValid";
        public readonly static string CREATE_FIELDTRIP_VALID = "CFieldTripValid";
        public readonly static string UPDATE_FIELDTRIP_VALID = "UFieldTripValid";
        public readonly static string UPDATE_FIELDTRIP_STATUS_VALID = "UFieldtripStatusValid";
        public readonly static string UPDATE_FIELDTRIP_GETTHERE_VALID = "UFieldTripGettingThereValid";
        public readonly static string CREATE_FIELDTRIP_DAYBYDAY_VALID = "CFieldTripDayByDayValid";
        public readonly static string UPDATE_FIELDTRIP_DAYBYDAY_VALID = "UFieldTripDayByDayValid";
        public readonly static string CREATE_FIELDTRIP_INCLUSION_VALID = "CFieldTripInclusionValid";
        public readonly static string UPDATE_FIELDTRIP_INCLUSION_VALID = "UFieldTripInclusionValid";
        public readonly static string CREATE_FIELDTRIP_TOURFEATURES_VALID = "CFieldTripTourFeaturesValid";
        public readonly static string UPDATE_FIELDTRIP_TOURFEATURES_VALID = "UFieldTripTourFeaturesValid";
        public readonly static string CREATE_FIELDTRIP_IMPORTANTTOKNOW_VALID = "CFieldTripImportantValid";
        public readonly static string UPDATE_FIELDTRIP_IMPORTANTTOKNOW_VALID = "UFieldTripImportantValid";
        public readonly static string CREATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID = "CFieldTripActAndTrasValid";
        public readonly static string UPDATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID = "UFieldTripActAndTrasValid";
        public readonly static string CREATE_FIELDTRIP_MEDIA_VALID = "CFieldTripMediaValid";
        public readonly static string UPDATE_FIELDTRIP_MEDIA_VALID = "UFieldTripMediaValid";
        public readonly static string CREATE_CONTEST_VALID = "CContestValid";
        public readonly static string UPDATE_CONTEST_VALID = "UContestValid";
        public readonly static string UPDATE_CONTEST_STATUS_VALID = "UContestStatusValid";
        public readonly static string CREATE_CONTEST_MEDIA_VALID = "CContestMediaValid";
        public readonly static string UPDATE_CONTEST_MEDIA_VALID = "UContestMediaValid";
        public readonly static string CREATE_CONTEST_PARTICIPATION_VALID = "CContestparticipantValid";
        public readonly static string CREATE_BLOG_VALID = "CBlogValid";
        public readonly static string UPDATE_BLOG_VALID = "UBlogValid";
        public readonly static string CREATE_NEWS_VALID = "CNewsValid";
        public readonly static string UPDATE_NEWS_VALID = "UNewsValid";*/

        public readonly static string CREATE_TEACHER_DETAILS_VALID = "CTeacherDetailsValid";
        public readonly static string CREATE_PARENT_DETAILS_VALID = "CParentDetailsValid";
        public readonly static string CREATE_STUDENT_DETAILS_VALID = "CStudentDetailsValid";

        /*public readonly static string UPDATE_MEMBER_DETAILS_VALID = "UMemberDetailsValid";
        public readonly static string UPDATE_MEMBER_PASSWORD_VALID = "UMemberPasswordValid";
        public readonly static string UPDATE_MANAGER_DETAILS_VALID = "UManagerDetailsValid";
        public readonly static string UPDATE_MANAGER_PASSWORD_VALID = "UManagerPasswordValid";
        public readonly static string UPDATE_STAFF_DETAILS_VALID = "UStaffDetailsValid";
        public readonly static string UPDATE_STAFF_PASSWORD_VALID = "UStaffPasswordValid";
        public readonly static string UPDATE_ADMIN_DETAILS_VALID = "UAdminDetailsValid";
        public readonly static string UPDATE_ADMIN_PASSWORD_VALID = "UAdminPasswordValid";*/

        /*public readonly static string EVENT_STATUS_ON_HOLD = "OnHold";
        public readonly static string EVENT_STATUS_NAME_ON_HOLD = "On Hold";
        public readonly static string EVENT_STATUS_POSTPONED = "Postponed";
        public readonly static string EVENT_STATUS_NAME_POSTPONED = "Postponed";
        public readonly static string EVENT_STATUS_CANCELLED = "Cancelled";
        public readonly static string EVENT_STATUS_NAME_CANCELLED = "Cancelled";
        public readonly static string EVENT_STATUS_ENDED = "Ended";
        public readonly static string EVENT_STATUS_NAME_ENDED = "Ended";
        public readonly static string EVENT_STATUS_OPEN_REGISTRATION = "OpenRegistration";
        public readonly static string EVENT_STATUS_NAME_OPEN_REGISTRATION = "Open Registration";
        public readonly static string EVENT_STATUS_CLOSED_REGISTRATION = "ClosedRegistration";
        public readonly static string EVENT_STATUS_NAME_CLOSED_REGISTRATION = "Closed Registration";
        public readonly static string EVENT_STATUS_CHECKING_IN = "CheckingIn";
        public readonly static string EVENT_STATUS_NAME_CHECKING_IN = "Checking In";
        public readonly static string EVENT_STATUS_ONGOING = "Ongoing";
        public readonly static string EVENT_STATUS_NAME_ONGOING = "Ongoing";*/

        public readonly static string COURSE_STATUS_DRAFT = "Draft";
        public readonly static string COURSE_STATUS_NAME_DRAFT = "Draft";
        public readonly static string COURSE_STATUS_AVAILABLE = "Available";
        public readonly static string COURSE_STATUS_NAME_AVAILABLE = "Available";
        public readonly static string COURSE_STATUS_POSTPONED = "Postponed";
        public readonly static string COURSE_STATUS_NAME_POSTPONED = "Postponed";
        public readonly static string COURSE_STATUS_CANCELLED = "Cancelled";
        public readonly static string COURSE_STATUS_NAME_CANCELLED = "Cancelled";
        public readonly static string COURSE_STATUS_END_OF_SERVICE = "EndofService";
        public readonly static string COURSE_STATUS_NAME_END_OF_SERVICE = "End of Service";

        public readonly static string SESSION_STATUS_DRAFT = "Draft";
        public readonly static string SESSION_STATUS_NAME_DRAFT = "Draft";
        public readonly static string SESSION_STATUS_AVAILABLE = "Available";
        public readonly static string SESSION_STATUS_NAME_AVAILABLE = "Available";
        public readonly static string SESSION_STATUS_POSTPONED = "Postponed";
        public readonly static string SESSION_STATUS_NAME_POSTPONED = "Postponed";
        public readonly static string SESSION_STATUS_CANCELLED = "Cancelled";
        public readonly static string SESSION_STATUS_NAME_CANCELLED = "Cancelled";
        public readonly static string SESSION_STATUS_END_OF_SERVICE = "EndofService";
        public readonly static string SESSION_STATUS_NAME_END_OF_SERVICE = "End of Service";

        public readonly static string CLASS_STATUS_DRAFT = "Draft";
        public readonly static string CLASS_STATUS_NAME_DRAFT = "Draft";
        public readonly static string CLASS_STATUS_AVAILABLE = "Available";
        public readonly static string CLASS_STATUS_NAME_AVAILABLE = "Available";
        public readonly static string CLASS_STATUS_POSTPONED = "Postponed";
        public readonly static string CLASS_STATUS_NAME_POSTPONED = "Postponed";
        public readonly static string CLASS_STATUS_CANCELLED = "Cancelled";
        public readonly static string CLASS_STATUS_NAME_CANCELLED = "Cancelled";
        public readonly static string CLASS_STATUS_ENDED = "Ended";
        public readonly static string CLASS_STATUS_NAME_ENDED = "Ended";

        /*public readonly static string COURSE_STATUS_ENDED = "Ended";
        public readonly static string COURSE_STATUS_NAME_ENDED = "Ended";
        public readonly static string COURSE_STATUS_OPEN_REGISTRATION = "OpenRegistration";
        public readonly static string COURSE_STATUS_NAME_OPEN_REGISTRATION = "Open Registration";
        public readonly static string COURSE_STATUS_CLOSED_REGISTRATION = "ClosedRegistration";
        public readonly static string COURSE_STATUS_NAME_CLOSED_REGISTRATION = "Closed Registration";
        public readonly static string COURSE_STATUS_CHECKING_IN = "CheckingIn";
        public readonly static string COURSE_STATUS_NAME_CHECKING_IN = "Checking In";
        public readonly static string COURSE_STATUS_ONGOING = "Ongoing";
        public readonly static string COURSE_STATUS_NAME_ONGOING = "Ongoing";*/

        public readonly static string COURSE_MEDIA_TYPE_SPOTLIGHT = "Spotlight";
        public readonly static string EVENT_MEDIA_TYPE_LOCATION_MAP = "LocationMap";
        public readonly static string EVENT_MEDIA_TYPE_ADDITIONAL = "Additional";

        public readonly static int COURSE_AGE_REQ = 11;
        public readonly static int COURSE_TOTAL_HOURS = 1;
        public readonly static int EVENT_FIELDTRIP_MIN_PART_REQ = 10;
        public readonly static int EVENT_CONTEST_MIN_PART_REQ = 10;

       /* public readonly static string BIRD_STATUS_ACTIVE = "Active";
        public readonly static string BIRD_STATUS_INJURED = "Injured";
        public readonly static string BIRD_STATUS_INACTIVE = "Inactive";
        public readonly static string BIRD_STATUS_UNAVAILABLE = "Unavailable";*/

        public readonly static string REQUIRED_ELO_RANGE_DEFAULT_NAME = "Elo Range";
        public readonly static string REQUIRED_ELO_RANGE_DEFAULT = "500--3000";
        public readonly static string REQUIRED_ELO_RANGE_BELOW_1000_NAME = "Below 1000 Elo";
        public readonly static string REQUIRED_ELO_RANGE_BELOW_1000 = "500--1000";
        public readonly static string REQUIRED_ELO_RANGE_1000_TO_1500_NAME = "1000--1500 Elo";
        public readonly static string REQUIRED_ELO_RANGE_1000_TO_1500 = "1000--1500";
        public readonly static string REQUIRED_ELO_RANGE_1500_TO_2000_NAME = "1500--2000 Elo";
        public readonly static string REQUIRED_ELO_RANGE_1500_TO_2000 = "1500--2000";
        public readonly static string REQUIRED_ELO_RANGE_ABOVE_2000_NAME = "Above 2000 Elo";
        public readonly static string REQUIRED_ELO_RANGE_ABOVE_2000 = "2000--3000";

        /*public readonly static string EVENT_PARTICIPANT_STATUS_NOT_CHECKED_IN = "Not Checked-In";
        public readonly static string EVENT_PARTICIPANT_STATUS_CHECKED_IN = "Checked-In";

        public readonly static string FIELDTRIP_INCLUSION_TYPE_INCLUDED = "Included";
        public readonly static string FIELDTRIP_INCLUSION_TYPE_EXCLUDED = "Excluded";*/

        public readonly static string NOTIFICATION_TYPE_ACCOUNT_REGISTER = "Account Registration";
        public readonly static string NOTIFICATION_TYPE_MEETING_REGISTER = "Meeting Registration";
        public readonly static string NOTIFICATION_TYPE_FIELDTRIP_REGISTER = "Field Trip Registration";
        public readonly static string NOTIFICATION_TYPE_CONTEST_REGISTER = "Contest Registration";
        public readonly static string NOTIFICATION_TYPE_MEETING_DEREGISTER = "Meeting Deregistration";
        public readonly static string NOTIFICATION_TYPE_FIELDTRIP_DEREGISTER = "Field Trip Deregistration";
        public readonly static string NOTIFICATION_TYPE_CONTEST_DEREGISTER = "Contest Deregistration";

        public readonly static string NOTIFICATION_DESCRIPTION_ACCOUNT_REGISTER =
            "You have successfully joined ChaoMao Bird Club!";
        public readonly static string NOTIFICATION_DESCRIPTION_MEETING_REGISTER =
            "You have successfully registered in the meeting: ";
        public readonly static string NOTIFICATION_DESCRIPTION_FIELDTRIP_REGISTER =
            "You have successfully registered in the field trip: ";
        public readonly static string NOTIFICATION_DESCRIPTION_CONTEST_REGISTER =
            "You have successfully registered in the bird contest: ";
        public readonly static string NOTIFICATION_DESCRIPTION_MEETING_DEREGISTER =
            "You no longer register in the meeting: ";
        public readonly static string NOTIFICATION_DESCRIPTION_FIELDTRIP_DEREGISTER =
            "You no longer register in the field trip: ";
        public readonly static string NOTIFICATION_DESCRIPTION_CONTEST_DEREGISTER =
            "You no longer register in the contest: ";

        public readonly static string NOTIFICATION_STATUS_UNREAD = "Unread";
        public readonly static string NOTIFICATION_STATUS_READ = "Read";

        public readonly static string ROLE_NAME = "ROLE_NAME";
        public readonly static string ACC_TOKEN = "ACCESS_TOKEN";
        //public readonly static string USR_FULL_NAME = "USER_FULL_NAME";
        public readonly static string USR_ID = "USER_ID";
        public readonly static string USR_NAME = "USER_NAME";
        public readonly static string USR_IMAGE = "IMAGE_PATH";

        public readonly static string ADMIN = "Admin";
        public readonly static string TEACHER = "Teacher";
        public readonly static string PARENT = "Parent";
        public readonly static string STUDENT = "Student";
        public readonly static string TEMPUSER = "TempUser";
        public readonly static string GUEST = "Guest";
        /*public readonly static string STAFF = "Staff";
        public readonly static string MANAGER = "Manager";*/

        public readonly static string GENDER_TITLE = "Gender";
        public readonly static string GENDER_MALE = "Male";
        public readonly static string GENDER_FEMALE = "Female";
        public readonly static string GENDER_OTHER = "Other";

        public readonly static string GET_METHOD = "GET";
        public readonly static string POST_METHOD = "POST";
        public readonly static string PUT_METHOD = "PUT";
        public readonly static string DELETE_METHOD = "DELETE";

        public const string GOOGLE_CLIENT_ID = "Authentication:Google:ClientId";
        public const string GOOGLE_CLIENT_SECRET = "Authentication:Google:ClientSecret";

        public const string GOOGLE_ACCESS_TOKEN_KEY_NAME = ".Token.access_token";
        public const string GOOGLE_ACC_COOKIE = "New-Membership-Google-Registration-Cookie";

        public const string GOOGLE_REDIRECT_URI = "https://localhost:7005/Auth/GoogleResponse";
        public const string GOOGLE_REDIRECT_URI_PATH = "/Auth/GoogleResponse";

        public const string GOOGLE_GRANT_TYPE = "authorization_code";

        public const string GOOGLE_LINK_GET_TOKEN = "https://accounts.google.com/o/oauth2/token";
        public const string GOOGLE_LINK_GET_USER_INFO = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=";

        /*public const string JWT_SECRET_KEY = "Authentication:JWT:SecretKey";
        public const string JWT_VALID_ISSUER = "Authentication:JWT:ValidIssuer";
        public const string JWT_VALID_AUDIENCE = "Authentication:JWT:ValidAudience";*/
        public readonly static string SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH = "DefaultApiUrl:ConnectionString";
        public readonly static string SYSTEM_DEFAULT_API_URL_CONFIG_PATH = "DefaultApiUrl:ApiUrl";

        public readonly static string TEMP_FILE_LOCATION_FOLDER = "wwwroot/tempFiles";

        public readonly static string SYSTEM_DEFAULT_ACCOUNT_USR_NAME = "Authentication:DefaultSystemLogin:Username";
        public readonly static string SYSTEM_DEFAULT_ACCOUNT_USR_PASSWORD = "Authentication:DefaultSystemLogin:Password";

        public readonly static string SYSTEM_DEFAULT_AZURE_CONNECTION_STRING = "AzureStorage:BlobConnectionString";
        public readonly static string SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_URL = "AzureStorage:BlobDefaultFolderURL";
        public readonly static string SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_NAME = "AzureStorage:BlobContainerName";
        public readonly static string SYSTEM_DEFAULT_AZURE_BLOB_AVATAR_FOLDER_URL = "AzureStorage:BlobAvatarsContainerNamePath";
        public readonly static string SYSTEM_DEFAULT_AZURE_BLOB_NEWS_FOLDER_URL = "AzureStorage:BlobNewsContainerNamePath";
        public readonly static string SYSTEM_DEFAULT_AZURE_BLOB_BLOG_FOLDER_URL = "AzureStorage:BlobBlogContainerNamePath";
        public readonly static string SYSTEM_DEFAULT_AZURE_BLOB_MEETING_FOLDER_URL = "AzureStorage:BlobMeetingsContainerNamePath";
        public readonly static string SYSTEM_DEFAULT_AZURE_BLOB_FIELDTRIP_FOLDER_URL = "AzureStorage:BlobFieldTripsContainerNamePath";
        public readonly static string SYSTEM_DEFAULT_AZURE_BLOB_CONTEST_FOLDER_URL = "AzureStorage:BlobContestsContainerNamePath";
        public readonly static string SYSTEM_DEFAULT_AZURE_BLOB_BIRD_FOLDER_URL = "AzureStorage:BlobBirdsContainerNamePath";
    }
}
