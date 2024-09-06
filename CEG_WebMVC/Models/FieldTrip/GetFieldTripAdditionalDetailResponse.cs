﻿using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.FieldTrip
{
    public class GetFieldTripAdditionalDetailResponse : DefaultResponseViewModel<object>
    {
        public GetFieldTripAdditionalDetailResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
