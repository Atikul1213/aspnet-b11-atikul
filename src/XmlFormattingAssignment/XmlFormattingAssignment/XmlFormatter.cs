using System.Collections;
using System.Reflection;
using System.Text;

namespace XmlFormattingAssignment
{
    public static class XmlFormatter
    {
        public static string Convert(object obj)
        {
            if (obj == null)
                return "";

            StringBuilder xmlBuilder = new StringBuilder();
            Type type = obj.GetType();
            xmlBuilder.AppendLine($"\t<{type.Name}>");
            ConvertObjectToXml(obj, xmlBuilder);
            xmlBuilder.AppendLine($"\t</{type.Name}>");

            return xmlBuilder.ToString();
        }

        private static void ConvertObjectToXml(object obj, StringBuilder xmlBuilder)
        {
            if (obj == null) return;

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string? propertyName = property?.Name;

                object? value = property?.GetValue(obj);

                if (value == null)
                {
                    xmlBuilder.AppendLine($"\t<{propertyName}></{propertyName}>");
                }
                else if (value is string || value.GetType().IsPrimitive || value is DateTime || value is decimal)
                {
                    xmlBuilder.AppendLine($"\t<{propertyName}>{value}</{propertyName}>");
                }
                else
                {
                    Type? propertyType = property?.PropertyType;
                    var isArray = propertyType?.IsArray;
                    Type? elementType = propertyType?.GetElementType();

                    var propertyTypeName = propertyType?.Name;
                    if (propertyType?.IsClass == true && isArray == false)
                    {
                        if (propertyType?.IsGenericType == true && value is IEnumerable enumerableValue)
                        {
                            xmlBuilder.AppendLine($"\t<{propertyName}>");

                            foreach (var item in enumerableValue)
                            {
                                if (item is not null)
                                {
                                    Type itemType = item.GetType();
                                    xmlBuilder.AppendLine($"\t\t<{itemType.Name}>");
                                    ConvertObjectToXml(item, xmlBuilder);
                                    xmlBuilder.AppendLine($"\t\t</{itemType.Name}>");
                                }
                            }

                            xmlBuilder.AppendLine($"\t\t</{propertyName}>");
                        }
                        else
                        {
                            xmlBuilder.AppendLine($"\t<{propertyName}>");
                            ConvertObjectToXml(value, xmlBuilder);
                            xmlBuilder.AppendLine($"\t\t</{propertyName}>");
                        }
                    }
                    else
                    {
                        xmlBuilder.AppendLine($"\t<{propertyName}>");
                        if (value is IEnumerable enumerableValue)
                        {
                            foreach (var item in enumerableValue)
                            {
                                xmlBuilder.AppendLine($"\t\t<{elementType?.Name}>{item}</{elementType?.Name}>");
                            }
                        }

                        xmlBuilder.AppendLine($"\t\t</{propertyName}>");

                    }

                }
            }
        }

    }
}