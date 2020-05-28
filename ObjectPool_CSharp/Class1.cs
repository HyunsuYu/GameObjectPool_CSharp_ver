using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool_CSharp
{
    sealed unsafe public class GameObjectPool : IDisposable
    {
        //  sub
        public struct Node
        {
            public bool mbactive;
            public GameObject mgameObject;

            public Node(in GameObject gameObject)
            {
                mbactive = true;
                mgameObject = gameObject;
            }
        }
        public struct Coord
        {
            public readonly int mx;
            public readonly int my;

            public Coord(int x, int y)
            {
                mx = x;
                my = y;
            }
            public static bool operator==(Coord a, Coord b)
            {
                return (a.mx == b.mx) && (a.my == b.my);
            }
            public static bool operator!=(Coord a, Coord b)
            {
                return !((a.mx == b.mx) && (a.my == b.my));
            }
        }

        //  property
        public Dictionary<Coord, Node> mtable;

        public int msize;

        //  method
        public GameObjectPool(int size = 30)
        {
            msize = size;
            mtable = new Dictionary<Coord, Node>(msize);
        }
        ~GameObjectPool()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
        }
        public void Add(in Coord key, in GameObject gameObject)
        {
            mtable.Add(key, new Node(gameObject));
        }
        public bool TryAdd(in Coord key, in GameObject gameObject)
        {
            return mtable.TryAdd(key, new Node(gameObject));
        }
        public bool Remove(in Coord key)
        {
            return mtable.Remove(key);
        }
        public void Clear()
        {
            mtable.Clear();
        }
        public bool Containskey(in Coord key)
        {
            return mtable.ContainsKey(key);
        }


        public int SIZE
        {
            get => msize;
        }
        public Node this[Coord key]
        {
            get
            {
                return mtable[key];
            }
        }


        private void Dispose(bool bisDispose)
        {
            if(!bisDispose)
            {
                return;
            }

            mtable = null;
        }
    }
}
