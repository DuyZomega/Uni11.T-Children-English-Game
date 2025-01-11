using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Configurations
{
    public class CEG_BAL_Library
    {
        public CEG_BAL_Library()
        {

        }
        public static bool IsCourseNewStatusValid(string? currentStatus, string newStatus)
        {
            var validStatuses = Array.Empty<string>();
            if (currentStatus == null) return false;
            switch (currentStatus)
            {
                case var value when value.Equals(CEGConstants.COURSE_STATUS_DRAFT):
                    {
                        validStatuses =
                        [
                            CEGConstants.COURSE_STATUS_AVAILABLE,
                            CEGConstants.COURSE_STATUS_DRAFT
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.COURSE_STATUS_AVAILABLE):
                    {
                        validStatuses =
                        [
                            CEGConstants.COURSE_STATUS_AVAILABLE,
                            CEGConstants.COURSE_STATUS_POSTPONED,
                            CEGConstants.COURSE_STATUS_CANCELLED,
                            CEGConstants.COURSE_STATUS_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.COURSE_STATUS_POSTPONED):
                    {
                        validStatuses =
                        [
                            CEGConstants.COURSE_STATUS_POSTPONED,
                            CEGConstants.COURSE_STATUS_AVAILABLE,
                            CEGConstants.COURSE_STATUS_CANCELLED,
                            CEGConstants.COURSE_STATUS_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.COURSE_STATUS_CANCELLED):
                    {
                        validStatuses =
                        [
                            CEGConstants.COURSE_STATUS_CANCELLED,
                            /*Constants.COURSE_STATUS_AVAILABLE,
                            Constants.COURSE_STATUS_END_OF_SERVICE*/
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.COURSE_STATUS_END_OF_SERVICE):
                    {
                        validStatuses =
                        [
                            CEGConstants.COURSE_STATUS_AVAILABLE,
                            CEGConstants.COURSE_STATUS_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                default :
                    {
                        break;
                    }
            }
            return false;
        }
        public static bool IsClassNewStatusValid(string? currentStatus, string newStatus)
        {
            var validStatuses = Array.Empty<string>();
            if (currentStatus == null) return false;
            switch (currentStatus)
            {
                case var value when value.Equals(CEGConstants.CLASS_STATUS_DRAFT):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_OPEN,
                            CEGConstants.CLASS_STATUS_DRAFT
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.CLASS_STATUS_OPEN):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_OPEN,
                            CEGConstants.CLASS_STATUS_ONGOING,
                            CEGConstants.CLASS_STATUS_POSTPONED,
                            CEGConstants.CLASS_STATUS_CANCELLED,
                            CEGConstants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.CLASS_STATUS_ONGOING):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_ONGOING,
                            CEGConstants.CLASS_STATUS_POSTPONED,
                            CEGConstants.CLASS_STATUS_CANCELLED,
                            CEGConstants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.CLASS_STATUS_POSTPONED):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_POSTPONED,
                            CEGConstants.CLASS_STATUS_OPEN,
                            CEGConstants.CLASS_STATUS_ONGOING,
                            CEGConstants.CLASS_STATUS_CANCELLED,
                            CEGConstants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.CLASS_STATUS_CANCELLED):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_CANCELLED,
                            CEGConstants.CLASS_STATUS_OPEN,
                            CEGConstants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.CLASS_STATUS_ENDED):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_ENDED,
                            CEGConstants.CLASS_STATUS_OPEN          
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                default:
                    {
                        break;
                    }
            }
            return false;
        }
        public static bool IsScheduleNewStatusValid(string? currentStatus, string newStatus)
        {
            var validStatuses = Array.Empty<string>();
            if (currentStatus == null) return false;
            switch (currentStatus)
            {
                case var value when value.Equals(CEGConstants.SCHEDULE_STATUS_DRAFT):
                    {
                        validStatuses =
                        [
                            CEGConstants.SCHEDULE_STATUS_DRAFT,
                            CEGConstants.SCHEDULE_STATUS_UPCOMING
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.SCHEDULE_STATUS_UPCOMING):
                    {
                        validStatuses =
                        [
                            CEGConstants.SCHEDULE_STATUS_UPCOMING,
                            CEGConstants.SCHEDULE_STATUS_ONGOING,
                            CEGConstants.SCHEDULE_STATUS_CANCELLED,
                            CEGConstants.SCHEDULE_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.SCHEDULE_STATUS_ONGOING):
                    {
                        validStatuses =
                        [
                            CEGConstants.SCHEDULE_STATUS_ONGOING,
                            CEGConstants.SCHEDULE_STATUS_CANCELLED,
                            CEGConstants.SCHEDULE_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.SCHEDULE_STATUS_CANCELLED):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_CANCELLED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.SCHEDULE_STATUS_ENDED):
                    {
                        validStatuses =
                        [
                            CEGConstants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                default:
                    {
                        break;
                    }
            }
            return false;
        }

        /*public static bool IsClassNewWeeklyScheduleValid(string newSchedule)
        {
            var validSchedule = new List<string>()
            {
                Constants.CLASS_SCHEDULE_MONDAY_THURSDAY,
                Constants.CLASS_SCHEDULE_TUESDAY_FRIDAY,
                Constants.CLASS_SCHEDULE_WEDNESDAY_SATURDAY
            };
            if (validSchedule.Contains(newSchedule)) return true;
            return false;
        }*/

        public static bool IsEnrollNewStatusValid(string currentStatus, string newStatus)
        {
            var validStatuses = Array.Empty<string>();
            if (currentStatus == null) return false;
            switch (currentStatus)
            {
                case var value when value.Equals(CEGConstants.ENROLL_STATUS_ENROLLED):
                    {
                        validStatuses =
                        [
                            CEGConstants.ENROLL_STATUS_ENROLLED,
                            CEGConstants.ENROLL_STATUS_SUSPENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.ENROLL_STATUS_PENDING):
                    {
                        validStatuses =
                        [
                            CEGConstants.ENROLL_STATUS_PENDING,
                            CEGConstants.ENROLL_STATUS_ENROLLED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.ENROLL_STATUS_SUSPENDED):
                    {
                        validStatuses =
                        [
                            CEGConstants.ENROLL_STATUS_SUSPENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                default:
                    {
                        break;
                    }
            }
            return false;
        }

        public static bool IsAttendanceNewStatusValid(string currentStatus, string newStatus)
        {
            var validStatuses = Array.Empty<string>();
            if (currentStatus == null) return false;
            switch (currentStatus)
            {
                case var value when value.Equals(CEGConstants.ATTENDANCE_STATUS_ATTENDED):
                    {
                        validStatuses =
                        [
                            CEGConstants.ATTENDANCE_STATUS_ATTENDED,
                            CEGConstants.ATTENDANCE_STATUS_ABSENT
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(CEGConstants.ATTENDANCE_STATUS_ABSENT):
                    {
                        validStatuses =
                        [
                            CEGConstants.ATTENDANCE_STATUS_ATTENDED,
                            CEGConstants.ATTENDANCE_STATUS_ABSENT
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                default:
                    {
                        break;
                    }
            }
            return false;
        }
    }
}
