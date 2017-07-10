using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HeapDS.Tests
{
    [TestClass]
    public class HeapTest
    {
        [TestMethod]
        public void TestMinHeap()
        {
            var comparer = System.Collections.Generic.Comparer<int>.Create((x, y) =>
            {
                return x == y ? 0 : x > y ? 1 : -1;
            });
            var nums = new List<int>() { 0, 1, 2, 4, 5, 5, 6, 7, 9, 10, 10, 15, 25, 30 };
            IHeap<int> heap = new Heap<int>(comparer);
            foreach (var n in nums)
            {
                heap.AddItem(n);
            }

            int index = 0;
            int num;
            while (heap.TryRemoveTopItem(out num))
            {
                Assert.AreEqual(nums[index], num);
                index++;
                int newTop;
                var hasTop = heap.TryGetTopItem(out newTop);
                Console.WriteLine("Item removed: {0}, New top: {1}", num, hasTop ? newTop.ToString() : "No top");
            }
        }

        [TestMethod]
        public void TestMaxHeap()
        {
            var comparer = System.Collections.Generic.Comparer<int>.Create((x, y) =>
            {
                return x == y ? 0 : x > y ? -1 : 1;
            });
            var nums = new List<int>() { 0, 1, 2, 4, 5, 5, 6, 7, 9, 10, 10, 15, 25, 30 };
            IHeap<int> heap = new Heap<int>(comparer);
            foreach (var n in nums)
            {
                heap.AddItem(n);
            }

            int index = nums.Count - 1;
            int num;
            while (heap.TryRemoveTopItem(out num))
            {
                Assert.AreEqual(nums[index], num);
                index--;
                int newTop;
                var hasTop = heap.TryGetTopItem(out newTop);
                Console.WriteLine("Item removed: {0}, New top: {1}", num, hasTop ? newTop.ToString() : "No top");
            }
        }
    }
}
