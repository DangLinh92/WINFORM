using System.Data;

namespace Wisol.Objects
{
    public class UserInfo
    {
        private string id = string.Empty;
        private string name = string.Empty;
        private string language = string.Empty;
        private DataTable userRole = null;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }

        public DataTable Role
        {
            get
            {
                return userRole;
            }
            set
            {
                userRole = value;
            }
        }
    }
}
