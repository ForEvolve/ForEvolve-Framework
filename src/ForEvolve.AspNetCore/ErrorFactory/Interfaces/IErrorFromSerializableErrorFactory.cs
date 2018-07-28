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
using Microsoft.AspNetCore.Mvc;

namespace ForEvolve.AspNetCore
{
    public interface IErrorFromSerializableErrorFactory
    {
        Error Create(SerializableError serializableError);
    }
}
