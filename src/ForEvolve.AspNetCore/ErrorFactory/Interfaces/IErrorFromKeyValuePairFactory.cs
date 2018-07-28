// ***********************************************************************
// Assembly         : ForEvolve.AspNetCore
// Author           : Carl-Hugo Marcotte
// Created          : 11-02-2017
//
// Last Modified By : Carl-Hugo Marcotte
// Last Modified On : 11-02-2017
// ***********************************************************************
// <copyright file="IErrorFromKeyValuePairFactory.cs" company="ForEvolve">
//     Carl-Hugo Marcotte
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using ForEvolve.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    /// <summary>
    /// Expose a method that builds an <c>ForEvolve.Contracts.Errors</c> using <c>KeyValuePair&lt;string, object&gt;</c>.
    /// </summary>
    public interface IErrorFromKeyValuePairFactory
    {
        /// <summary>
        /// Creates an error object based on the specified values.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorTargetAndMessage">A KeyValuePair where the key is the error's target and the value is the error message.</param>
        /// <returns>The Error.</returns>
        Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage);
    }
}
