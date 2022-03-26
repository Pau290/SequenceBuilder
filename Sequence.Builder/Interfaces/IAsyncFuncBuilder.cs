using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sequence.Builder
{
    /// <summary>
    /// Arrezife Software Demo ©
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncFuncBuilder<T>
    {
        /// <summary>
        /// Gets the T
        /// </summary>
        /// <returns></returns>
        T GetSubject();

        /// <summary>
        /// Runs initial Task
        /// </summary>
        /// <param name="funcTask"></param>
        void RunInitialTask(Func<Task<T>> funcTask);

        /// <summary>
        /// Add a Task<typeparamref name="T"/>
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IAsyncFuncBuilder<T> AddTask(Task<T> task, string index);

        /// <summary>
        /// Add a Task
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IAsyncFuncBuilder<T> AddActionTask(Task task, string index);

        /// <summary>
        /// Add a Task with token
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        /// <param name="tokenSource"></param>
        /// <returns></returns>
        IAsyncFuncBuilder<T> AddActionTask(Task task, string index, CancellationTokenSource tokenSource);


        IAsyncFuncBuilder<T> AddParalelTask(Task task, string index, CancellationTokenSource tokenSource = default);

        /// <summary>
        /// Run all and continue
        /// </summary>
        void RunAllAndContinue();

        void RunParalelTasks();


        void RunInParallel(params Task[] tasks);

        /// <summary>
        /// Run task by index
        /// </summary>
        /// <param name="index"></param>
        void RunTaskByIndex(string index);


        /// <summary>
        ///  Add continue with option to Task on result
        /// </summary>
        /// <param name="index"></param>
        /// <param name="action"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        IAsyncFuncBuilder<T> AddContinueOption(string index, Expression<Action<Task>> action, TaskContinuationOptions option);

        /// <summary>
        /// Add Continue Action
        /// </summary>
        /// <param name="index"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        IAsyncFuncBuilder<T> AddContinue(string index, Expression<Action<Task>> action);


        IAsyncFuncBuilder<T> RunWhen(string index, string otherTaskIndex, Expression<Func<Task, bool>> expression);


        /// <summary>
        /// Cancel task by index
        /// </summary>
        /// <param name="index"></param>
        void CancelTask(string index);

        /// <summary>
        /// Inits the checker status tasks timer
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        IAsyncFuncBuilder<T> InitCheckerTimer(int interval, int stopAfter);

        /// <summary>
        /// Checks Task Status by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        TaskStatus? CheckTaskStatus(string index);

        #region ↕ task wrapper

        /// <summary>
        /// Gets the representation of Task which run on the subject
        /// </summary>
        /// <param name="action"></param>
        /// <param name="tokenSource"></param>
        /// <returns></returns>
        Task GetTaskForSubject(Expression<Action<T>> action, CancellationTokenSource tokenSource = default);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, int cancelAfterMiliseconds);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, TimeSpan cancelAfter);

        CancellationTokenSource GetCancelationToken(int cancelAfterMiliseconds);

        CancellationTokenSource GetCancelationToken(TimeSpan cancelAfter);

        #endregion  task wrapper
    }
}
