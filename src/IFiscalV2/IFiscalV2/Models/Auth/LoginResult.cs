namespace IFiscalV2.Models.Auth
{
    using IFiscalV2.Common;

    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string LoginStepTo { get; set; } = LoginSate.None;
    }
}
