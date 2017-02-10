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

        #region [Casting]
        public class Animal
        {
            public Animal() { }
            public Animal(string anymalSays) { this.anymalSays = anymalSays; }
            public virtual string WhoAmI() { return this.GetType().ToString(); }

            public string anymalSays = "#@!$&";
        }
        public class Dog : Animal
        {
            public Dog(string anymalSpeaks) : base(anymalSpeaks) { }

            public static explicit operator Dog(Cat c)
            {
                return new Dog(c.anymalSays);
            }
        }
        public class Cat : Animal
        {
            public Cat(string anymalSpeaks) : base(anymalSpeaks) { }

            public static implicit operator Cat(Dog dog)
            {
                return new Cat(dog.anymalSays);
            }
        }
        #endregion

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
                //foreach (var aa in arr)
                //{
                //    System.Console.WriteLine(aa.speak());
                //}

                Str str;
                //str.a = null;
                //useStruct(str);

                //Str str1 = new Str();
                //useStruct(str1);

                string s = "string";
                s.ToUpper();

                double d = 1 / 7; //0.(142857)
            }
            #endregion

            #region [Casting]
            {

                //implicit
                int i = int.MaxValue;
                long l = i;


                //explicit
                i = -1;
                uint ui = 0;
                ui = (uint)i;

                float f = 0;
                f = (float)double.MaxValue;

                f = float.MaxValue + 1;
                if(float.IsInfinity(f))
                    Console.WriteLine("To Infinity and Beyond");




                try
                {
                    Dog dog = new Dog("Woof-Woof!");
                    Cat cat = new Cat("Mew?");
                    System.Console.WriteLine($"Dog says: [{dog.anymalSays}]    {dog.GetType()}");
                    System.Console.WriteLine($"Cat says: [{cat.anymalSays}]    {cat.GetType()}");
                    System.Console.WriteLine("Explicitly converting Cat to Dog");

                    dog = (Dog)cat;
                    System.Console.WriteLine($"Dog says: [{dog.anymalSays}]    {dog.GetType()}");


                    dog = new Dog("Woof-Woof!");
                    cat = new Cat("Mew?");
                    System.Console.WriteLine($"Dog says: [{dog.anymalSays}]    {dog.GetType()}");
                    System.Console.WriteLine($"Cat says: [{cat.anymalSays}]    {cat.GetType()}");
                    System.Console.WriteLine("IMPLICITLY converting Dog to Cat");

                    cat = (Cat)dog;
                    System.Console.WriteLine($"Cat says: [{cat.anymalSays}]    {cat.GetType()}");
                }
                catch (System.Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(ex.ToString());
                    Console.ResetColor();
                }
            }
            #endregion

            #region [Overflows]
            {
                int i;
                int iMax = int.MaxValue;
                //i = int.MaxValue + 1; //compiler got your back
                i = iMax + 1; //compiler does not care

                unchecked
                {
                    i = int.MaxValue + 1;
                }

                i = unchecked(int.MaxValue + 1);

                try
                {
                    checked { i = iMax + 1; }
                    i = checked(iMax + 1);
                }
                catch (Exception) { }

                
                try
                {
                    uint ui = uint.MaxValue;
                    uint uiMax = uint.MaxValue;
                    ui += 1;
                    ui = checked(uiMax + 1);
                }
                catch (Exception) { }

            }
            #endregion

            #region [Pointers]
            {
                //stolen from MSDN
                // Normal pointer to an object.  
                int[] a = new int[5] { 10, 20, 30, 40, 50 };
                // Must be in unsafe code to use interior pointers.  
                unsafe
                {
                    // Must pin object on heap so that it doesn't move while using interior pointers.  
                    fixed (int* p = &a[0])
                    {
                        // p is pinned as well as object, so create another pointer to show incrementing it.  
                        int* p2 = p;
                        Console.WriteLine(*p2);
                        // Incrementing p2 bumps the pointer by four bytes due to its type ...  
                        p2 += 1;
                        Console.WriteLine(*p2);
                        p2 += 1;
                        Console.WriteLine(*p2);
                        Console.WriteLine("--------");
                        Console.WriteLine(*p);
                        // Deferencing p and incrementing changes the value of a[0] ...  
                        *p += 1;
                        Console.WriteLine(*p);
                        *p += 1;
                        Console.WriteLine(*p);
                    }
                }

                Console.WriteLine("--------");
                Console.WriteLine(a[0]);
            }
            #endregion

            System.Console.WriteLine("Thats All Folks!");
            System.Console.ReadLine();
        }
    }
}
