﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Common
{
    public class ConstMinSortedDLL
    {
        public int Size { get; set; }
        public int MaxSize { get; set; }
        public Node Head { get; set; }
        public Node Tail { get; set; }

        public ConstMinSortedDLL(int maxSize)
        {
            this.Head = null;
            this.Tail = null;
            this.Size = 0;
            this.MaxSize = maxSize;
        }

        public bool AddAndTrim(Node node)
        {

            this.Add(node);
            return !this.Trim();

        }

        public Dictionary<int, double> ReturnAsDictionary()
        {
            Dictionary<int, double> dictionaryFormDLL = new Dictionary<int, double>(this.Size);

            Node curr = this.Head;

            while(curr != null)
            {
                dictionaryFormDLL.Add(curr.Index, curr.Data);
                curr = curr.Next;
            }

            return dictionaryFormDLL;
        }

        private void Add(Node node)
        {
            this.Size += 1;

            // empty list condition
            if (this.Head == null)
            {
                this.Head = node;
                this.Tail = node;
                return;
            }

            // greater than head condition
            if (node.Data <= this.Head.Data)
            {
                node.Next = this.Head;
                this.Head.Prev = node;
                this.Head = node;
                return;
            }

            // find properly sorted spot in LL and store in curr
            Node curr = this.Head;
            while(curr.Next != null)
            {
                if(node.Data <= curr.Next.Data)
                {
                    break;
                }

                curr = curr.Next;
            }

            // tail condition
            if(curr.Next == null)
            {
                curr.Next = node;
                node.Prev = curr;
                this.Tail = node;
                return;
            }

            // all other conditions

            node.Next = curr.Next;
            node.Prev = curr;
            curr.Next.Prev = node;
            curr.Next = node;

        }

        private bool Trim()
        {
            if(this.Size > this.MaxSize)
            {
                Node newTail = this.Tail.Prev;
                newTail.Next = null;
                this.Tail.Prev = null;
                this.Tail = newTail;
                this.Size -= 1;

                return true;

            }

            return false;
        }

        
    }

    public class Node
    {
        
        public Node(double data, int idx, Node prev = null, Node next = null)
        {
            this.Data = data;
            this.Prev = prev;
            this.Next = next;
            this.Index = idx;
        }

        public double Data { get; set; }
        public int Index { get; set; }
        public Node Prev { get; set; }
        public Node Next { get; set; }
    } 
}