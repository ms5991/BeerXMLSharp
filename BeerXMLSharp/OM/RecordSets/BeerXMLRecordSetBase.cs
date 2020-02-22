using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Base class for all BeerXML Record Sets
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="BeerXMLSharp.OM.BeerXMLEntityBase" />
    /// <seealso cref="BeerXMLSharp.OM.IRecordSet" />
    public abstract class BeerXMLRecordSetBase<T> : BeerXMLEntityBase, IRecordSet
    {
        private IList<IRecord> _children { get; set; } = null;

        /// <summary>
        /// Number of items in this set
        /// </summary>
        public int Count
        {
            get
            {
                return this._children.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLRecordSetBase{T}"/> class.
        /// </summary>
        internal BeerXMLRecordSetBase() : base()
        {
            this._children = new List<IRecord>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLRecordSetBase{T}"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        internal BeerXMLRecordSetBase(IList<IRecord> children)
        {
            this._children = children;
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        public override bool IsValid(ref ValidationCode errorCode)
        {
            return IsValidRecordSet(ref errorCode, suppressTypeCheck: false) & base.IsValid(ref errorCode);
        }

        /// <summary>
        /// Internal method used for testing purposes to allow bypassing the type check
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="suppressTypeCheck"></param>
        /// <returns></returns>
        internal bool IsValidRecordSet(ref ValidationCode errorCode, bool suppressTypeCheck)
        {
            bool result = true;
            foreach (IBeerXMLEntity child in _children)
            {
                if (!suppressTypeCheck && child.GetType() != typeof(T))
                {
                    result = false;
                    errorCode |= ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE;
                }

                result &= child.IsValid(ref errorCode);
            }

            return result;
        }

        /// <summary>
        /// Add a record to this set
        /// </summary>
        /// <param name="child">Child record to add</param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(IRecord child)
        {
            this._children.Add(child);
        }

        /// <summary>
        /// Removes a record from this set
        /// </summary>
        /// <param name="child">Child record to remove</param>
        public void Remove(IRecord child)
        {
            this._children.Remove(child);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IRecord> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)_children.GetEnumerator();
        }
    }
}
