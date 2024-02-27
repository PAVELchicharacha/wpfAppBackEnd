using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labFrontEnd.model
{
    public class Response
    {
        public string? access_token { get; set; }
        public string? EMail { get; set; }

        public Response(string? access_token, string? username)
        {
            this.access_token = access_token;
            this.EMail = username;
        }
    }
}
