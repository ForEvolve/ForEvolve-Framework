// ***********************************************************************
// Assembly         : ForEvolve.AspNetCore
// Author           : Carl-Hugo Marcotte
// Created          : 11-02-2017
//
// Last Modified By : Carl-Hugo Marcotte
// Last Modified On : 11-02-2017
// ***********************************************************************
// <copyright file="IErrorFromDictionaryFactory.cs" company="ForEvolve">
//     Carl-Hugo Marcotte
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using ForEvolve.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    /// <summary>
    /// Expose a method that builds a collection of <c>ForEvolve.Contracts.Errors</c> using <c>Dictionary&lt;string, object&gt;</c>.
    /// </summary>
    public interface IErrorFromDictionaryFactory
    {
        /// <summary>
        /// Creates an error object based on the specified values.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="details">The list of errors to create where the key is the error's target and the value is the error message.</param>
        /// <returns>IEnumerable&lt;Error&gt;.</returns>
        IEnumerable<Error> Create(string errorCode, Dictionary<string, object> details);
    }
}
