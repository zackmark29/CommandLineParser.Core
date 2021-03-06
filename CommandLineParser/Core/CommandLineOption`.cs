﻿using System;
using System.Linq.Expressions;
using MatthiWare.CommandLine.Abstractions;
using MatthiWare.CommandLine.Abstractions.Parsing;
using MatthiWare.CommandLine.Core.Utils;
using Microsoft.Extensions.Logging;

namespace MatthiWare.CommandLine.Core
{
    internal class CommandLineOption<TOption> : CommandLineOptionBase, IOptionBuilder<TOption>
    {
        public CommandLineOption(CommandLineParserOptions parserOptions, object source, LambdaExpression selector, IArgumentResolver<TOption> argumentResolver, ILogger logger)
             : base(parserOptions, source, selector, argumentResolver, logger)
        {
            this.AllowMultipleValues = typeof(TOption).CanHaveMultipleValues();
        }

        public IOptionBuilder<TOption> Required(bool required = true)
        {
            ((IOptionBuilder)this).Required(required);

            return this;
        }

        public new IOptionBuilder<TOption> Description(string description)
        {
            ((IOptionBuilder)this).Description(description);

            return this;
        }

        public IOptionBuilder<TOption> Default(TOption defaultValue)
        {
            ((IOptionBuilder)this).Default(defaultValue);

            return this;
        }

        public IOptionBuilder<TOption> Name(string shortName)
        {
            ((IOptionBuilder)this).Name(shortName);

            return this;
        }

        public IOptionBuilder<TOption> Name(string shortName, string longName)
        {
            ((IOptionBuilder)this).Name(shortName, longName);

            return this;
        }

        public IOptionBuilder<TOption> Transform(Expression<Func<TOption, TOption>> transformation)
        {
            SetTranslator(transformation.Compile());

            return this;
        }

        IOptionBuilder IOptionBuilder.Required(bool required)
        {
            IsRequired = required;

            return this;
        }

        IOptionBuilder IOptionBuilder.Description(string description)
        {
            base.Description = description;

            return this;
        }

        IOptionBuilder IOptionBuilder.Default(object defaultValue)
        {
            DefaultValue = defaultValue;

            return this;
        }

        IOptionBuilder IOptionBuilder.Name(string shortName)
        {
            LongName = $"{m_parserOptions.PrefixLongOption}{shortName}";
            ShortName = $"{m_parserOptions.PrefixShortOption}{shortName}";

            return this;
        }

        IOptionBuilder IOptionBuilder.Name(string shortName, string longName)
        {
            LongName = $"{m_parserOptions.PrefixLongOption}{longName}";
            ShortName = $"{m_parserOptions.PrefixShortOption}{shortName}";

            return this;
        }

        public IOptionBuilder<TOption> Order(int order)
        {
            ((IOptionBuilder)this).Order(order);

            return this;
        }

        IOptionBuilder IOptionBuilder.Order(int order)
        {
            base.Order = order;

            return this;
        }
    }
}
