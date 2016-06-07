using GoldKeyLib.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GoldKeyLib.Entities
{
    /// <summary>
    /// Provides standard functionality for all business objects
    /// </summary>

    public abstract class BaseEntity : IDataErrorInfo, INotifyPropertyChanged, IDisposable
    {
        #region Constants

        //protected const string cnLastUpdateUser = "LastUpdateUser";
        //protected const string cnRowState = "RowState";

        /// <summary>
        /// Defines the valid entity states
        /// </summary>
        protected internal enum EntityStateType
        {
            Unchanged,
            Added,
            Deleted,
            Modified,
        }

        #endregion Constants

        #region Properties

        private EntityStateType _entitystate;

        /// <summary>
        /// Defines the business object state
        /// </summary>
        /// <value>EntityStateEnum</value>
        /// <returns>Value identifying the entity's state</returns>
        /// <remarks></remarks>
        [BrowsableAttribute(false)]
        [BindableAttribute(false)]
        protected EntityStateType EntityState
        {
            get { return _entitystate; }
            set { _entitystate = value; }
        }

        [BindableAttribute(false)]
        [BrowsableAttribute(false)]
        public bool IsDirty
        {
            get { return this.EntityState != EntityStateType.Unchanged; }
        }

        [BindableAttribute(false)]
        [BrowsableAttribute(false)]
        public bool IsNew
        {
            get { return this.EntityState == EntityStateType.Added; }
        }

        /// <summary>
        /// Defines whether the business object is valid
        /// </summary>
        /// <returns>True if it is valid;False if it is not valid</returns>
        /// <remarks>This property should not be browsable in the Properties Window
        /// or Data Sources window</remarks>
        [BrowsableAttribute(false)]
        [BindableAttribute(false)]
        public bool IsValid
        {
            get { return false; }
        }

        private Validation _validator;

        ///// <summary>
        ///// Expose the validation instance for use by the business objects
        ///// </summary>
        ///// <value>Instance of the Validation class</value>
        ///// <returns>Instance of the Validation class</returns>
        ///// <remarks>By using one instance of the Validation class for
        ///// a business object, all validation errors/rules will be
        ///// managed as one unit</remarks>
        [BrowsableAttribute(true)]
        [BindableAttribute(true)]
        protected Validation Validator
        {
            get { return _validator; }
            private set { _validator = value; }
        }

        #endregion Properties

        #region Constructor

        protected BaseEntity()
        {
            // Create the instance of the validation class
            this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }

        #endregion Constructor

        #region IDataErrorInfo Members

        [BrowsableAttribute(false)]
        [BindableAttribute(false)]
        public string Error
        {
            get { return Validator.ToString(); }
        }

        [BrowsableAttribute(false)]
        [BindableAttribute(false)]
        public string this[string columnName]
        {
            get { return Validator[columnName]; }
        }

        #endregion IDataErrorInfo Members

        #region Property changed

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Property changed

        #region SetEntityState

        protected internal void SetEntityState(EntityStateType newEntityState)
        {
            switch (newEntityState)
            {
                case EntityStateType.Deleted:
                case EntityStateType.Unchanged:
                case EntityStateType.Added:
                    // If the new state is deleted, mark it as deleted
                    // If the new state is unchanged, mark it as unchanged
                    // If the new state is added, mark it as added
                    this._entitystate = newEntityState;
                    break;

                default:
                    // Only set other data states if the existing state is unchanged
                    if (this._entitystate == EntityStateType.Unchanged)
                        this._entitystate = newEntityState;
                    break;
            }
        }

        protected internal void SetEntityState(EntityStateType newEntityState, string propertyName)
        {
            SetEntityState(newEntityState);

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion SetEntityState

        #region Lock

        private bool _lock = false;

        public bool Lock
        {
            get { return _lock; }
            set { _lock = value; }
        }

        #endregion Lock

        //[System.AttributeUsage(System.AttributeTargets.Property)]
        //public class ValidationAttribute : Attribute
        //{
        //    public virtual bool IsValid(object obj)
        //    {
        //        return false;
        //    }
        //}

        //public static bool Validate(this object value, string propertyName)
        //{
        //    var result = true;

        //    PropertyInfo propertyInfo = value.GetType().GetProperty(propertyName);

        //    foreach (var attribute in propertyInfo.GetCustomAttributes(true))
        //    {
        //        if (attribute is ValidationAttribute)
        //        {
        //            try
        //            {
        //                var attr = (ValidationAttribute)attribute;
        //                result = result && attr.IsValid(propertyInfo.GetValue(this, null));
        //            }
        //            catch (Exception)
        //            {
        //                result = false;
        //            }
        //        }
        //    }

        //    return result;
        //}




        // Flag: Has Dispose already been called? 
        bool _disposed = false;

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
            }

            // Free any unmanaged objects here. 
            //
            _disposed = true;
        }

    }



    public static class ObjectExtension
    {
        public static List<string> GetProperties(this object value)
        {
            return value.GetType().GetProperties().Select(z => z.Name).ToList();
        }

        public static object GetPropertyValue(this object value, string propertyName)
        {
            PropertyInfo propertyInfo = value.GetType().GetProperty(propertyName);
            return propertyInfo.GetValue(value, null);
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> filterParam)
        {
            return list.Where(filterParam);
        }

        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }
    }



    #region Basic Observer Pattern

    /// <summary>
    ///   Used to push messages to the UI and as a helper for syncing between forms etc.
    ///
    ///   Implemented as a Singleton .. that inherits from IObservable
    ///   All notifying objects, forms etc inherit and implement IObserver
    /// </summary>

    #region Observer Interfaces

    public interface IObserver
    {
        
        void Notify(string sStatusMessage);

        //void Notify(string sStatusMessage, int iProgress);

        void Notify(ObserverEventTypes nEventType, string sStatus, int iProgress);
    }

    public interface IObservable
    {
        void Register(IObserver anObserver);

        void UnRegister(IObserver anObserver);
    }

    #endregion Observer Interfaces

    #region ObserveringTypes etc

    public enum ObserverEventTypes
    {
        NonEvent = -1,
        TimerElapsed = 0,
        TextUpdate = 1,
        ProgressUpdateOverAll = 3,
        ProgressUpdateIndividual = 4
    }

    #endregion ObserveringTypes etc

    public sealed class Observer : IObservable
    {
        private static volatile Observer _notifier = null;
        private static object _lock = new Object();

        private Observer()
        { }

        public static Observer Notifier
        {
            get
            {
                if (_notifier == null)
                {
                    lock (_lock)
                    {
                        if (_notifier == null)
                        {
                            _notifier = new Observer();
                        }
                    }
                }
                return _notifier;
            }
        }

        private Hashtable _observers = new Hashtable();

        public void Register(IObserver observer)
        {
            _observers.Add(observer, observer);
        }

        public void UnRegister(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(string sMessage)
        {
            foreach (IObserver anObserver in _observers.Keys)
            {
                anObserver.Notify(sMessage);
            }
        }

        //public void NotifyObservers(string sMessage, int iProgress)
        //{
        //    foreach (IObserver anObserver in mObservers.Keys)
        //    {
        //        Application.DoEvents();
        //        anObserver.Notify(sMessage, iProgress);
        //    }
        //}

        //public void NotifyObservers(ObserverEventTypes nEventType, string sStatus, int iProgress)
        //{
        //    foreach (IObserver anObserver in mObservers.Keys)
        //    {
        //        Application.DoEvents();
        //        anObserver.Notify(nEventType, sStatus, iProgress);
        //    }
        //}
    }

    #endregion Basic Observer Pattern
}