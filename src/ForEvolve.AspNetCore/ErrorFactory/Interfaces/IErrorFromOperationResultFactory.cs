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
using ForEvolve.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    public interface IErrorFromOperationResultFactory
    {
        Error Create(IOperationResult operationResult);
    }
}
