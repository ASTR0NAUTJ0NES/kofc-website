using KofCWebSite.Models;
using KofCWebSite.ReCaptcha.Dto.Posts;
using KofCWebSite.ReCaptcha.Dto.Results;
using KofCWebSite.ReCaptcha.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KofCWebSite.ReCaptcha
{
    public class GoogleReCaptchaClient : IGoogleReCaptchaClient
    {
        private HttpClient _httpClient = new HttpClient();
        private ReCaptchaClientSettings _recaptchaClientSettings;
        private const string _SiteVerifyUrl = "/api/siteverify";
        private TimeSpan _timeout { get; set; } = TimeSpan.FromSeconds(30);

        public GoogleReCaptchaClient(IOptions<ReCaptchaClientSettings> options)
        {
            _recaptchaClientSettings = options?.Value ?? new ReCaptchaClientSettings();
        }

        public async Task<SiteVerifyResult> VerifyAsync(SiteVerifyPost post)
        {
            SiteVerifyResponse response = await GetSiteVerifyAsync(post);

            SiteVerifyResult result = new SiteVerifyResult();
            if (response == null)
                throw new Exception("No response returned from Google reCAPTCHA.");

            if (response.ErrorCodes != null && response.ErrorCodes.Any())
            {
                List<ResultErrorMessage> errorList = new List<ResultErrorMessage>();
                foreach (var error in response.ErrorCodes)
                {
                    errorList.Add(new ResultErrorMessage() { Id = "", Message = error });
                }
                result.IsVerified = false;
                result.ResultStatus = enums.ResultStatus.Error;
                result.ResultStatusMessages = errorList.ToArray();
                return result;
            }

            result.IsVerified = response.Success;

            return result;
        }

        private async Task<SiteVerifyResponse> GetSiteVerifyAsync(SiteVerifyPost post)
        {
            //string urlParam = UriExt.JsonObjectToUrlParams(post);
            var sKey = post.SecretKey;
            var gcResponse = post.RecaptchaResponseToken;
            string url = "https://www.google.com/recaptcha/api/siteverify";
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(post.SecretKey), "secret");
            form.Add(new StringContent(post.RecaptchaResponseToken), "response");
            var msg = CreateMessage(HttpMethod.Post, url);
            msg.Content = form;

            SiteVerifyResponse response = null;
            try
            {
                response = await SendMsgAsync<SiteVerifyResponse>(msg);
            }
            catch(Exception)
            {
                throw;
            }

            return response;
        }

        private async Task<T> SendMsgAsync<T>(HttpRequestMessage msg)
        {
            var response = await _httpClient.SendAsync(msg);
            return GetResult<T>(response.Content);
        }

        private T GetResult<T>(HttpContent httpContent)
        {
            var resultStringTask = httpContent.ReadAsStringAsync();
            resultStringTask.Wait();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resultStringTask.Result);
        }

        private HttpRequestMessage CreateMessage(HttpMethod httpMethod, string endPoint)
        {
            string finalUrl = UriExt.Combine(_recaptchaClientSettings.Url, endPoint);
            return new HttpRequestMessage(httpMethod, finalUrl);
        }
    }
}
