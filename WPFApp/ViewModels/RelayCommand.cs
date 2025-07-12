using System.Diagnostics;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        private readonly Action<Exception> _errorHandler;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <param name="errorHandler">Optional error handling logic.</param>
        public RelayCommand(Action<object> execute,
                          Predicate<object> canExecute = null,
                          Action<Exception> errorHandler = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class for no-parameter commands.
        /// </summary>
        public RelayCommand(Action execute,
                          Func<bool> canExecute = null,
                          Action<Exception> errorHandler = null)
            : this(_ => execute(),
                  canExecute != null ? _ => canExecute() : null,
                  errorHandler)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        #region ICommand Members

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null || _canExecute(parameter);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        public void Execute(object parameter)
        {
            try
            {
                _execute(parameter);
            }
            catch (Exception ex)
            {
                _errorHandler?.Invoke(ex);
#if DEBUG
                // Break into debugger in development
                if (_errorHandler == null)
                    Debugger.Break();
#endif
            }
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion
    }
}