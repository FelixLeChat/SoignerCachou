using System;
using Cachou.WebAPI;

static class WebmessageSender
{
    public static void postServer(string message)
    {
        DateTime localDate = DateTime.Now;
        string culture = "en-GB";
        WebAPI.SendChildAction(localDate.ToString(culture), message);
    }
}