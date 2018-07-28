using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.AspNetCore
{
    /// <summary>
    /// Expose a method that builds an <c>ForEvolve.Contracts.Errors</c> based on an IdentityError.
    /// </summary>
    public interface IErrorFromIdentityErrorFactory
    {
        /// <summary>
        /// Creates an error object based on the specified IdentityError.
        /// </summary>
        /// <param name="IdentityError">The identity error to convert.</param>
        /// <returns>The Error.</returns>
        Error Create(IdentityError identityError);
    }
}
