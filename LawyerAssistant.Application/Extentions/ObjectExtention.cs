using Application.Exceptions;
using System.Reflection;

namespace LawyerAssistant.Application.Extentions;

public static class ObjectExtention
{


    public static void MustNotNull(this object data, string errorMessage)
    {
        if (data == null)
            throw new CustomException(errorMessage);
    }


    public static List<KeyValuePair<string, string>> ConvertToKeyValuePairs(this object data , bool ignoreNullOrWhiteSpaceString = false)
    {
        if (data is IList<object> dataList)
        { 
            var keyValuesList = new List<KeyValuePair<string, string>>();
            foreach (var item in dataList)
            {
                var itemKeyValues = ConvertObjectToKeyValuePairs(item , ignoreNullOrWhiteSpaceString);
                keyValuesList.AddRange(itemKeyValues);
            }
            return keyValuesList;
        }
        else
        { 
            return ConvertObjectToKeyValuePairs(data, ignoreNullOrWhiteSpaceString);
        }
    }



    private static List<KeyValuePair<string, string>> ConvertObjectToKeyValuePairs(object data , bool ignoreNullOrWhiteSpaceString)
    {
        Type type = data.GetType();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var keyValues = new List<KeyValuePair<string, string>>();
        foreach (var property in properties)
        {
            var value = property.GetValue(data)?.ToString() ?? "";
            var key = property.Name;
            if (ignoreNullOrWhiteSpaceString && string.IsNullOrWhiteSpace(value))
                continue;
            keyValues.Add(new KeyValuePair<string, string>(key, value));
        }
        return keyValues;
    }

}
