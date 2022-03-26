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
    public interface IAsyncFuncBuilder2<T>
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
        IAsyncFuncBuilder2<T> AddTask(Task<T> task, string index);

        /// <summary>
        /// Add a Task
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IAsyncFuncBuilder2<T> AddActionTask(Task task, string index);

        /// <summary>
        /// Add a Task with token
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        /// <param name="tokenSource"></param>
        /// <returns></returns>
        IAsyncFuncBuilder2<T> AddActionTask(Task task, string index, CancellationTokenSource tokenSource);


        /// <summary>
        /// Add Task with Action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IAsyncFuncBuilder2<T> AddAction(Expression<Action<T>> action, string index);


        /// <summary>
        /// Run all and continue
        /// </summary>
        void RunAllAndContinue();

        /// <summary>
        /// Run task inline
        /// </summary>
        /// <param name="task"></param>
        void RunTaskInline(Task task);


        /// <summary>
        ///  Add continue with option to Task on result
        /// </summary>
        /// <param name="index"></param>
        /// <param name="action"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        IAsyncFuncBuilder2<T> AddContinueOption(string index, Expression<Action<Task>> action, TaskContinuationOptions option);

        /// <summary>
        /// Add Continue Action
        /// </summary>
        /// <param name="index"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        IAsyncFuncBuilder2<T> AddContinue(string index, Expression<Action<Task>> action);


        /// <summary>
        /// Add condition to run
        /// </summary>
        /// <param name="index"></param>
        /// <param name="otherTaskIndex"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        IAsyncFuncBuilder2<T> RunWhen(string index, string otherTaskIndex, Expression<Func<Task, bool>> expression);


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
        IAsyncFuncBuilder2<T> InitCheckerTimer(int interval, int stopAfter);

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
        Task GetTaskForSubjectV2(Expression<Action<T>> action, CancellationTokenSource tokenSource = default);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, int cancelAfterMiliseconds);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, TimeSpan cancelAfter);

        CancellationTokenSource GetCancelationToken(int cancelAfterMiliseconds);

        CancellationTokenSource GetCancelationToken(TimeSpan cancelAfter);

        #endregion  task wrapper
    }
}
