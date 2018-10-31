using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtSettings.Model
{
    public class JwtSetting
    {
        /// <summary>
        /// Token 颁发者；
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 给谁使用；
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 密钥；
        /// </summary>
        public string SecretKey { get; set; }
    }
}
