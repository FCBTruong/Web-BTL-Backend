using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Users
    {
        public Users()
        {
            Reports = new HashSet<Reports>();
        }

        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int IdRole { get; set; }

        public virtual Roles IdRoleNavigation { get; set; }
        public virtual ICollection<Reports> Reports { get; set; }
    }
}
