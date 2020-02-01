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
        private IList<IBeerXmlEntity> _children { get; set; } = null;

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
            this._children = new List<IBeerXmlEntity>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLRecordSetBase{T}"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        internal BeerXMLRecordSetBase(IList<IBeerXmlEntity> children)
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
            bool result = true;
            foreach (IBeerXmlEntity child in _children)
            {
                result &= child.IsValid(ref errorCode);
            }

            return result;
        }

        /// <summary>
        /// Add a record to this set
        /// </summary>
        /// <param name="child">Child record to add</param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(IBeerXmlEntity child)
        {
            if (child.GetType() != typeof(T))
            {
                throw new ArgumentException(
                    string.Format(
                        "Cannot add child of type [{0}] to record set of type [{1}]. Child must be of type [{2}]",
                        child.GetType(),
                        this.GetType(),
                        typeof(T)));
            }

            this._children.Add(child);
        }

        /// <summary>
        /// Removes a record from this set
        /// </summary>
        /// <param name="child">Child record to remove</param>
        public void Remove(IBeerXmlEntity child)
        {
            this._children.Remove(child);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IBeerXmlEntity> GetEnumerator()
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
