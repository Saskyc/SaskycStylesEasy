using System;
using CommandSystem;
using Exiled.API.Features;
using SaskycStylesEasy.Classes;
using SaskycStylesTestt.Classes;

namespace SaskycStylesEasy.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CommandName : ICommand
    {
        public string Command { get; } = "TagTest";
        public string[] Aliases { get; } = { "" };
        public string Description { get; } = "Test your tag";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var senderPlayer = Player.Get(sender); // The player

            var myArguments = string.Join(" ", arguments); // All arguments

            var s = myArguments[1]; // Single argument

            if (senderPlayer == null)
            {
                //Unsuccessful
                response = "You need a player";
                return false;
            }
            
            var player = Player.Get(arguments.At(0));

            if (player == null)
            {
                response = "Unable to find player";
                return false;
            }
            
            var tagName = arguments.At(1);

            Tag.ExecuteTag(senderPlayer, tagName, [], "CommandText");
            
            //Successful
            response = "Command executed";
            return true;
        }
    }
}