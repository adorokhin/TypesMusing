using System;
using System.Diagnostics;

namespace ConsoleApplication
{
    public class Program
    {
        public class A
        {
            public string speak() { return "#$!&*"; }
        }
        public class B { }
        public struct Str
        {
            public A a;
        }
        enum TestEnum { uno, dos, tres };
        public static void useStruct(Str s)
        {
            s.a.speak();
        }
        public static void Main(string[] args)
        {
            #region [Uses for different types]
            {
                //floating types
                decimal dec_pi = 3.141592653589793238462643383279502884197169399375105820974944592307816406286M; //significant digits limit
                double d_pi = System.Math.PI;
                float f_pi = (float)d_pi;

                //enums
                TestEnum testEnum = TestEnum.uno;
                Console.WriteLine(testEnum.ToString());

                //Code Snippets
                //switch...
            }
            #endregion

            #region [CLR Synonyms]
            {
                int i;
                System.Int32 si;
                DateTime dt;
                string sNull;
                string s = "string";

                System.Console.WriteLine(typeof(string));
                System.Console.WriteLine(typeof(System.String));

                Debug.Assert(typeof(string) == typeof(System.String));
                Debug.Assert(s.GetType() == typeof(System.String));

                Console.WriteLine((s is string) ? "yes" : "no");
            }
            #endregion

            #region [is, as]
            {
                string s = "string";
                Console.WriteLine((s is string) ? "yes" : "no");

                //stolen from MSDN
                object[] objArray = new object[6];
                objArray[0] = new A();
                objArray[1] = new B();
                objArray[2] = "hello";
                objArray[3] = 123;
                objArray[4] = 123.4;
                objArray[5] = null;

                for (int i = 0; i < objArray.Length; ++i)
                {
                    s = objArray[i] as string;
                    Console.Write("{0}:", i);
                    if (s != null)
                        Console.WriteLine("'" + s + "'");
                    else
                        Console.WriteLine("not a string");
                }
            }
            #endregion

            #region [Gotchas]
            {
                A a;
                A[] arr = new A[10];
                foreach (var aa in arr)
                {
                    System.Console.WriteLine(aa.speak());
                }

                Str str;
                str.a = null;
                useStruct(str);

                Str str1 = new Str();
                useStruct(str1);

                string s = "string";
                s.ToUpper();
            }
            #endregion

            #region [defaults]
            object obj = default(decimal);
            #endregion

            System.Console.WriteLine("Thats All Folks!");
            System.Console.ReadLine();

        }
    }
}
