﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.ApplicationService.Stared.Operator.Dto
{
    public class OperatorWordDto
    {
        /// <summary>
        /// 登录人ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }
        public string fullName { get; set; }

        public string headPic { get; set; }

        public string sex { get; set; }

        public List<string> post { get; set; }

        public List<string> role { get; set; }

        public string organize { get; set; }

        public DateTime? lastTime { get; set; }

        public int loginSum { get; set; } = 0;

        public int messageSum { get; set; } = 0;

        public int agencySum { get; set; } = 0;

        public string summary { get; set; }
    }
}
