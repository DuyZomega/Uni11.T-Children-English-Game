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
                case var value when value.Equals(Constants.COURSE_STATUS_DRAFT):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_AVAILABLE,
                            Constants.COURSE_STATUS_DRAFT
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_AVAILABLE):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_AVAILABLE,
                            Constants.COURSE_STATUS_POSTPONED,
                            Constants.COURSE_STATUS_CANCELLED,
                            Constants.COURSE_STATUS_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_POSTPONED):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_POSTPONED,
                            Constants.COURSE_STATUS_AVAILABLE,
                            Constants.COURSE_STATUS_CANCELLED,
                            Constants.COURSE_STATUS_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_CANCELLED):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_CANCELLED,
                            Constants.COURSE_STATUS_AVAILABLE,
                            Constants.COURSE_STATUS_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_END_OF_SERVICE):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_AVAILABLE,
                            Constants.COURSE_STATUS_END_OF_SERVICE
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
                case var value when value.Equals(Constants.CLASS_STATUS_DRAFT):
                    {
                        validStatuses =
                        [
                            Constants.CLASS_STATUS_OPEN,
                            Constants.CLASS_STATUS_DRAFT
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.CLASS_STATUS_OPEN):
                    {
                        validStatuses =
                        [
                            Constants.CLASS_STATUS_OPEN,
                            Constants.CLASS_STATUS_POSTPONED,
                            Constants.CLASS_STATUS_CANCELLED,
                            Constants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.CLASS_STATUS_POSTPONED):
                    {
                        validStatuses =
                        [
                            Constants.CLASS_STATUS_POSTPONED,
                            Constants.CLASS_STATUS_OPEN,
                            Constants.CLASS_STATUS_CANCELLED,
                            Constants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.CLASS_STATUS_CANCELLED):
                    {
                        validStatuses =
                        [
                            Constants.CLASS_STATUS_CANCELLED,
                            Constants.CLASS_STATUS_OPEN,
                            Constants.CLASS_STATUS_ENDED
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.CLASS_STATUS_ENDED):
                    {
                        validStatuses =
                        [
                            Constants.CLASS_STATUS_ENDED,
                            Constants.CLASS_STATUS_OPEN          
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
