using System.Collections.Generic;
using System.Web.Mvc;

public class BaseController : Controller
{
    public BaseController()
    {


    }

    public string ErrorMessage
    {
        get { return TempData["ErrorMessage"] == null ? string.Empty : TempData["ErrorMessage"].ToString(); }
        set { TempData["ErrorMessage"] = value; }
    }

    public void AddMessage(UserMessageType type, string message, string title)
    {
        var messages = new List<UserMessageModel>
        {
            new UserMessageModel()
            {
                Message = message,
                Title = title,
                Type = type
            }
        };
        TempData["Notifications"] = messages;
    }
}
public enum UserMessageType
{
    Success,
    Error,
    Warning, 
    Info

}
public class UserMessageModel
{
    public string Message { get; set; }
    public string Title { get; set; }
    public UserMessageType Type { get; set; }
}