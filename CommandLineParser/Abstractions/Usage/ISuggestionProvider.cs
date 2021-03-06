﻿using MatthiWare.CommandLine.Abstractions.Command;
using System.Collections.Generic;

namespace MatthiWare.CommandLine.Abstractions.Usage
{
    /// <summary>
    /// Creates suggestions based on input
    /// </summary>
    public interface ISuggestionProvider
    {
        /// <summary>
        /// Gets a list of matching suggestions
        /// </summary>
        /// <param name="input">The wrongly typed input</param>
        /// <param name="command">The current command context</param>
        /// <returns>A sorted list of suggestions, first item being the best match.</returns>
        IEnumerable<string> GetSuggestions(string input, ICommandLineCommandContainer command);
    }
}
