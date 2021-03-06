﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DIGEVOIndicadoresBot.Dialogs
{
    [Serializable]
    public class IndicadoresLuisDialog : LuisDialog<object>
    {
        public IndicadoresLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"])))
        {
            //todo buscar la forma de realizar log.
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            var meridian = context.Activity.LocalTimestamp.Value.ToString("tt", CultureInfo.InvariantCulture).ToLower();
            var hour = context.Activity.LocalTimestamp.Value.Hour;
            var greetings = meridian == "am" ? "buenos días" : hour >= 19 ? "buenas noches" : "buenas tardes";
            var firstName = context.Activity.From.Name.Split(' ')[0];

            await context.PostAsync($"Hola {firstName}, {greetings}, disculpa ¿puedes repetir tu pregunta?, no entiendo '{result.Query}'");

            context.Wait(MessageReceived);
        }

        [LuisIntent("conocer")]
        public async Task KnowIntent(IDialogContext context, LuisResult result)
        {
            var str = String.Join(", ", Enumerable.Select(
                result.Entities,
                e => $"{e.Entity.ToString()} {e.Type.ToString()} {e.EndIndex.ToString()}")
                .ToList());
            var meridian = context.Activity.LocalTimestamp.Value.ToString("tt", CultureInfo.InvariantCulture).ToLower();
            var hour = context.Activity.LocalTimestamp.Value.Hour;
            var greetings = meridian == "am" ? "buenos días" : hour >= 19 ? "buenas noches" : "buenas tardes";
            var firstName = context.Activity.From.Name.Split(' ')[0];

            await context.PostAsync($"Hola {firstName}, {greetings}, entiendo que deseas \"conocer\" acerca de: {str}, " +
                $"estamos trabajando para brindarte una mejor respuesta");
            context.Wait(MessageReceived);
        }

        [LuisIntent("comparar")]
        public async Task CompareIntent(IDialogContext context, LuisResult result)
        {
            var str = String.Join(", ", Enumerable.Select(
                result.Entities, 
                e => $"{e.Entity.ToString()} {e.Type.ToString()} {e.EndIndex.ToString()}")
                .ToList());
            var meridian = context.Activity.LocalTimestamp.Value.ToString("tt", CultureInfo.InvariantCulture).ToLower();
            var hour = context.Activity.LocalTimestamp.Value.Hour;
            var greetings = meridian == "am" ? "buenos días" : hour >= 19 ? "buenas noches" : "buenas tardes";
            var firstName = context.Activity.From.Name.Split(' ')[0];

            await context.PostAsync($"Hola {firstName}, {greetings}, entiendo que deseas \"comparar\" con: {str} " +
                $"estamos trabajando para brindarte una mejor respuesta");
            context.Wait(MessageReceived);
        }
    }
}