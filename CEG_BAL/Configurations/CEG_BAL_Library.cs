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
                        if (newStatus.Equals(Constants.COURSE_STATUS_AVAILABLE)) return true;
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_AVAILABLE):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_NAME_POSTPONED,
                            Constants.COURSE_STATUS_NAME_CANCELLED,
                            Constants.COURSE_STATUS_NAME_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_POSTPONED):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_NAME_AVAILABLE,
                            Constants.COURSE_STATUS_NAME_CANCELLED,
                            Constants.COURSE_STATUS_NAME_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_CANCELLED):
                    {
                        validStatuses =
                        [
                            Constants.COURSE_STATUS_NAME_AVAILABLE,
                            Constants.COURSE_STATUS_NAME_END_OF_SERVICE
                        ];
                        return validStatuses.Contains(newStatus);
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_NAME_END_OF_SERVICE):
                    {
                        if (newStatus.Equals(Constants.COURSE_STATUS_AVAILABLE)) return true;
                        break;
                    }
                default :
                    {
                        break;
                    }
            }
            return false;
        }
    }
}
