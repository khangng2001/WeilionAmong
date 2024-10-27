using System;

namespace Task
{
    public interface ITask
    {
        Player Player { get; set; }

        void Update();
        
        void CloseTask();
    }
}