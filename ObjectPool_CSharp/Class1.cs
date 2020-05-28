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

        //  property
        public Dictionary<string, Node> mtable;

        public int msize;

        //  method
        public GameObjectPool(int size = 30)
        {
            msize = size;
            mtable = new Dictionary<string, Node>(msize);
        }
        ~GameObjectPool()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
        }
        public void Add(in string key, in GameObject gameObject)
        {
            mtable.Add(key, new Node(gameObject));
        }
        public bool TryAdd(in string key, in GameObject gameObject)
        {
            return mtable.TryAdd(key, new Node(gameObject));
        }
        public bool Remove(in string key)
        {
            return mtable.Remove(key);
        }
        public void Clear()
        {
            mtable.Clear();
        }
        public bool Containskey(in string key)
        {
            return mtable.ContainsKey(key);
        }


        public int SIZE
        {
            get => msize;
        }
        public Node this[string key]
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
