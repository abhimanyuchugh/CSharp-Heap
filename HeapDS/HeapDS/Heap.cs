using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapDS
{
    public interface IHeap<T>
    {
        bool TryGetTopItem(out T item);
        void AddItem(T item);
        bool TryRemoveTopItem(out T item);
    }

    public class Heap<T> : IHeap<T>
    {
        private readonly IComparer<T> comparer;
        private readonly List<T> items;

        public Heap(IComparer<T> comparer)
        {
            this.comparer = comparer;
            this.items = new List<T>();
        }

        public bool TryGetTopItem(out T item)
        {
            item = default(T);
            if (items == null || !items.Any()) return false;
            item = items[0];
            return true;
        }

        public void AddItem(T item)
        {
            items.Add(item);
            if (items.Count <= 1) return;
            var itemIndex = items.Count - 1;
            // Trickle the new node up the tree until we either reach a parent which is smaller than us
            // or we reach the root
            while (itemIndex > 0)
            {
                var parentIndex = (itemIndex - 1) / 2;
                if (comparer.Compare(items[parentIndex], items[itemIndex]) <= 0)
                    break;
                items[itemIndex] = items[parentIndex];
                items[parentIndex] = item;
                itemIndex = parentIndex;
            }
        }

        public bool TryRemoveTopItem(out T item)
        {
            item = default(T);
            if (items == null || !items.Any()) return false;
            item = items[0];
            var lastItem = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            if (items.Count == 0) return true;
            items[0] = lastItem;
            HeapRebuild();
            return true;
        }

        private void HeapRebuild()
        {
            if (items.Count <= 1) return;
            var index = 0;
            while (index < items.Count - 1)
            {
                var firstChildIndex = 2 * index + 1;
                var secondChildIndex = 2 * index + 2;
                // if we've reached a leaf node, no need to continue further
                if (firstChildIndex >= items.Count && secondChildIndex >= items.Count)
                    break;
                int? childrenComparison = null;
                if (firstChildIndex < items.Count && comparer.Compare(items[index], items[firstChildIndex]) > 0 &&
                    (secondChildIndex >= items.Count || (childrenComparison = comparer.Compare(items[firstChildIndex], items[secondChildIndex])) <= 0))
                {
                    var item = items[index];
                    items[index] = items[firstChildIndex];
                    items[firstChildIndex] = item;
                    index = firstChildIndex;
                }
                else if (secondChildIndex < items.Count && comparer.Compare(items[index], items[secondChildIndex]) > 0 &&
                        (childrenComparison != null && childrenComparison > 0 ||
                        (childrenComparison = comparer.Compare(items[firstChildIndex], items[secondChildIndex])) > 0))
                {
                    var item = items[index];
                    items[index] = items[secondChildIndex];
                    items[secondChildIndex] = item;
                    index = secondChildIndex;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
