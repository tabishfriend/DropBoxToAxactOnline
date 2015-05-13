using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Diagnostics;
using Dropbox.Api;
using OAuthProtocol;
using Utility;
using DataModelEdmx;
using BLL;



namespace DropBoxApi
{
    public class Manager
    {
        #region Private variables

        readonly string ConsumerKey;
        readonly string ConsumerSecret;
        public bool IsUserAccessTokenExistsInDB;
        Guid UserId;

        #endregion

        public Manager()
        {
            ConsumerKey = Common.GetValueFromAppSetting(Constants.DropBoxApplicationKey);
            ConsumerSecret = Common.GetValueFromAppSetting(Constants.DropBoxSecretKey);
            UserId = Common.GetCurrentLoginUserID();
            IsUserAccessToeknExists();

        }

        #region Authentication

        /// <summary>
        /// Perform authertication and read Token values
        /// </summary>
        /// <returns></returns>
        public string DoAuthentication()
        {
            var oauth = new OAuth();
            var requestToken = GetRequestToken(oauth);
            var authorizeUri = GetAuthorizationURI(oauth, requestToken);

            SaveToken(requestToken);
            HttpContext.Current.Session["s"] = requestToken;
            return authorizeUri.AbsoluteUri;
        }

        /// <summary>
        /// Get Request token
        /// </summary>
        /// <param name="oauth"></param>
        /// <returns></returns>
        OAuthToken GetRequestToken(OAuth oauth)
        {
            return oauth.GetRequestToken(new Uri(DropboxRestApi.BaseUri), ConsumerKey, ConsumerSecret);
        }

        /// <summary>
        /// Get redirection URL for allow application
        /// </summary>
        /// <param name="oauth"></param>
        /// <param name="requestToken"></param>
        /// <returns></returns>
        Uri GetAuthorizationURI(OAuth oauth, OAuthToken requestToken)
        {
            return oauth.GetAuthorizeUri(new Uri(DropboxRestApi.AuthorizeBaseUri), requestToken);
        }

        /// <summary>
        /// Save Token values for next login
        /// </summary>
        /// <param name="requestToke"></param>
        void SaveToken(OAuthToken requestToke)
        {

            DropBoxUser NewRecord = new DropBoxUser();
            NewRecord.UserID = UserId;
            NewRecord.Secret = requestToke.Secret;
            NewRecord.Token = requestToke.Token;

            var obj = new DropBoxUserManager(NewRecord);
            obj.InsertUpdate();
        }


        void UpdateToken(OAuthToken requestToke)
        {

            DropBoxUser NewRecord = new DropBoxUser();
            NewRecord.UserID = UserId;
            NewRecord.Secret = requestToke.Secret;
            NewRecord.Token = requestToke.Token;
            NewRecord.IsAccessToken = true;

            var obj = new DropBoxUserManager(NewRecord);
            obj.UpdateUser();
        }
        #endregion



        #region Authorization

        public void UpdateAccessToken()
        {
            GetAccessToken();
        }

        public DropboxApi GetAuthorizedObject()
        {
            var accessToken = GetAccessToken();
            return new DropboxApi(ConsumerKey, ConsumerSecret, accessToken);
        }


        /// <summary>
        /// Get Access Token
        /// </summary>
        /// <returns></returns>
        public OAuthToken GetAccessToken()
        {
            var obj = GetUserObject();
            var User = obj.ReadUser();
            var requestToken = ReadToken();

            if (!User.IsAccessToken)
            {
                var oauth = new OAuth();
                var AccessToken = oauth.GetAccessToken(new Uri(DropboxRestApi.BaseUri), ConsumerKey, ConsumerSecret, requestToken);
                UpdateToken(AccessToken);
                return AccessToken;
            }
            else
            {
                return requestToken;
            }

        }

        /// <summary>
        /// Read Token value from Database
        /// </summary>
        /// <returns></returns>
        OAuthToken ReadToken()
        {
            OAuthToken requestToken = new OAuthToken(string.Empty, string.Empty);
            var obj = GetUserObject();
            var Result = obj.ReadUser();
            if (Result != null)
            {
                requestToken = new OAuthToken(Result.Token, Result.Secret);
            }

            return requestToken;
        }

        #endregion

        /// <summary>
        /// Check user access token exists or not
        /// </summary>
        void IsUserAccessToeknExists()
        {
            var obj = GetUserObject();
            IsUserAccessTokenExistsInDB = obj.IsUserAccessTokenExists();
        }

        DropBoxUserManager GetUserObject()
        {
            DropBoxUser NewRecord = new DropBoxUser();
            NewRecord.UserID = UserId;
            var obj = new DropBoxUserManager(NewRecord);
            return obj;
        }


    }
}
