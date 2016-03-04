using System;
using System.Threading.Tasks;
using System.Windows;

namespace EVE_Fast_Fitting_Assessment.Ui
{
    public static class TaskExtensions
    {
        public static Task ContinueInDispatcherAsyncWith<T>(this Task<T> task, Action<Task<T>> action)
        {
            return task.ContinueWith(t => Application.Current.Dispatcher.InvokeAsync(() => action(t)));
        }

        public static Task ContinueInDispatcherAsyncWith(this Task task, Action<Task> action)
        {
            return task.ContinueWith(t => Application.Current.Dispatcher.InvokeAsync(() => action(task)));
        }
    }
}
