using System;
using System.Reflection;

namespace EmployeeBenefits.EmployeeWeb.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}