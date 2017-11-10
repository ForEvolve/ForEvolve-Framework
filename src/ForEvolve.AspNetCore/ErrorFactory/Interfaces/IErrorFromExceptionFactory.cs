// ***********************************************************************
// Assembly         : ForEvolve.AspNetCore
// Author           : Carl-Hugo Marcotte
// Created          : 11-02-2017
//
// Last Modified By : Carl-Hugo Marcotte
// Last Modified On : 11-02-2017
// ***********************************************************************
// <copyright file="IErrorFromExceptionFactory.cs" company="ForEvolve">
//     Carl-Hugo Marcotte
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    /// <summary>
    /// Expose a method that builds an <c>ForEvolve.Api.Contracts.Errors</c> using an <c>Exception</c> object.
    /// </summary>
    public interface IErrorFromExceptionFactory
    {
        /// <summary>
        /// Creates an error object based on the specified values.
        /// </summary>
        /// <typeparam name="TException">The type of the Exception.</typeparam>
        /// <param name="exception">The exception to use to create the Error.</param>
        /// <returns>The Error.</returns>
        Error Create<TException>(TException exception)
            where TException : Exception;
    }
}
