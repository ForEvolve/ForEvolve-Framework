// ***********************************************************************
// Assembly         : ForEvolve.AspNetCore
// Author           : Carl-Hugo Marcotte
// Created          : 11-02-2017
//
// Last Modified By : Carl-Hugo Marcotte
// Last Modified On : 11-02-2017
// ***********************************************************************
// <copyright file="IErrorFactory.cs" company="ForEvolve">
//     Carl-Hugo Marcotte
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ForEvolve.AspNetCore
{
    /// <summary>
    /// Aggregate and expose <c>ForEvolve.Api.Contracts.Errors</c> building methods.
    /// </summary>
    /// <seealso cref="ForEvolve.AspNetCore.IErrorFromExceptionFactory" />
    /// <seealso cref="ForEvolve.AspNetCore.IErrorFromDictionaryFactory" />
    /// <seealso cref="ForEvolve.AspNetCore.IErrorFromKeyValuePairFactory" />
    /// <seealso cref="ForEvolve.AspNetCore.IErrorFromRawValuesFactory" />
    /// <seealso cref="ForEvolve.AspNetCore.IErrorFromIdentityErrorFactory" />
    public interface IErrorFactory : 
        IErrorFromExceptionFactory,
        IErrorFromDictionaryFactory, 
        IErrorFromKeyValuePairFactory, 
        IErrorFromRawValuesFactory, 
        IErrorFromIdentityErrorFactory,
        IErrorFromSerializableErrorFactory,
        IErrorFromOperationResultFactory
    {

    }
}
