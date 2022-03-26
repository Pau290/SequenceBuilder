using Sequence.Space;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Sequence.Builder
{
    public class AsyncFuncBuilder<T> : IAsyncFuncBuilder<T>
    {
        private T Subject
        {
            get;
            set;
        }

        public T GetSubject()
        {
            return this.Subject;
        }


        private TaskFactory taskFactory;

        private readonly ITaskWrapper<T> taskWrapper;


        public AsyncFuncBuilder()
        {
            this.Tasks = new Dictionary<string, Task<T>>();
            this.ActionTasks = new ConcurrentDictionary<string, Tuple<Task, CancellationTokenSource>>();

            this.Conditions = new ConcurrentDictionary<string, Tuple<string, Expression<Func<Task, bool>>>>();

            this.NotStartedActionTasks = new ConcurrentDictionary<string, Tuple<Task, CancellationTokenSource>>();

            this.ParalelTasks = new ConcurrentDictionary<string, Tuple<Task, CancellationTokenSource>>();

            this.taskFactory = new TaskFactory();

            this.taskWrapper = new TaskWrapper<T>();
        }

        IDictionary<string, Task<T>> Tasks { get; set; }

        ConcurrentDictionary<string, Tuple<Task, CancellationTokenSource>> ActionTasks { get; }

        ConcurrentDictionary<string, Tuple<Task, CancellationTokenSource>> ParalelTasks { get; }

        ConcurrentDictionary<string, Tuple<string, Expression<Func<Task, bool>>>> Conditions
        {
            get;
        }

        ConcurrentDictionary<string, Tuple<Task, CancellationTokenSource>> NotStartedActionTasks { get; }


        #region ↕ implementation

        public void RunInitialTask(Func<Task<T>> funcTask)
        {
            this.Subject = funcTask().Result;
        }

        public IAsyncFuncBuilder<T> AddTask(Task<T> task, string index)
        {
            this.Tasks.Add(index, task);
            return this;
        }

        public IAsyncFuncBuilder<T> AddActionTask(Task task, string index)
        {
            this.ActionTasks.TryAdd(index, this.BuildTuple(task, default(CancellationTokenSource)));

            return this;
        }

        public IAsyncFuncBuilder<T> AddActionTask(Task task, string index, CancellationTokenSource tokenSource)
        {
            this.ActionTasks.TryAdd(index, this.BuildTuple(task, tokenSource));

            return this;
        }

        public IAsyncFuncBuilder<T> AddParalelTask(Task task, string index, CancellationTokenSource tokenSource = default)
        {
            this.ParalelTasks.TryAdd(index, this.BuildTuple(task, tokenSource));

            return this;
        }


        public IAsyncFuncBuilder<T> AddContinue(string index, Expression<Action<Task>> action)
        {
            if (!this.ActionTasks.ContainsKey(index))
            {
                return this;
            }

            Task the_task = this.ActionTasks[index].Item1;

            the_task.ContinueWith((result) =>
            {
                action.Compile()(result);

            });

            return this;
        }


        public IAsyncFuncBuilder<T> AddContinueOption(string index, Expression<Action<Task>> action, TaskContinuationOptions option)
        {
            if (!this.ActionTasks.ContainsKey(index))
            {
                return this;
            }

            Task the_task = this.ActionTasks[index].Item1;

            the_task.ContinueWith((result) =>
            {
                action.Compile()(result);

            }, option);

            return this;
        }




        private void RunTasks()
        {
            Parallel.ForEach(this.Tasks,
                (task) =>
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    print($"{Environment.NewLine}begins task -> {task.Key}");

                    Task<T> the_task = task.Value;

                    the_task.Start();

                    the_task.Wait();

                    this.Subject = the_task.Result;

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    print(this.Subject);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"ends {task.Key}");
                });
        }

        private void RunActionTasks()
        {
            Parallel.ForEach(this.ActionTasks,
                (task) =>
                {
                    bool ok = this.AssertConditions(task.Key);

                    if (!ok)
                    {
                        this.AddToNotStartedTasks(task.Key, task.Value);
                        return;
                    }

                    Stopwatch crono = Stopwatch.StartNew();

                    Task the_task = task.Value.Item1;

                    if (the_task.IsCompleted)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        print($"the task is already completed: {task.Key} {task.Value.Item1.Status}");
                        return;
                    }

                    if (the_task.Status == TaskStatus.Running)
                    {
                        print($"the task is already started: {task.Key} {task.Value.Item1.Status}");
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;

                    print($"begins task -> {task.Key}");

                    the_task.Start();

                    the_task.Wait();

                    #region ☺ context

                    // wait

                    #endregion

                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine(task.Key);
                    Console.WriteLine("ends task ♣   {0:N0} nanoseconds", crono.ElapsedTicks * 100);

                    crono.Stop();
                });
        }

        private bool AddToNotStartedTasks(string index, Tuple<Task, CancellationTokenSource> tuple)
        {
            if (this.NotStartedActionTasks.ContainsKey(index))
            {
                return false;
            }

            bool added = this.NotStartedActionTasks.TryAdd(index, tuple);

            if (added)
            {
                print($"the task has been added to wait condition: {tuple.Item1.Id} : {tuple.Item1.Status}");
            }

            return added;
        }

        #region ↕ retries

        private void InitTimerNotStartedTasks()
        {
            this.notstarted_tasks_timer = new System.Timers.Timer(2000);

            this.notstarted_tasks_timer.Elapsed += Notstarted_tasks_timer_Elapsed;
            this.notstarted_tasks_timer.Enabled = true;
        }

        private void Notstarted_tasks_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.notstarted_tasks_timer.Stop();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Begin run not started tasks");

            this.taskFactory.StartNew(() =>
            {
                RunNotStartedTasks();
            });
        }

        System.Timers.Timer notstarted_tasks_timer;


        IList<string> notStartedRetried = new List<string>();

        private void RunNotStartedTasks()
        {
            Parallel.ForEach(this.NotStartedActionTasks,
                (task) =>
                {
                    bool ok = this.AssertConditions(task.Key);

                    if (notStartedRetried.Contains(task.Key))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("max 1 retry");
                        return;
                    }

                    if (!ok)
                    {
                        this.notStartedRetried.Add(task.Key);
                    }

                    Stopwatch crono = Stopwatch.StartNew();

                    Task the_task = task.Value.Item1;

                    if (the_task.IsCompleted)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        print($"the task is already completed: {task.Key} {task.Value.Item1.Status}");
                        return;
                    }

                    if (the_task.Status == TaskStatus.Running)
                    {
                        print($"the task is already started: {task.Key} {task.Value.Item1.Status}");
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;

                    print($"begins action.task -> {task.Key}");

                    the_task.Start();

                    the_task.Wait();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.WriteLine(task.Key);
                    Console.WriteLine("ends task ♣   {0:N0} nanoseconds", crono.ElapsedTicks * 100);

                    crono.Stop();
                });
        }

        #endregion retries

        private bool AssertConditions(string index)
        {
            Stopwatch crono = Stopwatch.StartNew();

            bool ok = true;

            if (!this.Conditions.ContainsKey(index))
            {
                return ok;
            }

            Tuple<string, Expression<Func<Task, bool>>> tuple = default;

            if (!this.Conditions.TryGetValue(index, out tuple))
            {
                throw new Exception("not get value");
            }

            string otherTaskIndex = tuple.Item1;

            Tuple<Task, CancellationTokenSource> otherTuple = default;

            if (!this.ActionTasks.TryGetValue(otherTaskIndex, out otherTuple))
            {
                throw new Exception("not get other value");
            }

            Task otherTask = otherTuple.Item1;

            if (otherTask.IsCompleted || new[] { 
                TaskStatus.Running, TaskStatus.WaitingForActivation, TaskStatus.WaitingToRun }.Contains(otherTask.Status))
            {
                return ok;
            }

            Expression<Func<Task, bool>> func = tuple.Item2;

            ok = func.Compile()(otherTask);

            crono.Stop();

            Console.WriteLine($"{crono.ElapsedMilliseconds}");
            Console.WriteLine(" conditions run in   {0:N0} nanoseconds", crono.ElapsedTicks * 100);

            return ok;
        }



        public IAsyncFuncBuilder<T> RunWhen(string index, string otherTaskIndex, Expression<Func<Task, bool>> expression)
        {
            if (this.Conditions.ContainsKey(index))
            {
                return this;
            }

            this.Conditions.TryAdd(index, this.BuildTuple(otherTaskIndex, expression));

            return this;
        }



        public void RunAllAndContinue()
        {
            this.taskFactory.StartNew(() =>
            {
                this.RunActionTasks();
            });

            this.InitTimerNotStartedTasks();
        }

        public void RunParalelTasks()
        {
            this.taskFactory.StartNew(() =>
            {
                this.RunAllParalelTasks();
            });
        }

        private void RunAllParalelTasks()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            print("Begin run all paralel tasks");

            Parallel.ForEach(this.ParalelTasks, (task) =>
            {
                Stopwatch crono = Stopwatch.StartNew();

                Task the_task = task.Value.Item1;

                if (the_task.IsCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    print($"the task is already completed: {the_task.Status}");
                    return;
                }

                if (new[] { TaskStatus.Running, TaskStatus.WaitingForActivation, TaskStatus.WaitingToRun }.Contains(the_task.Status))
                {
                    print($"the task is already started: {the_task.Status}");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;

                print($"begins action.task{the_task.Status}");

                the_task.Start();

                the_task.Wait();

                print($"ends action.task{the_task.Status}");

            });
        }

        public void RunInParallel(params Task[] tasks)
        {
            this.taskFactory.StartNew(() =>
            {
                this.RunTasksInParallel(tasks);
            });
        }

        private void RunTasksInParallel(Task[] tasks)
        {
            Parallel.ForEach(tasks, (task) =>
            {
                Stopwatch crono = Stopwatch.StartNew();

                if (task.IsCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    print($"the task is already completed: {task.Status}");
                    return;
                }

                if (new[] { TaskStatus.Running, TaskStatus.WaitingForActivation, TaskStatus.WaitingToRun }.Contains(task.Status))
                {
                    print($"the task is already started: {task.Status}");
                    return;
                }

                print($"begins action.task{task.Status}");

                task.Start();

                //the_task.Wait();

                print($"ends action.task{task.Status}");

            });
        }

        public void RunTaskByIndex(string index)
        {
            if (!ActionTasks.ContainsKey(index))
            {
                return;
            }

            CancellationTokenSource tokenSource = this.ActionTasks[index].Item2;

            if (tokenSource != null && tokenSource.IsCancellationRequested)
            {
                return;
            }

            Task task = this.ActionTasks[index].Item1;

            if (task.Status == TaskStatus.Running)
            {
                return;
            }

            task.Start();

            task.Wait();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            print($"{Environment.NewLine}begins action.task -> {index}");
        }

        public TaskStatus? CheckTaskStatus(string index)
        {
            if (!this.ActionTasks.ContainsKey(index))
            {
                return default;
            }

            Tuple<Task, CancellationTokenSource> taskData = default;

            if (!this.ActionTasks.TryGetValue(index, out taskData))
            {
                return default;
            }

            return taskData.Item1.Status;
        }

        public void CancelTask(string index)
        {
            if (!ActionTasks.ContainsKey(index))
            {
                return;
            }

            Task task = this.ActionTasks[index].Item1;

            if (!task.IsCompleted)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;

                print(task.Status);

                if (task.Status == TaskStatus.Running)
                {
                    return;
                }

                CancellationTokenSource tokenSource = this.ActionTasks[index].Item2;

                Console.ForegroundColor = ConsoleColor.Cyan;
                print($"send cancel for task {index}");

                tokenSource.Cancel();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                print($"{index} task is already completed {task.Status}");
            }
        }


        #region ↕ task wrapper

        public Task GetTaskForSubject(Expression<Action<T>> action, CancellationTokenSource tokenSource = default)
        {
            return this.taskWrapper.GetTaskOnSubject(action, tokenSource, this);
        }

        public CancellationTokenSource GetCancelationToken(Expression<Action> registerAction)
        {
            return this.taskWrapper.GetCancelationToken(registerAction);
        }

        public CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, int cancelAfterMiliseconds)
        {
            return this.taskWrapper.GetCancelationToken(registerAction, cancelAfterMiliseconds);
        }

        public CancellationTokenSource GetCancelationToken(Expression<Action> registerAction, TimeSpan cancelAfter)
        {
            return this.taskWrapper.GetCancelationToken(registerAction, cancelAfter);
        }

        public CancellationTokenSource GetCancelationToken(int cancelAfterMiliseconds)
        {
            return this.taskWrapper.GetCancelationToken(cancelAfterMiliseconds);
        }

        public CancellationTokenSource GetCancelationToken(TimeSpan cancelAfter)
        {
            return this.taskWrapper.GetCancelationToken(cancelAfter);
        }

        #endregion taskwrapper

        #endregion implementation

        #region ↕ timer


        System.Timers.Timer timer;
        System.Timers.Timer stopTimer;

        public IAsyncFuncBuilder<T> InitCheckerTimer(int interval, int stopAfter)
        {
            this.timer = new System.Timers.Timer(interval);
            this.stopTimer = new System.Timers.Timer(stopAfter);

            this.timer.Elapsed += CheckTasksStatus;
            this.timer.Enabled = true;

            this.stopTimer.Elapsed += StopTimer_Elapsed;
            this.stopTimer.Enabled = true;

            return this;
        }

        private void StopTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();
            this.stopTimer.Stop();
        }

        private void CheckTasksStatus(Object source, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("check at: {0:HH:mm:ss.fff}", e.SignalTime);

            foreach (var task in this.ActionTasks)
            {
                Console.WriteLine("{0}.{1}", task.Value.Item1.Status, task.Key);
            }
        }


        #endregion timer

        #region ↕ utils

        private Tuple<M, S> BuildTuple<M, S>(M m, S s)
        {
            return new Tuple<M, S>(m, s);
        }

        private Tuple<M, S, R> BuildTuple<M, S, R>(M m, S s, R r)
        {
            return new Tuple<M, S, R>(m, s, r);
        }

        private void print<S>(S value)
        {
            Console.WriteLine(value);
        }


        #endregion utils
    }
}
