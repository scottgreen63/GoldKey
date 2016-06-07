using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GoldKeyLib.Entities
{

    public abstract class BaseEntityList<T> : BindingList<T> where T : BaseEntity, new()
    {
        #region Properties

        //private List<T> mDeletedItemsList = new List<T>();

        //protected List<T> DeletedItemsList
        //{
        //    get
        //    {
        //        if (mDeletedItemsList == null)
        //        {
        //            mDeletedItemsList = new List<T>();
        //        }
        //        return mDeletedItemsList;
        //    }
        //}

        //public bool IsDirty
        //{
        //    get
        //    {
        //        // This list is dirty if any of the elements in the list are dirty
        //        bool anyDirty = this.Any(item => item.IsDirty);

        //        // List is also dirty if there are deleted items
        //        if (!anyDirty)
        //        {
        //            anyDirty = DeletedItemsList.Count != 0;
        //        }
        //        return anyDirty;
        //    }
        //}

        //public bool IsValid
        //{
        //    get
        //    {
        //        // This list is valid only if all of the items in the list are valid
        //        bool allValid = this.All(item => item.IsValid);
        //        return allValid;
        //    }
        //}
        private User _currentuser;
        protected User CurrentUser
        {
            get { return _currentuser; }
            set { _currentuser = value; }
        }
        #endregion Properties

        #region "Methods"

        //#region DeleteItem
        //        /// <summary>
        //        /// Deletes the defined item from the list.
        //        /// </summary>
        //        /// <param name="item">Item to delete.</param>
        //        public void DeleteItem(T item)
        //        {
        //            // Ensure the item is marked for deletion
        //            item.SetEntityState(CMMSBase.EntityStateType.Deleted);

        //            // Add it to the deleted items list
        //            this.DeletedItemsList.Add(item);

        //            // Remove the item from "this" list
        //            base.Remove(item);
        //        }
        //#endregion

        //#region AddItem

        //public void AddItem(T item)
        //{
        //    // Ensure the item is marked for addition
        //    item.SetEntityState(CMMSBase.EntityStateType.Added);

        //    // Add it items list
        //    // Remove the item from "this" list
        //    base.Add(item);
        //}
        //#endregion

        //#region Save
        ///// <summary>
        ///// Performs the save.
        ///// </summary>
        //public void SaveItem()
        //{
        //    //Perform the deletions first

        //    //Process each deleted entry
        //    foreach (T item in DeletedItemsList)
        //        //"Save" the delete
        //        item.SaveItem();

        //    // Clear the deleted items list
        //    _DeletedItemsList = null;

        //    //Process each entry in the binding list
        //    foreach (T item in this)
        //    {
        //        if (item.IsDirty)
        //        {
        //            // Save the item
        //            item.SaveItem();

        //            // Clear the change state
        //            item.SetEntityState(PWSBase.EntityStateType.Unchanged);
        //        }
        //    }
        //}
        //#endregion

        #endregion "Methods"

        #region "Searching Support"

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override int FindCore(System.ComponentModel.PropertyDescriptor prop, object key)
        {
            for (int i = 0; i <= this.Count - 1; i++)
            {
                T item = base.Items[i];

                if (prop.GetValue(item).Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion "Searching Support"

        #region "Sorting Support"

        private bool mSorted = false;
        private ListSortDirection mSortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor mSortProperty = null;
        private List<T> mOriginalCollection = new List<T>();

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get
            {
                return mSorted;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return mSortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return mSortProperty;
            }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            mSortDirection = direction;
            mSortProperty = prop;
            BaseEntitySortComparer<T> comparer = new BaseEntitySortComparer<T>(prop, direction);
            ApplySortInternal(comparer);
        }

        private void ApplySortInternal(BaseEntitySortComparer<T> comparer)
        {
            if (mOriginalCollection.Count == 0)
            {
                mOriginalCollection.AddRange(this);
            }
            List<T> listRef = this.Items as List<T>;
            if (listRef == null)
            {
                return;
            }
            listRef.Sort(comparer);
            mSorted = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            if (!mSorted)
            {
                return;
            }
            Clear();
            foreach (T item in mOriginalCollection)
            {
                Add(item);
            }
            mOriginalCollection.Clear();
            mSortProperty = null;
            mSorted = false;
        }

        public new bool AllowNew
        {
            get { return !(mSorted); }
        }

        public new bool AllowRemove
        {
            get { return !(mSorted); }
        }

        #endregion "Sorting Support"


    }

    public class BaseEntitySortComparer<T> : IComparer<T>
    {
        private PropertyDescriptor mProp;
        private ListSortDirection mDirection;

        public BaseEntitySortComparer(PropertyDescriptor propdesc, ListSortDirection direction)
        {
            mProp = propdesc;
            mDirection = direction;
        }

        private int CompareValues(object xValue, object yValue, ListSortDirection direction)
        {
            int retval = 0;
            if (xValue is IComparable)
            {
                retval = ((IComparable)xValue).CompareTo(yValue);
            }
            else
            {
                if (yValue is IComparable)
                {
                    retval = ((IComparable)yValue).CompareTo(xValue);
                }
                else
                {
                    if (!xValue.Equals(yValue))
                    {
                        retval = xValue.ToString().CompareTo(yValue.ToString());
                    }
                }
            }
            if (direction == ListSortDirection.Ascending)
            {
                return retval;
            }
            else
            {
                return retval - 1;
            }
        }

        public int Compare(T x, T y)
        {
            if (mProp != null)
            {
                object xvalue = mProp.GetValue(x);
                object yvalue = mProp.GetValue(y);
                return CompareValues(xvalue, yvalue, ListSortDirection.Ascending);
            }
            //this should be possible
            return 9999;
        }
    }

    public class SingletonProvider<T> where T : new()
    {
        SingletonProvider() { }

        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }

        class SingletonCreator
        {
            static SingletonCreator() { }

            internal static readonly T instance = new T();
        }
    }



}