using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Wisol
{
    public static class eXtendClass
    {
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        public static bool IsNullOrEmpty(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }
        /// <summary>
        /// BaseEdit EditValue NullOrEmpty. 
        /// by jbblee.
        /// </summary>
        /// <param name="baseEdit"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DevExpress.XtraEditors.BaseEdit baseEdit)
        {
            if (baseEdit.EditValue != null)
            {
                return string.IsNullOrEmpty(baseEdit.Text);
            }
            return true;
        }
        /// <summary>
        /// Sender  IsNull.
        /// BaseEdit EditValue NullOrEmpty. 
        /// Control Text NullOrEmpty.
        /// by jbblee.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object sender)
        {
            if (sender != null)
            {
                if (sender is DevExpress.XtraEditors.BaseEdit edit)
                {
                    return edit.IsNullOrEmpty();
                }
                else if (sender is Control ctrl)
                {
                    return string.IsNullOrEmpty(ctrl.Text);
                }
                return false;
            }
            return true;
        }

        public static IEnumerable<DataRow> Rows(this DataTable dataTable)
        {
            return dataTable.Rows.Cast<DataRow>();
        }

        public static IEnumerable<DataColumn> Columns(this DataTable dataTable)
        {
            return dataTable.Columns.Cast<DataColumn>();
        }

        public static IEnumerable<DataRow> Where(this DataTable dataTable, Func<DataRow, bool> predicate)
        {
            return dataTable.Rows.Cast<DataRow>().Where(predicate);
        }

        public static IEnumerable<T> FindAll<T>(this Control.ControlCollection controls) where T : Control
        {
            var list = controls.Cast<T>().Where(x => x.GetType().Equals(typeof(T)));
            return list.Cast<T>();
        }

        public static AccordionControlElement FindBy(this IEnumerable<AccordionControlElement> source, Func<AccordionControlElement, bool> predicate)
        {
            var result = source.SingleOrDefault<AccordionControlElement>(predicate);
            if (result != null)
            {
                return result;
            }

            foreach (var control in source)
            {
                result = control.Elements.FindBy(predicate);
                if (result != null) return result;
            }
            return result;
        }

        public static int ToInt(this object value)
        {
            return int.Parse(value.ToString());
        }

        public static DataTable SelectRecursive(this DataTable dataTable, string parentKey, string childKey, object startKey, string orderBy)
        {
            var lookup = dataTable.Rows().ToLookup(x => x[parentKey]);
            var result = lookup[startKey].SelectRecursive(x => lookup[x[childKey]]).OrderBy(x => x[!string.IsNullOrEmpty(orderBy) ? orderBy : childKey]).CopyToDataTable();
            return result;
        }

        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (var parent in source)
            {
                yield return parent;

                var children = selector(parent);
                foreach (var child in SelectRecursive(children, selector))
                    yield return child;
            }
        }
        /// <summary>
        /// Hierarchy node class which contains a nested collection of hierarchy nodes
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        public class HierarchyNode<T> where T : class
        {
            public T Entity { get; set; }
            public IEnumerable<HierarchyNode<T>> ChildNodes { get; set; }
            public int Depth { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="allItems"></param>
        /// <param name="parentItem"></param>
        /// <param name="idProperty"></param>
        /// <param name="parentIdProperty"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private static IEnumerable<HierarchyNode<TEntity>> CreateHierarchy<TEntity, TProperty>(IEnumerable<TEntity> allItems, TEntity parentItem, Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty, int depth) where TEntity : class
        {
            IEnumerable<TEntity> childs;

            if (parentItem == null)
                childs = allItems.Where(i => parentIdProperty(i).Equals(default(TProperty)));
            else
                childs = allItems.Where(i => parentIdProperty(i).Equals(idProperty(parentItem)));

            if (childs.Count() > 0)
            {
                depth++;

                foreach (var item in childs)
                    yield return new HierarchyNode<TEntity>()
                    {
                        Entity = item,
                        ChildNodes = CreateHierarchy<TEntity, TProperty>(allItems, item, idProperty, parentIdProperty, depth),
                        Depth = depth
                    };
            }
        }
    }
}
