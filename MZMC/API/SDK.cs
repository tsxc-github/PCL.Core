using System;
using PCL.Core.MZMC;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using PCL.Core.MZMC.Helper;

namespace PCL.Core.MZMC.API
{
	public class SDK
	{
		private const string BaseUrl= "https://api.mzmc.top";

		/// <summary>
		/// 发送请求
		/// </summary>
		/// <param name="request">请求参数</param>
		/// <returns></returns>
		public static Message Request(RestRequest request)
		{
			var client = new RestClient(BaseUrl);
			var result = client.Execute(request);
			if(result.ResponseStatus==ResponseStatus.Error)
				return new Message(false,result.Content);
			dynamic content=JsonConvert.DeserializeObject(result.Content);
			return new Message(result.IsSuccessStatusCode,content.msg.Value,content.data);
		}

		public class User
		{

			private string _username = "";
			private string _token = "";

			public string Username{get => _username;}

			public Message IfLogin()
			{
                if (_token == "")
                    return new Message(false, "未登录");
                var request = new RestRequest("/user/auth/check", Method.Post).AddHeader("Authorization", $"Bearer {_token}");
                var message = Request(request);

				if (message.OK == false)
					_token = "";
                return message;
            }

			/// <summary>
			/// 登录（用户名）
			/// </summary>
			/// <param name="username">用户名</param>
			/// <returns>Message，不带Data</returns>
			public Message Login(string username, string password)
			{
				var sha256=Converter.ComputeSHA256(password).ToLower();
                var request = new RestRequest("/user/auth/login", Method.Post).AddHeader("Authorization", $"Bearer {_token}");
                var message = Request(request.AddJsonBody(new { username = username, password = sha256 }));

				if (message.OK == true)
				{
					_username = username;
					_token = message.Data.access_token.Value;
				}

				return new Message(message.OK, message.MessageText);
			}

			/// <summary>
			/// 登出
			/// </summary>
			/// <returns>Message，无data</returns>
			public Message Logout()
			{
				if (IfLogin().OK == false)
					return new Message(false, "未登录");
                var request = new RestRequest("/user/auth/logout", Method.Post).AddHeader("Authorization", $"Bearer {_token}");
                var message = Request(request);
				_token = "";
				return message;
			}

			public Message GetProfile()
            {
                if (IfLogin().OK == false)
                    return new Message(false, "未登录");
				var request = new RestRequest("/player/profile", Method.Get).AddHeader("Authorization", $"Bearer {_token}");
				var message = Request(request);
				return message;
			}


            public Message GetQQ()
            {
                if (IfLogin().OK == false)
                    return new Message(false, "未登录");
                var request = new RestRequest("/user/bind/qq", Method.Get).AddHeader("Authorization", $"Bearer {_token}");
                var message = Request(request);
                return message;
            }

            public Message BindQQ(string QQ)
            {
                if (IfLogin().OK == false)
                    return new Message(false, "未登录");
                var request = new RestRequest("/user/bind/qq", Method.Post).AddHeader("Authorization", $"Bearer {_token}");
                var message = Request(request.AddJsonBody(new { qq_id = QQ }));
                return message;
            }
        }
	}
}
