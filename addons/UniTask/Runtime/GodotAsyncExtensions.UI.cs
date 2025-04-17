#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
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

        public static AsyncGodotSignalHandler<bool> GetAsyncToggleChangedEventHandler(this BaseButton button)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled, button.GetCancellationTokenOnExitTree(), false);
        }

        public static AsyncGodotSignalHandler<bool> GetAsyncToggleChangedEventHandler(this BaseButton button, CancellationToken cancellationToken)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled, cancellationToken, false);
        }

        public static UniTask<bool> OnToggleChangedAsync(this BaseButton button)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled, button.GetCancellationTokenOnExitTree(),true).OnInvokeAsync();
        }

        public static UniTask<bool> OnToggleChangedAsync(this BaseButton button, CancellationToken cancellationToken)
        {
            button.ToggleMode = true;
            return new AsyncGodotSignalHandler<bool>(button,BaseButton.SignalName.Toggled,  cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<bool> OnToggleChangedAsAsyncEnumerable(this BaseButton button)
        {
            button.ToggleMode = true;
            return new GodotSignalHandlerAsyncEnumerable<bool>(button,BaseButton.SignalName.Toggled, button.GetCancellationTokenOnExitTree());
        }

        public static IUniTaskAsyncEnumerable<bool> OnToggleChangedAsAsyncEnumerable(this BaseButton button, CancellationToken cancellationToken)
        {
            button.ToggleMode = true;
            return new GodotSignalHandlerAsyncEnumerable<bool>(button,BaseButton.SignalName.Toggled, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncToggleChangedEventHandler(this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncToggleChangedEventHandler(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, false);
        }

        public static UniTask<float> OnToggleChangedAsync(this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<float> OnToggleChangedAsync(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<float> OnToggleChangedAsAsyncEnumerable(this Scrollbar scrollbar)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<float> OnToggleChangedAsAsyncEnumerable(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(scrollbar.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncToggleChangedEventHandler(this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncToggleChangedEventHandler(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, false);
        }

        public static UniTask<Vector2> OnToggleChangedAsync(this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<Vector2> OnToggleChangedAsync(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<Vector2> OnToggleChangedAsAsyncEnumerable(this ScrollRect scrollRect)
        {
            return new UnityEventHandlerAsyncEnumerable<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<Vector2> OnToggleChangedAsAsyncEnumerable(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<Vector2>(scrollRect.onValueChanged, cancellationToken);
        }

        public static AsyncGodotSignalHandler<double> GetAsyncToggleChangedEventHandler(this Range range)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, range.GetCancellationTokenOnExitTree(), false);
        }

        public static AsyncGodotSignalHandler<double> GetAsyncToggleChangedEventHandler(this Range range, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, cancellationToken, false);
        }

        public static UniTask<double> OnToggleChangedAsync(this Range range)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, range.GetCancellationTokenOnExitTree(), true).OnInvokeAsync();
        }

        public static UniTask<double> OnToggleChangedAsync(this Range range, CancellationToken cancellationToken)
        {
            return new AsyncGodotSignalHandler<double>(range,Range.SignalName.ValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<double> OnToggleChangedAsAsyncEnumerable(this Range range)
        {
            return new GodotSignalHandlerAsyncEnumerable<double>(range,Range.SignalName.ValueChanged, range.GetCancellationTokenOnExitTree());
        }

        public static IUniTaskAsyncEnumerable<double> OnToggleChangedAsAsyncEnumerable(this Range range, CancellationToken cancellationToken)
        {
            return new GodotSignalHandlerAsyncEnumerable<double>(range,Range.SignalName.ValueChanged, cancellationToken);
        }

        public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, false);
        }

        public static UniTask<string> OnEndEditAsync(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<string> OnEndEditAsync(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(this InputField inputField)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(this InputField inputField, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onEndEdit, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<string> GetAsyncToggleChangedEventHandler(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<string> GetAsyncToggleChangedEventHandler(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, false);
        }

        public static UniTask<string> OnToggleChangedAsync(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<string> OnToggleChangedAsync(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<string> OnToggleChangedAsAsyncEnumerable(this InputField inputField)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<string> OnToggleChangedAsAsyncEnumerable(this InputField inputField, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncToggleChangedEventHandler(this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncToggleChangedEventHandler(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, false);
        }

        public static UniTask<int> OnToggleChangedAsync(this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<int> OnToggleChangedAsync(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<int> OnToggleChangedAsAsyncEnumerable(this Dropdown dropdown)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<int> OnToggleChangedAsAsyncEnumerable(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, cancellationToken);
        }
    }
}
