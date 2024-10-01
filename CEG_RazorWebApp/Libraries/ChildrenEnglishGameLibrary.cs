using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Azure;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Libraries
{
    public class ChildrenEnglishGameLibrary
    {
        public ChildrenEnglishGameLibrary()
        {

        }
        public async Task<T?> CallMethodReturnObject<T>(
            HttpClient _httpClient,
            JsonSerializerOptions options,
            string methodName,
            string url,
            ILogger _logger,
            object inputType = null,
            string accessToken = null) where T : class
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (accessToken != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            if ((methodName.Equals(Constants.GET_METHOD) || methodName.Equals(Constants.DELETE_METHOD)) && inputType == null)
            {
                switch (methodName)
                {
                    case var value when value.Equals(Constants.GET_METHOD):
                        {
                            response = await _httpClient.GetAsync(url);
                            break;
                        }
                    case var value when value.Equals(Constants.DELETE_METHOD):
                        {
                            response = await _httpClient.DeleteAsync(url);
                            break;
                        }
                }
            }
            else if (inputType != null)
            {
                string json = JsonSerializer.Serialize(inputType, options);
                // sử dụng frombody để lấy dữ liệu
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                switch (methodName)
                {
                    case var value when value.Equals(Constants.POST_METHOD):
                        {
                            response = await _httpClient.PostAsync(url, content);
                            break;
                        }
                    case var value when value.Equals(Constants.PUT_METHOD):
                        {
                            response = await _httpClient.PutAsync(url, content);
                            break;
                        }
                }
            }
            string jsonResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error while processing your request!: " + response.StatusCode + "\t\nApi Url: " + url + "\t\nError Message: " + jsonResponse);
                var error = JsonSerializer.Deserialize<T>(jsonResponse, options);
                return error;
            };
            var result = JsonSerializer.Deserialize<T>(jsonResponse, options);
            _logger.LogInformation("Processing your request Successfully!: " + response.StatusCode + "\t\nApi Url: " + url + "\t\nSuccess Message: " + jsonResponse);
            return result;
        }

        public void SetCookie(HttpResponse response, string key, object inputType, CookieOptions cookieOptions, JsonSerializerOptions jsonOptions, int? expireTime = null)
        {
            string json = JsonSerializer.Serialize(inputType, jsonOptions);
            if (expireTime.HasValue)
            {
                CookieOptions privatecookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(expireTime.Value),
                    MaxAge = TimeSpan.FromMinutes(10),
                    Secure = true,
                    IsEssential = true,
                };
                response.Cookies.Append(key, json, privatecookieOptions);
            }
            else
                response.Cookies.Append(key, json, cookieOptions);
        }

        public async Task<T> GetCookie<T>(HttpRequest request, string key, JsonSerializerOptions jsonOptions) where T : class
        {
            string value = request.Cookies.FirstOrDefault(c => c.Key == key).Value;
            if (value == null) return null;
            var returnobject = JsonSerializer.Deserialize<T>(value, jsonOptions);
            return returnobject;
        }
        public void RemoveCookie(HttpResponse response, string key, CookieOptions cookieOptions, JsonSerializerOptions jsonOptions)
        {
            response.Cookies.Delete(key, cookieOptions);
        }
        public List<SelectListItem> GetGenderSelectableList(string genderName)
        {
            List<SelectListItem> defaultGenders = new();
            switch (genderName)
            {
                case var value when value.Equals(Constants.GENDER_MALE):
                    {
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_TITLE, Value = "" });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_MALE, Value = Constants.GENDER_MALE, Selected = true });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_FEMALE, Value = Constants.GENDER_FEMALE });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_OTHER, Value = Constants.GENDER_OTHER });
                        break;
                    }
                case var value when value.Equals(Constants.GENDER_FEMALE):
                    {
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_TITLE, Value = "" });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_MALE, Value = Constants.GENDER_MALE });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_FEMALE, Value = Constants.GENDER_FEMALE, Selected = true });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_OTHER, Value = Constants.GENDER_OTHER });
                        break;
                    }
                case var value when value.Equals(Constants.GENDER_OTHER):
                    {
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_TITLE, Value = "" });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_MALE, Value = Constants.GENDER_MALE });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_FEMALE, Value = Constants.GENDER_FEMALE });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_OTHER, Value = Constants.GENDER_OTHER, Selected = true });
                        break;
                    }
                case var value when value.Equals(Constants.GENDER_TITLE):
                    {
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_TITLE, Value = "", Selected = true });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_MALE, Value = Constants.GENDER_MALE });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_FEMALE, Value = Constants.GENDER_FEMALE });
                        defaultGenders.Add(new SelectListItem { Text = Constants.GENDER_OTHER, Value = Constants.GENDER_OTHER });
                        break;
                    }
            }
            return defaultGenders;
        }
        public List<SelectListItem> GetAccountStatusSelectableList(string statusName)
        {
            List<SelectListItem> defaultStatuses = new();
            switch (statusName)
            {
                case var value when value.Equals(Constants.ACCOUNT_STATUS_INACTIVE):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_TITLE, Value = "" });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_INACTIVE, Value = Constants.ACCOUNT_STATUS_INACTIVE, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_ACTIVE, Value = Constants.ACCOUNT_STATUS_ACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_EXPIRED, Value = Constants.ACCOUNT_STATUS_EXPIRED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_DENIED, Value = Constants.ACCOUNT_STATUS_DENIED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_SUSPENDED, Value = Constants.ACCOUNT_STATUS_SUSPENDED });
                        break;
                    }
                case var value when value.Equals(Constants.ACCOUNT_STATUS_ACTIVE):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_TITLE, Value = "" });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_INACTIVE, Value = Constants.ACCOUNT_STATUS_INACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_ACTIVE, Value = Constants.ACCOUNT_STATUS_ACTIVE, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_EXPIRED, Value = Constants.ACCOUNT_STATUS_EXPIRED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_DENIED, Value = Constants.ACCOUNT_STATUS_DENIED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_SUSPENDED, Value = Constants.ACCOUNT_STATUS_SUSPENDED });
                        break;
                    }
                case var value when value.Equals(Constants.ACCOUNT_STATUS_EXPIRED):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_TITLE, Value = "" });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_INACTIVE, Value = Constants.ACCOUNT_STATUS_INACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_ACTIVE, Value = Constants.ACCOUNT_STATUS_ACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_EXPIRED, Value = Constants.ACCOUNT_STATUS_EXPIRED, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_DENIED, Value = Constants.ACCOUNT_STATUS_DENIED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_SUSPENDED, Value = Constants.ACCOUNT_STATUS_SUSPENDED });
                        break;
                    }
                case var value when value.Equals(Constants.ACCOUNT_STATUS_DENIED):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_TITLE, Value = "" });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_INACTIVE, Value = Constants.ACCOUNT_STATUS_INACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_ACTIVE, Value = Constants.ACCOUNT_STATUS_ACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_EXPIRED, Value = Constants.ACCOUNT_STATUS_EXPIRED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_DENIED, Value = Constants.ACCOUNT_STATUS_DENIED, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_SUSPENDED, Value = Constants.ACCOUNT_STATUS_SUSPENDED });
                        break;
                    }
                case var value when value.Equals(Constants.ACCOUNT_STATUS_SUSPENDED):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_TITLE, Value = "" });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_INACTIVE, Value = Constants.ACCOUNT_STATUS_INACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_ACTIVE, Value = Constants.ACCOUNT_STATUS_ACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_EXPIRED, Value = Constants.ACCOUNT_STATUS_EXPIRED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_DENIED, Value = Constants.ACCOUNT_STATUS_DENIED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_SUSPENDED, Value = Constants.ACCOUNT_STATUS_SUSPENDED, Selected = true });
                        break;
                    }
                case var value when value.Equals(Constants.ACCOUNT_STATUS_TITLE):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_TITLE, Value = "", Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_INACTIVE, Value = Constants.ACCOUNT_STATUS_INACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_ACTIVE, Value = Constants.ACCOUNT_STATUS_ACTIVE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_EXPIRED, Value = Constants.ACCOUNT_STATUS_EXPIRED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_DENIED, Value = Constants.ACCOUNT_STATUS_DENIED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.ACCOUNT_STATUS_SUSPENDED, Value = Constants.ACCOUNT_STATUS_SUSPENDED });
                        break;
                    }
            }
            return defaultStatuses;
        }
        public List<SelectListItem> GetCourseDifficultySelectableList(string difficultyName)
        {
            List<SelectListItem> defaultDifficulties = new();
            switch (difficultyName)
            {
                case var value when value.Equals(Constants.COURSE_DIFFICULTY_BEGINNER):
                    {
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_BEGINNER, Value = Constants.COURSE_DIFFICULTY_BEGINNER, Selected = true });
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_INTERMEDIATE, Value = Constants.COURSE_DIFFICULTY_INTERMEDIATE });
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_ADVANCED, Value = Constants.COURSE_DIFFICULTY_ADVANCED });
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_DIFFICULTY_INTERMEDIATE):
                    {
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_BEGINNER, Value = Constants.COURSE_DIFFICULTY_BEGINNER });
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_INTERMEDIATE, Value = Constants.COURSE_DIFFICULTY_INTERMEDIATE, Selected = true });
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_ADVANCED, Value = Constants.COURSE_DIFFICULTY_ADVANCED });
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_DIFFICULTY_ADVANCED):
                    {
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_BEGINNER, Value = Constants.COURSE_DIFFICULTY_BEGINNER });
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_INTERMEDIATE, Value = Constants.COURSE_DIFFICULTY_INTERMEDIATE });
                        defaultDifficulties.Add(new SelectListItem { Text = Constants.COURSE_DIFFICULTY_ADVANCED, Value = Constants.COURSE_DIFFICULTY_ADVANCED, Selected = true });
                        break;
                    }
            }
            return defaultDifficulties;
        }
        public List<SelectListItem> GetCourseCategorySelectableList(string categoryName)
        {
            List<SelectListItem> defaultCategories = new();
            switch (categoryName)
            {
                case var value when value.Equals(Constants.COURSE_CATEGORY_MIDDLE_SCHOOL):
                    {
                        defaultCategories.Add(new SelectListItem { Text = Constants.COURSE_CATEGORY_MIDDLE_SCHOOL, Value = Constants.COURSE_CATEGORY_MIDDLE_SCHOOL, Selected = true });
                        defaultCategories.Add(new SelectListItem { Text = Constants.COURSE_CATEGORY_HIGH_SCHOOL, Value = Constants.COURSE_CATEGORY_HIGH_SCHOOL });
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_CATEGORY_HIGH_SCHOOL):
                    {
                        defaultCategories.Add(new SelectListItem { Text = Constants.COURSE_CATEGORY_MIDDLE_SCHOOL, Value = Constants.COURSE_CATEGORY_MIDDLE_SCHOOL });
                        defaultCategories.Add(new SelectListItem { Text = Constants.COURSE_CATEGORY_HIGH_SCHOOL, Value = Constants.COURSE_CATEGORY_HIGH_SCHOOL, Selected = true });
                        break;
                    }
            }
            return defaultCategories;
        }
        public List<SelectListItem> GetCourseStatusSelectableList(string statusName)
        {
            List<SelectListItem> defaultStatuses = new();
            switch (statusName)
            {
                case var value when value.Equals(Constants.COURSE_STATUS_DRAFT):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_DRAFT, Value = Constants.COURSE_STATUS_DRAFT, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_AVAILABLE, Value = Constants.COURSE_STATUS_AVAILABLE });
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_AVAILABLE):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_AVAILABLE, Value = Constants.COURSE_STATUS_AVAILABLE, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_POSTPONED, Value = Constants.COURSE_STATUS_POSTPONED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_CANCELLED, Value = Constants.COURSE_STATUS_CANCELLED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_END_OF_SERVICE, Value = Constants.COURSE_STATUS_END_OF_SERVICE });
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_POSTPONED):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_AVAILABLE, Value = Constants.COURSE_STATUS_AVAILABLE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_POSTPONED, Value = Constants.COURSE_STATUS_POSTPONED, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_CANCELLED, Value = Constants.COURSE_STATUS_CANCELLED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_END_OF_SERVICE, Value = Constants.COURSE_STATUS_END_OF_SERVICE });
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_CANCELLED):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_AVAILABLE, Value = Constants.COURSE_STATUS_AVAILABLE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_CANCELLED, Value = Constants.COURSE_STATUS_CANCELLED, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_END_OF_SERVICE, Value = Constants.COURSE_STATUS_END_OF_SERVICE });
                        break;
                    }
                case var value when value.Equals(Constants.COURSE_STATUS_NAME_END_OF_SERVICE):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_AVAILABLE, Value = Constants.COURSE_STATUS_AVAILABLE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.COURSE_STATUS_NAME_END_OF_SERVICE, Value = Constants.COURSE_STATUS_END_OF_SERVICE, Selected = true });
                        break;
                    }
            }
            return defaultStatuses;
        }
        public List<SelectListItem> GetSessionStatusSelectableList(string statusName)
        {
            List<SelectListItem> defaultStatuses = new();
            switch (statusName)
            {
                case var value when value.Equals(Constants.SESSION_STATUS_DRAFT):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_DRAFT, Value = Constants.SESSION_STATUS_DRAFT, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_AVAILABLE, Value = Constants.SESSION_STATUS_AVAILABLE });
                        break;
                    }
                case var value when value.Equals(Constants.SESSION_STATUS_AVAILABLE):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_AVAILABLE, Value = Constants.SESSION_STATUS_AVAILABLE, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_POSTPONED, Value = Constants.SESSION_STATUS_POSTPONED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_CANCELLED, Value = Constants.SESSION_STATUS_CANCELLED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_END_OF_SERVICE, Value = Constants.SESSION_STATUS_END_OF_SERVICE });
                        break;
                    }
                case var value when value.Equals(Constants.SESSION_STATUS_POSTPONED):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_AVAILABLE, Value = Constants.SESSION_STATUS_AVAILABLE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_POSTPONED, Value = Constants.SESSION_STATUS_POSTPONED, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_CANCELLED, Value = Constants.SESSION_STATUS_CANCELLED });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_END_OF_SERVICE, Value = Constants.SESSION_STATUS_END_OF_SERVICE });
                        break;
                    }
                case var value when value.Equals(Constants.SESSION_STATUS_CANCELLED):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_AVAILABLE, Value = Constants.SESSION_STATUS_AVAILABLE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_CANCELLED, Value = Constants.SESSION_STATUS_CANCELLED, Selected = true });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_END_OF_SERVICE, Value = Constants.SESSION_STATUS_END_OF_SERVICE });
                        break;
                    }
                case var value when value.Equals(Constants.SESSION_STATUS_NAME_END_OF_SERVICE):
                    {
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_AVAILABLE, Value = Constants.SESSION_STATUS_AVAILABLE });
                        defaultStatuses.Add(new SelectListItem { Text = Constants.SESSION_STATUS_NAME_END_OF_SERVICE, Value = Constants.SESSION_STATUS_END_OF_SERVICE, Selected = true });
                        break;
                    }
            }
            return defaultStatuses;
        }

        public T? GetValidationTempData<T>(
            ControllerBase context,
            ITempDataDictionary tempData,
            string tempDataName,
            string viewObjectName,
            JsonSerializerOptions jsonOptions
            ) where T : class
        {
            if (tempData.Peek(tempDataName) != null)
            {
                var objectForValidation = JsonSerializer.Deserialize<T>(tempData[tempDataName].ToString(), jsonOptions);
                tempData.Remove(tempDataName);
                context.TryValidateModel(objectForValidation, viewObjectName);
                return objectForValidation;
            }
            return null;
        }
        public T? GetValidationTempData<T>(
            PageModel context,
            ITempDataDictionary tempData,
            string tempDataName,
            string viewObjectName,
            JsonSerializerOptions jsonOptions
            ) where T : class
        {
            if (tempData.Peek(tempDataName) != null)
            {
                var objectForValidation = JsonSerializer.Deserialize<T>(tempData[tempDataName].ToString(), jsonOptions);
                tempData.Remove(tempDataName);
                context.TryValidateModel(objectForValidation, viewObjectName);
                return objectForValidation;
            }
            return null;
        }

        public List<T> GetValidationTempDataList<T>(
            ControllerBase context,
            ITempDataDictionary tempData,
            string tempDataNamePrefix,
            string viewObjectNamePrefix,
            JsonSerializerOptions jsonOptions
            ) where T : class
        {
            var list = tempData.Where(t => t.Key.StartsWith(tempDataNamePrefix + "_"));
            if (list != null)
            {
                List<T> result = new();
                foreach (var item in list)
                {
                    var objectForValidation = JsonSerializer.Deserialize<T>(item.Value.ToString(), jsonOptions);
                    result.Add(objectForValidation);
                    tempData.Remove(item.Key);
                    context.TryValidateModel(objectForValidation, viewObjectNamePrefix + "_" + item.Key.Split("_")[1]);
                }
                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
            return null;
        }

        public Dictionary<string, string>? GetValidationModelStateErrorMessageList<T>(
            ControllerBase context,
            ITempDataDictionary tempData,
            string tempDataNamePrefix,
            string viewObjectNamePrefix,
            JsonSerializerOptions jsonOptions
            ) where T : class
        {
            var list = tempData.Where(t => t.Key.StartsWith(tempDataNamePrefix + "_"));
            if (list.Count() > 0)
            {
                Dictionary<string, string> result = new();
                foreach (var item in list)
                {
                    var objectForValidation = JsonSerializer.Deserialize<T>(item.Value.ToString(), jsonOptions);
                    context.TryValidateModel(objectForValidation, viewObjectNamePrefix + "_" + item.Key.Split("_")[1]);
                    var listErrors = context.ModelState.FindKeysWithPrefix(viewObjectNamePrefix + "_" + item.Key.Split("_")[1]);
                    foreach (var erroritem in listErrors)
                    {
                        var errors = erroritem.Value.Errors;
                        if (errors != null)
                        {
                            if (errors.Count > 1)
                            {
                                string errorsList = "";
                                foreach (var error in errors)
                                {
                                    errorsList += error.ErrorMessage + ";";
                                }
                                result.Add(erroritem.Key, errorsList);
                            }
                            else
                            {
                                result.Add(erroritem.Key, errors.FirstOrDefault().ErrorMessage);
                            }
                        }
                    }
                }
                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
            return null;
        }
        public string? GetUrlStringIfUserSessionDataInValid(Controller context, string requireRole, bool mustBeRole = true)
        {
            string? accToken = context.HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (string.IsNullOrEmpty(accToken))
            {
                return Constants.LOGIN_URL;
            }

            string? role = context.HttpContext.Session.GetString(Constants.ROLE_NAME);
            if (string.IsNullOrEmpty(role))
            {
                return Constants.LOGIN_URL;
            }
            else if (!role.Equals(requireRole))
            {
                switch (role)
                {
                    case var value when value.Equals(Constants.STUDENT):
                        {
                            return Constants.STUDENT_URL;
                        }
                    case var value when value.Equals(Constants.PARENT):
                        {
                            return Constants.PARENT_URL;
                        }
                    case var value when value.Equals(Constants.TEACHER):
                        {
                            return Constants.TEACHER_URL;
                        }
                    case var value when value.Equals(Constants.ADMIN):
                        {
                            return Constants.ADMIN_URL;
                        }
                }
            }

            string? usrId = context.HttpContext.Session.GetString(Constants.USR_ID);
            if (string.IsNullOrEmpty(usrId))
            {
                return Constants.LOGIN_URL;
            }

            string? usrname = context.HttpContext.Session.GetString(Constants.USR_NAME);
            if (string.IsNullOrEmpty(usrname))
            {
                return Constants.LOGIN_URL;
            }
            string? imagepath = context.HttpContext.Session.GetString(Constants.USR_IMAGE);

            context.TempData[Constants.ROLE_NAME] = role;
            context.TempData[Constants.USR_NAME] = usrname;
            context.TempData[Constants.USR_IMAGE] = imagepath;

            return null;
        }
        public void InitTempData(PageModel context)
        {
            string? role = context.HttpContext.Session.GetString(Constants.ROLE_NAME);
            string? usrname = context.HttpContext.Session.GetString(Constants.USR_NAME);
            string? imagepath = context.HttpContext.Session.GetString(Constants.USR_IMAGE);

            context.TempData[Constants.ROLE_NAME] = role;
            context.TempData[Constants.USR_NAME] = usrname;
            context.TempData[Constants.USR_IMAGE] = imagepath;
        }
        public void SetUserDefaultData(Controller context)
        {
            string? accToken = context.HttpContext.Session.GetString(Constants.ACC_TOKEN);
            string? role = context.HttpContext.Session.GetString(Constants.ROLE_NAME);
            if (string.IsNullOrEmpty(role))
            {
                role = Constants.GUEST;
                context.HttpContext.Session.SetString(Constants.ROLE_NAME, role);
            }
            string? usrId = context.HttpContext.Session.GetString(Constants.USR_ID);
            string? usrname = context.HttpContext.Session.GetString(Constants.USR_NAME);
            string? imagepath = context.HttpContext.Session.GetString(Constants.USR_IMAGE);

            context.TempData[Constants.ACC_TOKEN] = accToken;
            context.TempData[Constants.ROLE_NAME] = role;
            context.TempData[Constants.USR_ID] = usrId;
            context.TempData[Constants.USR_NAME] = usrname;
            context.TempData[Constants.USR_IMAGE] = imagepath;
        }
        public void SetUserRoleGuest(Controller context)
        {
            string? role = context.HttpContext.Session.GetString(Constants.ROLE_NAME);
            if (string.IsNullOrEmpty(role))
            {
                role = Constants.GUEST;
            }
            context.TempData[Constants.ROLE_NAME] = role;
        }
        public void LogOut(Controller context)
        {
            context.HttpContext.Session.Clear();
            context.TempData[Constants.ACC_TOKEN] = null;
            context.TempData[Constants.ROLE_NAME] = null;
            context.TempData[Constants.USR_ID] = null;
        }
        public ITempDataDictionary SetValidationTempData<T>(ITempDataDictionary tempData, string tempDataName, T objectForSerialize, JsonSerializerOptions jsonOptions) where T : class
        {
            string validJson = JsonSerializer.Serialize(objectForSerialize, jsonOptions);
            tempData[tempDataName] = validJson;
            return tempData;
        }
        public ITempDataDictionary SetValidationTempDataWithId<T>(ITempDataDictionary tempData, string tempDataName, int objectId, T objectForSerialize, JsonSerializerOptions jsonOptions) where T : class
        {
            string validJson = JsonSerializer.Serialize(objectForSerialize, jsonOptions);
            tempData[tempDataName + "_" + objectId] = validJson;
            return tempData;
        }
        public void SetCookieForTempFile(HttpResponse response, string key, IFormFile inputType, CookieOptions cookieOptions, JsonSerializerOptions jsonOptions, int? expireTime = null)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Constants.TEMP_FILE_LOCATION_FOLDER);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            /*FileInfo fileInfo = new FileInfo(inputType.FileName);
            string fileName = inputType. + fileInfo.Extension;*/

            string fileNameWithPath = Path.Combine(path, inputType.FileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                try
                {
                    inputType.CopyTo(stream);
                }
                catch (Exception e)
                {
                    stream.Dispose();
                    throw;
                }
                finally
                {
                    stream.Dispose();
                }
            }
            string json = JsonSerializer.Serialize(fileNameWithPath, jsonOptions);
            if (expireTime.HasValue)
            {
                CookieOptions privatecookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(expireTime.Value),
                    MaxAge = TimeSpan.FromMinutes(10),
                    Secure = true,
                    IsEssential = true,
                };
                response.Cookies.Append(key, json, privatecookieOptions);
            }
            else
                response.Cookies.Append(key, json, cookieOptions);
        }
        public async Task<IFormFile> GetCookieForTempFile(HttpRequest request, string key, JsonSerializerOptions jsonOptions)
        {
            string value = request.Cookies.FirstOrDefault(c => c.Key == key).Value;
            if (value == null) return null;
            var filepath = JsonSerializer.Deserialize<string>(value, jsonOptions);
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                var ms = new MemoryStream();
                try
                {
                    return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
                catch (Exception e)
                {
                    stream.Dispose();
                    ms.Dispose();
                    throw;
                }
                finally
                {
                    ms.Dispose();
                    stream.Dispose();
                }
            }
        }
        public void RemoveCookieTempFile(HttpResponse response, string key, IFormFile inputType, CookieOptions cookieOptions)
        {
            response.Cookies.Delete(key, cookieOptions);
            string path = Path.Combine(Directory.GetCurrentDirectory(), Constants.TEMP_FILE_LOCATION_FOLDER);
            string fileNameWithPath = Path.Combine(path, inputType.FileName);
            if (File.Exists(fileNameWithPath))
            {
                File.Delete(fileNameWithPath);
            }
        }

        /* Getter
         * testmodel2.CreateFieldTrip = null;
        if (TempData.Peek(Constants.Constants.CREATE_FIELDTRIP_VALID) != null)
        {
            testmodel2.CreateFieldTrip = JsonSerializer.Deserialize<FieldTripViewModel>(TempData[Constants.Constants.CREATE_FIELDTRIP_VALID].ToString());
            TempData.Remove(Constants.Constants.CREATE_FIELDTRIP_VALID);
            TryValidateModel(testmodel2.CreateFieldTrip, "createFieldTrip");
        }*/
        /*
         * Setter
        if (!ModelState.IsValid)
            {
                string validJson = JsonSerializer.Serialize(createMeeting, options);
        TempData[Constants.Constants.CREATE_MEETING_VALID] = validJson;
                return RedirectToAction("ManagerMeeting");
        }*/
        /* Old Code
         * TempData["ValidationErrors"] = ModelState.Values.SelectMany(v => v.Errors.Select(c => c.ErrorMessage)).ToList();
                List<string> errorlist = ModelState.Values.SelectMany(v => v.Errors.Select(c => c.ErrorMessage)).ToList();
                methcall.SetCookie(Response, "ValidationErrors", errorlist, cookieOptions, options);
        */

    }
}
