using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using TCPConnectedAPI.Code;
using TCPConnectedAPI.Models;
using TCPLightingWebServer.Domain;


namespace TCPConnectedAPI.Controllers
{

    public sealed class TokenController : BaseLightController
    {

        public Token Get()
        {
            var token = new Token();

            var prevToken = (from t in Context.LightingDictionaries
                             where t.ItemKey == "Token"
                             select t).FirstOrDefault();
            if (prevToken == null)
            {
                token.Value = "";
                token.Success = false;
            }
            else
            {
                token.Value = prevToken.Value;
                token.Success = true;
            }
            return token;
        }
        //I know I am taking a bit of a leap with what a put is...
        public Token Put()
        {
            var token = new Models.Token();
            token.Value = GenerateToken();
            if (String.IsNullOrEmpty(token.Value))
            {
                token.Success = false;
            }
            else
            {
                token.Success = true;
            }
            return token;
        }

        [HttpPost]
        public Token Post([FromBody]Token token)
        {
            var prevToken = (from t in Context.LightingDictionaries
                             where t.ItemKey == "Token"
                             select t).FirstOrDefault();

            var newToken = new Token();
            var temp = new LightingDictionary();
            if (prevToken == null)
            {
                temp.ItemKey = "Token";
                temp.Value = token.Value;

                Context.LightingDictionaries.InsertOnSubmit(temp);
            }
            else
            {
                prevToken.Value = token.Value;
            }
            Context.SubmitChanges();
            newToken.Success = true;
            newToken.Value = token.Value;
            return newToken;
        }

    }
}
