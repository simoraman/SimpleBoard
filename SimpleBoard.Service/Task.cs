using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleBoard.Service
{
    public class Task
    {
        public int Id { get; set; }

        public string Status
        {
            get;
            set;
        }
        public string Description { get; set; }
    }
}
