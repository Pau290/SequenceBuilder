using Sequence.Builder;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sequence.Space
{
    /// <summary>
    /// Arrezife Software Demo ©
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITaskWrapper<T>
    {
        /// <summary>
        /// Gets representation of Task
        /// </summary>
        /// <param name="action"></param>
        /// <param name="tokenSource"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        Task GetTaskOnSubject(Expression<Action<T>> action, CancellationTokenSource tokenSource, IAsyncFuncBuilder<T> builder);

        Task GetTaskOnSubjectV2(Expression<Action<T>> action, CancellationTokenSource tokenSource, IAsyncFuncBuilder2<T> builder);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, int cancelAfterMiliseconds);

        CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, TimeSpan cancelAfter);

        CancellationTokenSource GetCancelationToken(int cancelAfterMiliseconds);

        CancellationTokenSource GetCancelationToken(TimeSpan cancelAfter);
    }
}

