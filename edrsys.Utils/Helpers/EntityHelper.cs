using System.Web.Helpers;

namespace edrsys.Utils.Helpers
{
    public class EntityHelper
    {
        public static string ToJSON(object entity)
        {
            return Json.Encode(entity);
        }
    }
}