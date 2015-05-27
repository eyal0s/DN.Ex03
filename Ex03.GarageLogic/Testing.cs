using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Testing
    {
        static void Main(string[] args)
        {
            A a = new B();
            Console.WriteLine("printing..");
            Console.WriteLine(a.ToString());
            ((B)a).onlyInB();
            Console.ReadLine();
        }

        public class A 
        {
            public virtual string foo()
            {
                return "foo in A";
            }

            public override string ToString()
            {
                return "Class A!";
            }
        }

        public class B : A
        {
            public void onlyInB() 
            {
                Console.WriteLine("Only In B");
            }
            public new string foo() 
            {
                return "foo in B";
            }
            public override string ToString()
            {
                return base.ToString() + "Class B!";
            }
        }

        public class C : B 
        {
            public override string ToString()
            {
                string s = "Class C";
                return string.Format("{0} , {1}", base.ToString(),  s);
            }

        }
    }
}
