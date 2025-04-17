using System;
using System.Threading;
using Godot;

namespace Cysharp.Threading.Tasks
{
    public static class GodotBindingExtensions
    {
        // <string> -> Label

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, Label label, bool rebindOnError = true)
        {
            BindToCore(source, label, label.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, Label label, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, label, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<string> source, Label label, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    label.Text = e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // <T> -> Label

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, Label label, bool rebindOnError = true)
        {
            BindToCore(source, label, label.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, Label label, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, label, cancellationToken, rebindOnError).Forget();
        }

        public static void BindTo<T>(this AsyncReactiveProperty<T> source, Label label, bool rebindOnError = true)
        {
            BindToCore(source, label, label.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, Label label, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    label.Text = e.Current.ToString();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
        
        // <string> -> RichTextLabel

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, RichTextLabel label, bool rebindOnError = true)
        {
            BindToCore(source, label, label.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, RichTextLabel label, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, label, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<string> source, RichTextLabel label, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    label.Text = e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // <T> -> RichTextLabel

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, RichTextLabel label, bool rebindOnError = true)
        {
            BindToCore(source, label, label.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, RichTextLabel label, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, label, cancellationToken, rebindOnError).Forget();
        }

        public static void BindTo<T>(this AsyncReactiveProperty<T> source, RichTextLabel label, bool rebindOnError = true)
        {
            BindToCore(source, label, label.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, RichTextLabel label, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    label.Text = e.Current.ToString();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
        

        // <bool> -> BaseButton

        public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, BaseButton button, bool rebindOnError = true)
        {
            BindToCore(source, button, button.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, BaseButton button, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, button, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<bool> source, BaseButton button, CancellationToken cancellationToken, bool rebindOnError)
        {
            button.ToggleMode = true;
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    button.ButtonPressed = e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // <T> -> Action

        public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject node, Action<TObject, TSource> bindAction, bool rebindOnError = true)
            where TObject : Node
        {
            BindToCore(source, node, bindAction, node.GetCancellationTokenOnExitTree(), rebindOnError).Forget();
        }

        public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, bindTarget, bindAction, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore<TSource, TObject>(IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    bindAction(bindTarget, e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
    }
}
