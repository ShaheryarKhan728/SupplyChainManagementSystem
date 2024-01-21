using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SCMS.Controllers.Helper
{
    public static class clsSession
    {
        public static bool HasLogin(this ISession session)
        {
            return session.GetInt32(_AccountId) != null;
        }

        public static LoginResponse GetLogin(this ISession session)
        {
            if (session == null)
            {
                return null;
            }

            return new LoginResponse
            {
                AccountId = session.GetInt32(_AccountId) ?? 0,
                UserId = session.GetString(_UserId),
                UserName = session.GetString(_UserName),
                AccountType = session.GetInt32(_AccountType) ?? 0,
                Approval = session.GetInt32(_Approval) ?? 0
            };
        }


        public static void SetLogin(this ISession session, string jsonResponse)
        {
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

            if (loginResponse != null)
            {
                session.SetInt32(_AccountId, loginResponse.AccountId);
                session.SetString(_UserId, loginResponse.UserId);
                session.SetString(_UserName, loginResponse.UserName);
                session.SetInt32(_AccountType, loginResponse.AccountType ?? 0);
                session.SetInt32(_Approval, loginResponse.Approval ?? 0);
            }
        }

        //public static void SetTempLogin(this ISession session, int LoginId)
        //{
        //    if (LoginId > 0)
        //    {
        //        session.SetInt32("TempLoginId", LoginId);
        //    }
        //}

        //public static int GetTempLoginId(this ISession session)
        //{
        //    if (session.GetInt32("TempLoginId") != null)
        //        return session.GetInt32("TempLoginId").Value;
        //    else
        //        return 0;
        //}

        //public static string UserId
        //{
        //    get
        //    {
        //        return "admin";
        //    }
        //}

        //public static string CompanyName
        //{
        //    get
        //    {
        //        return "SCMS";
        //    }
        //}

        public static int LoginId(this ISession session)
       {
            if (session.GetInt32(_AccountId) != null)
                return session.GetInt32(_AccountId).Value;
            else
                return 0;
        }

        public static void ClearLogin(this ISession session)
        {
            session.Clear();
        }

        #region SessionKeys

        static string _AccountId = "account_id";
        static string _UserId = "user_id";
        static string _UserName = "user_name";
        static string _AccountType = "account_type";
        static string _Approval = "approval";

        #endregion SessionKeys
    }

    public class LoginResponse
    {
        public int AccountId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? AccountType { get; set; }
        public int? Approval { get; set; }
    }
}
