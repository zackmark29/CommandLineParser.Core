﻿namespace MatthiWare.CommandLine.Abstractions
{
    /// <summary>
    /// API for configuring options
    /// </summary>
    public interface IOptionBuilder
    {
        /// <summary>
        /// Sets if the option is required
        /// </summary>
        /// <param name="required">Required or not</param>
        /// <returns><see cref="IOptionBuilder"/></returns>
        IOptionBuilder Required(bool required = true);

        /// <summary>
        /// Help text to be displayed for this option
        /// </summary>
        /// <param name="description">The description of the option</param>
        /// <returns><see cref="IOptionBuilder"></see></returns>
        IOptionBuilder Description(string description);

        /// <summary>
        /// Specify the default value for this option
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns><see cref="IOptionBuilder"></see></returns>
        IOptionBuilder Default(object defaultValue);

        /// <summary>
        /// Configures the name for the option
        /// </summary>
        /// <param name="shortName">short name</param>
        /// <returns><see cref="IOptionBuilder"></see></returns>
        IOptionBuilder Name(string shortName);

        /// <summary>
        /// Configures the name for the option
        /// </summary>
        /// <param name="shortName">Short name</param>
        /// <param name="longName">Long name</param>
        /// <returns><see cref="IOptionBuilder"></see></returns>
        IOptionBuilder Name(string shortName, string longName);

        /// <summary>
        /// Order in which the option will be parsed
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        IOptionBuilder Order(int order);
    }
}
