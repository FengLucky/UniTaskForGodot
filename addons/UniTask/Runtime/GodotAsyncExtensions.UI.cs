#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Threading;
using Godot;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cysharp.Threading.Tasks
{
    public static partial class GodotAsyncExtensions
    {
        public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button)
        {
            return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(button.onClick, cancellationToken, false);
        }

        public static UniTask OnClickAsync(this Button button)
        {
            return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask OnClickAsync(this Button button, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(button.onClick, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> OnClickAsAsyncEnumerable(this Button button)
        {
            return new UnityEventHandlerAsyncEnumerable(button.onClick, button.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> OnClickAsAsyncEnumerable(this Button button, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable(button.onClick, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(this Toggle toggle)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(this Toggle toggle, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, false);
        }

        public static UniTask<bool> OnValueChangedAsync(this Toggle toggle)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<bool> OnValueChangedAsync(this Toggle toggle, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<bool> OnValueChangedAsAsyncEnumerable(this Toggle toggle)
        {
            return new UnityEventHandlerAsyncEnumerable<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<bool> OnValueChangedAsAsyncEnumerable(this Toggle toggle, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<bool>(toggle.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, false);
        }

        public static UniTask<float> OnValueChangedAsync(this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<float> OnValueChangedAsync(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Scrollbar scrollbar)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(scrollbar.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, false);
        }

        public static UniTask<Vector2> OnValueChangedAsync(this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<Vector2> OnValueChangedAsync(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<Vector2> OnValueChangedAsAsyncEnumerable(this ScrollRect scrollRect)
        {
            return new UnityEventHandlerAsyncEnumerable<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<Vector2> OnValueChangedAsAsyncEnumerable(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<Vector2>(scrollRect.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Slider slider)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Slider slider, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, false);
        }

        public static UniTask<float> OnValueChangedAsync(this Slider slider)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<float> OnValueChangedAsync(this Slider slider, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Slider slider)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Slider slider, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(slider.onValueChanged, cancellationToken);
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

        public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, false);
        }

        public static UniTask<string> OnValueChangedAsync(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<string> OnValueChangedAsync(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(this InputField inputField)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(this InputField inputField, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, false);
        }

        public static UniTask<int> OnValueChangedAsync(this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<int> OnValueChangedAsync(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(this Dropdown dropdown)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, cancellationToken);
        }
    }

    public interface IAsyncClickEventHandler : IDisposable
    {
        UniTask OnClickAsync();
    }

    public interface IAsyncValueChangedEventHandler<T> : IDisposable
    {
        UniTask<T> OnValueChangedAsync();
    }

    public interface IAsyncEndEditEventHandler<T> : IDisposable
    {
        UniTask<T> OnEndEditAsync();
    }
}
