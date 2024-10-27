using System;
using UnityEngine;

namespace Task
{
    public interface ITask
    {
        Player Player { get; set; }

        void TaskCheck();
        
        void CloseTask();
    }
}