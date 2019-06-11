using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Scrutor;

namespace ForEvolve.OperationResults.MediatR
{
    /// <summary>
    /// Contains extension methods to help wire up MediatR.
    /// </summary>
    public static class ForEvolveMediatRExtensions
    {
        /// <summary>
        /// Registers all <see cref="ValidationBehavior{TRequest, TResponse}"/> 
        /// that uses <see cref="IValidator{TRequest}"/> 
        /// as MediatR <see cref="IPipelineBehavior{TRequest, TResponse}"/>.
        /// 
        /// Automatic command and query validation should work as long there is a <see cref="IValidator{TRequest}"/> for the handler.
        /// </summary>
        /// <param name="selector">The selector used to scan for classes.</param>
        /// <returns>The selector.</returns>
        public static IImplementationTypeSelector AddValidationBehaviors(this IImplementationTypeSelector selector)
        {
            return selector.AddClasses(classes => classes.AssignableTo(typeof(ValidationBehavior<,>)))
                .As(typeof(IPipelineBehavior<,>))
                .WithTransientLifetime();
        }

        /// <summary>
        /// Scans and registers all <see cref="IValidator{T}"/>.
        /// </summary>
        /// <param name="selector">The selector used to scan for classes.</param>
        /// <returns>The selector.</returns>
        public static IImplementationTypeSelector AddValidators(this IImplementationTypeSelector selector)
        {
            return selector.AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        }

        /// <summary>
        /// Automatic validators discovery and automatic validation of commands/queries into the MediatR pipeline
        /// 
        /// Shortcut that calls <see cref="AddValidators"/> and <see cref="AddValidationBehaviors"/>.
        /// </summary>
        /// <param name="selector">The selector used to scan for classes.</param>
        /// <returns>The selector.</returns>
        public static IImplementationTypeSelector AddValidatorsAndBehaviors(this IImplementationTypeSelector selector)
        {
            return selector
                .AddValidators()
                .AddValidationBehaviors();
        }
    }
}
