using System.Reflection;
using EvaluationBackend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend;

public class EnumsController : BaseController
{
    [HttpGet("enums")]
    //TODO response type
    public Dictionary<string, dynamic> GetEnums()
    {
        var enums = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsEnum)
            .ToList();

        var result = new Dictionary<string, dynamic>();

        foreach (var e in enums)
        {
            var values = Enum.GetValues(e);
            var valuesList = new List<dynamic>();
            var index = 0;
            foreach (var value in values)
            {
                valuesList.Add(new
                {
                    Name = Enum.GetName(e, value),
                    Value = index++
                });
            }

            try
            {
                result.Add(e.Name, valuesList);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        return result;
    }

}
