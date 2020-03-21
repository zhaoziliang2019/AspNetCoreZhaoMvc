using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreZhaoMvc.Validations
{
    /// <summary>
    /// 自定义model属性验证
    /// </summary>
    public class ValidUrlAttribute : Attribute, IModelValidator
    {
        public string ErrorMessage { get; set; }
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var urlstr = context.Model as string;
            if (urlstr != null&&Uri.IsWellFormedUriString(urlstr,UriKind.Absolute))
            {
                return Enumerable.Empty<ModelValidationResult>();
            }
            return new List<ModelValidationResult>
            {
                new ModelValidationResult(string.Empty,ErrorMessage)
            };
        }
    }
}
