//using System.Linq;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using Microsoft.AspNetCore.Http;

//namespace ToDoList.Filters
//{
//    public class FileOperationFilter : IOperationFilter
//    {
//        public void Apply(OpenApiOperation operation, OperationFilterContext context)
//        {
//            var fileParams = context.MethodInfo.GetParameters().Where(p => p.ParameterType == typeof(IFormFile)).ToArray();

//            if (fileParams.Any())
//            {
//                operation.Parameters.Clear();

//                foreach (var param in fileParams)
//                {
//                    var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == param.Name);

//                    operation.Parameters.Add(new OpenApiParameter
//                    {
//                        Name = param.Name,
//                        In = ParameterLocation.Header,
//                        Description = description.ModelMetadata?.Description,
//                        Required = description.IsRequired
//                    });
//                }

//                operation.RequestBody = new OpenApiRequestBody
//                {
//                    Content = new Dictionary<string, OpenApiMediaType>
//                {
//                    {
//                        "multipart/form-data",
//                        new OpenApiMediaType
//                        {
//                            Schema = new OpenApiSchema
//                            {
//                                Type = "object",
//                                Properties =
//                                {
//                                    ["Image"] = new OpenApiSchema
//                                    {
//                                        Description = "Upload Image",
//                                        Type = "file",
//                                        Format = "file"
//                                    }
//                                },
//                                Required = new HashSet<string>
//                                {
//                                    "Image"
//                                }
//                            }
//                        }
//                    }
//                }
//                };
//            }
//        }
//    }
//}
