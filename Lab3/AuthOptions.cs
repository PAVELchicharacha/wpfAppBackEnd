﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lab3
{

        public class AuthOptions
        {
            public const string ISSUER = "MyAuthServer";
            public const string AUDIENCE = "MyAuthClient";
            const string KEY = "mysupersecret_secretkey!12345678";
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

        }

}
