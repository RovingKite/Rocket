﻿using System;
using Rocket.API.Drawing;
using Rocket.API.Commands;
using Rocket.Core.User;

namespace Rocket.Core.Commands
{
    public class CommandParameterParseException : CommandWrongUsageException
    {
        public string Argument { get; }
        public Type ExpectedType { get; }

        public CommandParameterParseException(string argument, Type expectedType) : base($"Converting \"{argument}\" to \"{expectedType.Name}\" failed!")
        {
            Argument = argument;
            ExpectedType = expectedType;
        }
    }

    public class CommandIndexOutOfRangeException : CommandWrongUsageException
    {
        public int Index { get; }
        public int Length { get; }

        public CommandIndexOutOfRangeException(int index, int length) : base($"Missing {index + 1}. argument.")
        {
            Index = index;
            Length = length;
        }
    }

    public class CommandWrongUsageException : Exception, ICommandFriendlyException
    {
        public CommandWrongUsageException() : base("The command was not used correctly.") { }

        public CommandWrongUsageException(string message) : base(message) { }

        public virtual void SendErrorMessage(ICommandContext context)
        {
            context.User.SendMessage(Message, Color.DarkRed);
            context.SendCommandUsage();
        }
    }
}