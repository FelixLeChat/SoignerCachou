using System;
using Cachou.WebAPI;

static class WebmessageSender
{
    public static void postServer(string message)
    {
        DateTime localDate = DateTime.Now;
        WebAPI.SendChildAction(localDate.ToString("yyyy-MM-dd H:m"), message);
    }
}