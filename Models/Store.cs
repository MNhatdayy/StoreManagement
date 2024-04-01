﻿using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class Store : DeletableEntity
    {
        public string StoreName { get; set; }
        public string AddressStore { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public virtual Collection<Menu> Menus { get; set; }
        public virtual Collection<Table> Tables { get; set; }

    }
}
