﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BookProject.Models
{
    //[DataContract]
    [Serializable]
    public class SuccessModel
    {
        //[DataMember]
        public bool Success { get; set; }

    }
}