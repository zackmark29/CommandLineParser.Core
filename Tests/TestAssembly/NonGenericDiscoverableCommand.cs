﻿using MatthiWare.CommandLine.Abstractions.Command;
using MatthiWare.CommandLine.Abstractions.Models;
using MatthiWare.CommandLine.Abstractions.Parsing;

namespace TestAssembly
{
    public class NonGenericDiscoverableCommand : Command
    {
        private readonly IArgumentResolver<NonGenericDiscoverableCommand> argResolver;

        public NonGenericDiscoverableCommand(IArgumentResolver<NonGenericDiscoverableCommand> argResolver)
        {
            this.argResolver = argResolver;
        }

        public override void OnConfigure(ICommandConfigurationBuilder builder)
        {
            builder
                .Name("cmd")
                .Required(true)
                .AutoExecute(true);
        }

        public override void OnExecute()
        {
            argResolver.CanResolve(new ArgumentModel(nameof(NonGenericDiscoverableCommand), string.Empty));
        }
    }
}
