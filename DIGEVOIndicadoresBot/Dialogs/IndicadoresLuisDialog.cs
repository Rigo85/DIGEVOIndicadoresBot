﻿//using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DIGEVOIndicadoresBot.Dialogs
{
    [Serializable]
    public class IndicadoresLuisDialog : LuisDialog<object>
    {
        //var modelId = ConfigurationManager.AppSettings.Get("LuisModelId");
        //var subscriptionKey = ConfigurationManager.AppSettings.Get("LuisSubscriptionKey");
        //public IndicadoresLuisDialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
        public IndicadoresLuisDialog() : base(
            new LuisService(
                new LuisModelAttribute(
                    ConfigurationManager.AppSettings.Get("LuisModelId"),
                    ConfigurationManager.AppSettings.Get("LuisSubscriptionKey"))))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            //context.Activity
            await context.PostAsync($"You have reached the none intent. You said: {result.Query}"); //
            context.Wait(MessageReceived);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "MyIntent" with the name of your newly created intent in the following handler
        [LuisIntent("conocer")]
        public async Task MyIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached the MyIntent intent. You said: {result.Query}"); //
            context.Wait(MessageReceived);
        }
    }
}