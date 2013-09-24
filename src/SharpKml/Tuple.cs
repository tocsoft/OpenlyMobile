using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpKml
{
    public class Tuple
    {
        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 itm1, T2 itm2, T3 itm3)
        {
            return new Tuple<T1, T2, T3>(itm1, itm2, itm3);
        }

        public static Tuple<T1, T2> Create<T1, T2>(T1 itm1, T2 itm2)
        {
            return new Tuple<T1, T2>(itm1, itm2);
        }
    }
    public class Tuple<T1, T2> : Tuple
    {

        public Tuple(T1 itm1, T2 itm2)
        {
            Item1 = itm1;
            Item2 = itm2;
        }
        public T1 Item1 {get;private set;}
        public T2 Item2 {get;private set;}
    }
    public class Tuple<T1,T2,T3> : Tuple<T1,T2>
    {
        public Tuple(T1 itm1, T2 itm2, T3 itm3) : base(itm1,itm2)
        {
            Item3 = itm3;
        }
        public T3 Item3 { get; private set; }
    }

    interface ICustomAttributeProvider { 
    
    }
}
