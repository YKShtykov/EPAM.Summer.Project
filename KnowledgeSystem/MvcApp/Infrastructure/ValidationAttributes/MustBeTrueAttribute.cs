using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApp.Infrastructure.ValidationAttributes
{
    /// <summary>
    /// Class for check-box validation
    /// </summary>
    public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Get client validation rules
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "mustbetrue"
            };
        }

        /// <summary>
        /// check if value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}