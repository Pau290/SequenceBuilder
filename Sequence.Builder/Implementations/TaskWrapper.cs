using Sequence.Space;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sequence.Builder
{
    public class TaskWrapper<T> : ITaskWrapper<T>
    {
        public Task GetTaskOnSubject(Expression<Action<T>> action, CancellationTokenSource tokenSource, IAsyncFuncBuilder<T> builder)
        {
            return new Task(() => action.Compile()(builder.GetSubject()), tokenSource != null ? tokenSource.Token : default);
        }

        public Task GetTaskOnSubjectV2(Expression<Action<T>> action, CancellationTokenSource tokenSource, IAsyncFuncBuilder2<T> builder)
        {
            return new Task(() => action.Compile()(builder.GetSubject()), tokenSource != null ? tokenSource.Token : default);
        }

        public CancellationTokenSource GetCancelationToken(Expression<Action> registerAction)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            tokenSource.Token.Register(() => registerAction.Compile());

            return tokenSource;
        }

        public CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, int cancelAfterMiliseconds)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource(cancelAfterMiliseconds);

            tokenSource.Token.Register(() => registerAction.Compile());

            return tokenSource;
        }

        public CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, TimeSpan cancelAfter)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource(cancelAfter);

            tokenSource.Token.Register(() => registerAction.Compile());

            return tokenSource;
        }

        public CancellationTokenSource GetCancelationToken(int cancelAfterMiliseconds)
        {
            return new CancellationTokenSource(cancelAfterMiliseconds);
        }

        public CancellationTokenSource GetCancelationToken(TimeSpan cancelAfter)
        {
            return new CancellationTokenSource(cancelAfter);
        }
    }
}
