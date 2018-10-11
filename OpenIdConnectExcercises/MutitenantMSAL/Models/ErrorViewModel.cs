using System;

namespace MutitenantMSAL.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Message { get; set; }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        public int Code { get; set; }
        public bool ShowCode => Code != 0;
    }
}
