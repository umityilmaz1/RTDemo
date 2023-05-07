﻿using Helper;

namespace Model.Base
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; } = DateTimeHelper.NowTurkey;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
