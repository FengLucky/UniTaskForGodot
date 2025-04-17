#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Threading;
using Godot;
using Range = Godot.Range;

namespace Cysharp.Threading.Tasks
{
    public static partial class GodotAsyncExtensions
    {
        public static AsyncGodotSignalHandler GetAsyncPressedEventHandler(this BaseButton button)
        {
            return new AsyncGodotSignalHandler(button,BaseButton.SignalName.Pressed, button.GetCancellationTokenOnExitTree(), false);
        }

        public static AsyncGodotSignalHandler GetAsyncPressedEventHandler(this BaseButton button, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler(button,BaseButton.SignalName.Pressed, cancellationToken, false);
        }

        public static UniTask OnPressedAsync(this BaseButton button)
        {
            return new AsyncGodotSignalHandler(button,BaseButton.SignalName.Pressed, button.GetCancellationTokenOnExitTree(), true).OnInvokeAsync();
        }

        public static UniTask OnPressedAsync(this BaseButton button, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler(button,BaseButton.SignalName.Pressed, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> OnPressedAsAsyncEnumerable(this BaseButton button)
        {
            return new GodotSignalHandlerAsyncEnumerable(button,BaseButton.SignalName.Pressed, button.GetCancellationTokenOnExitTree());
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> OnPressedAsAsyncEnumerable(this BaseButton button, CancellationToken cancellationToken)
        {
            return new GodotSignalHandlerAsyncEnumerable(button,BaseButton.SignalName.Pressed, cancellationToken);
        }

        public static AsyncGodotSignalHandler<bool> GetAsyncSelectChangedEventHandler(this BaseButton button)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled, button.GetCancellationTokenOnExitTree(), false);
        }

        public static AsyncGodotSignalHandler<bool> GetAsyncSelectChangedEventHandler(this BaseButton button, CancellationToken cancellationToken)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled, cancellationToken, false);
        }

        public static UniTask<bool> OnSelectChangedAsync(this BaseButton button)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled, button.GetCancellationTokenOnExitTree(),true).OnInvokeAsync();
        }

        public static UniTask<bool> OnSelectChangedAsync(this BaseButton button, CancellationToken cancellationToken)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled,  cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<bool> OnSelectChangedAsAsyncEnumerable(this BaseButton button)
        {
            button.ToggleMode = true;
            return new GodotSignalHandlerAsyncEnumerable<bool>(button,BaseButton.SignalName.Toggled, button.GetCancellationTokenOnExitTree());
        }

        public static IUniTaskAsyncEnumerable<bool> OnSelectChangedAsAsyncEnumerable(this BaseButton button, CancellationToken cancellationToken)
        {
            button.ToggleMode = true;
            return new GodotSignalHandlerAsyncEnumerable<bool>(button,BaseButton.SignalName.Toggled, cancellationToken);
        }

        public static AsyncGodotSignalHandler<double> GetAsyncSelectChangedEventHandler(this Range range)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, range.GetCancellationTokenOnExitTree(), false);
        }

        public static AsyncGodotSignalHandler<double> GetAsyncSelectChangedEventHandler(this Range range, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, cancellationToken, false);
        }

        public static UniTask<double> OnSelectChangedAsync(this Range range)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, range.GetCancellationTokenOnExitTree(), true).OnInvokeAsync();
        }

        public static UniTask<double> OnSelectChangedAsync(this Range range, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<double> OnSelectChangedAsAsyncEnumerable(this Range range)
        {
            return new GodotSignalHandlerAsyncEnumerable<double>(range,Range.SignalName.ValueChanged, range.GetCancellationTokenOnExitTree());
        }

        public static IUniTaskAsyncEnumerable<double> OnSelectChangedAsAsyncEnumerable(this Range range, CancellationToken cancellationToken)
        {
            return new GodotSignalHandlerAsyncEnumerable<double>(range,Range.SignalName.ValueChanged, cancellationToken);
        }

        public static AsyncGodotSignalHandler GetAsyncTextChangedEventHandler(this TextEdit edit)
        {
            return new AsyncGodotSignalHandler(edit,TextEdit.SignalName.TextChanged, edit.GetCancellationTokenOnExitTree(), false);
        }

        public static AsyncGodotSignalHandler GetAsyncTextChangedEventHandler(this TextEdit edit, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler(edit,TextEdit.SignalName.TextChanged, cancellationToken, false);
        }

        public static UniTask OnTextChangedAsync(this TextEdit edit)
        {
            return new AsyncGodotSignalHandler(edit,TextEdit.SignalName.TextChanged, edit.GetCancellationTokenOnExitTree(), true).OnInvokeAsync();
        }

        public static UniTask OnTextChangedAsync(this TextEdit edit, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler(edit,TextEdit.SignalName.TextChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> OnTextChangedAsAsyncEnumerable(this TextEdit edit)
        {
            return new GodotSignalHandlerAsyncEnumerable(edit,TextEdit.SignalName.TextChanged, edit.GetCancellationTokenOnExitTree());
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> OnTextChangedAsAsyncEnumerable(this TextEdit edit, CancellationToken cancellationToken)
        {
            return new GodotSignalHandlerAsyncEnumerable(edit,TextEdit.SignalName.TextChanged, cancellationToken);
        }

        public static AsyncGodotSignalHandler<long> GetAsyncSelectChangedEventHandler(this OptionButton optionButton)
        {
            return new AsyncGodotSignalHandler<long>(optionButton,OptionButton.SignalName.ItemSelected, optionButton.GetCancellationTokenOnExitTree(), false);
        }

        public static AsyncGodotSignalHandler<long> GetAsyncSelectChangedEventHandler(this OptionButton optionButton, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler<long>(optionButton,OptionButton.SignalName.ItemSelected, cancellationToken, false);
        }

        public static UniTask<long> OnSelectChangedAsync(this OptionButton optionButton)
        {
            return new AsyncGodotSignalHandler<long>(optionButton,OptionButton.SignalName.ItemSelected, optionButton.GetCancellationTokenOnExitTree(), true).OnInvokeAsync();
        }

        public static UniTask<long> OnSelectChangedAsync(this OptionButton optionButton, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler<long>(optionButton,OptionButton.SignalName.ItemSelected, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<long> OnSelectChangedAsAsyncEnumerable(this OptionButton optionButton)
        {
            return new GodotSignalHandlerAsyncEnumerable<long>(optionButton,OptionButton.SignalName.ItemSelected, optionButton.GetCancellationTokenOnExitTree());
        }

        public static IUniTaskAsyncEnumerable<long> OnSelectChangedAsAsyncEnumerable(this OptionButton optionButton, CancellationToken cancellationToken)
        {
            return new GodotSignalHandlerAsyncEnumerable<long>(optionButton,OptionButton.SignalName.ItemSelected, cancellationToken);
        }
    }
}
