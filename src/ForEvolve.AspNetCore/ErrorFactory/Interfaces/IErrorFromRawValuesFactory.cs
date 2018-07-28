// ***********************************************************************
// Assembly         : ForEvolve.AspNetCore
// Author           : Carl-Hugo Marcotte
// Created          : 11-02-2017
//
// Last Modified By : Carl-Hugo Marcotte
// Last Modified On : 11-02-2017
// ***********************************************************************
// <copyright file="IErrorFromRawValuesFactory.cs" company="ForEvolve">
//     Carl-Hugo Marcotte
// </copyright>
// <summary></summary>
// ***********************************************************************
using ForEvolve.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    /// <summary>
    /// Expose a method that builds an <c>ForEvolve.Contracts.Errors</c> using raw values.
    /// </summary>
    public interface IErrorFromRawValuesFactory
    {
        /// <summary>
        /// Creates an error object based on the specified values.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorTarget">The error's target.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The Error.</returns>
        Error Create(string errorCode, string errorTarget, object errorMessage);
    }
}
