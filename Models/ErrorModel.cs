using System;

namespace BiliWeb2.Models.ViewModels
{
    public class ErrorModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
