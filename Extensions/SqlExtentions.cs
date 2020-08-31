using System;
using System.Reflection;

namespace mvc.Extensions
{
    public static class SqlExtentions
    {
        public static string BuildReplaceQuery(this object obj, params string[] condition) 
        {
            PropertyInfo[] myPropertyInfo = obj.GetType().GetProperties();
            var query = "UPDATE tasks SET ";
            var conditionQuery = "WHERE "; // WHERE id = @Id
            foreach (var item in myPropertyInfo)
            {
                if(Array.Exists(condition, element => element.Equals(item.Name)))
                {
                    conditionQuery += $"{item.Name.ToLower()} = @{item.Name}, ";
                } else
                {
                    query += $"{item.Name.ToLower()} = @{item.Name}, ";
                }
            }
            return $"{query.Remove(query.Length - 2)} {conditionQuery.Remove(conditionQuery.Length - 2)}";
        } 
    }
}

// namespace mvc.Extensions
// {
//     public static class SqlExtentions
//     {
//         public static string BuildReplaceQuery(this object obj, params string[] excluded) 
//         {
//             PropertyInfo[] myPropertyInfo = obj.GetType().GetProperties();
//             var query = "";
//             foreach (var item in myPropertyInfo)
//             {
//                 if(!Array.Exists(excluded, element => element.Equals(item.Name)))
//                 {
//                     query += $"{item.Name.ToLower()} = @{item.Name}, ";
//                 }
//             }
//             return query.Remove(query.Length - 2);
//         } 
//     }
// }