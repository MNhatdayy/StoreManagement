﻿namespace StoreManagement.DTO
{
    public class TableDTO
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Status { get; set; } = false;
        public int StoreID { get; set; }
        public StoreDTO Store { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
