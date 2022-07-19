using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Core.ApiDoc
{
    /// <summary>
    /// Plugin made to send Enum values and names correctly in APIs.
    /// </summary>
    internal class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {

                schema.Enum.Clear();
                    Enum.GetNames(context.Type)
                        .ToList()
                        //.ForEach(name => schema.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, name))} - {name}")));
                        .ForEach(name => schema.Enum.Add(new OpenApiString($"{name}")));
                 
                //var enumValues = schema.Enum.ToArray();
                //var i = 0;
                //schema.Enum.Clear();
                //foreach (var n in Enum.GetNames(context.Type).ToList())
                //{
                //    schema.Enum.Add(new OpenApiString(n));
                //    schema.Title = ((OpenApiPrimitive<int>)enumValues[i]).Value.ToString();
                //    i++;
                //}
            }
        }
    }
}
