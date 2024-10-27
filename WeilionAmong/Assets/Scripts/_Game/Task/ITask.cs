using System;
using UnityEngine;

namespace MiniTask
{
    public interface ITask
    {
        Player Player { get; set; }

        void TaskCheck();
        
        void CloseTask();
    }
}