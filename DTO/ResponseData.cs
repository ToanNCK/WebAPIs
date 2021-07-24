using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ResponseData
    {
        public ResponseData()
        {

        }
        public ResponseData(int _code, string _msg, object _data = null)
        {
            this.Code = _code;
            this.Msg = _msg;
            this.Data = _data;
        }
        public ResponseData(int _code, string _msg)
        {
            this.Code = _code;
            this.Msg = _msg;
            this.Data = new object();
        }
        public int Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}
