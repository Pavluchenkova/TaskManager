using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Model.Entities
{
    public enum TaskStatus : int
    {
        ToDo = 0,
        InProgress = 1,
        Done = 2
    }
}
