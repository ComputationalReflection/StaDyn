using System;

namespace Pybench.Dict
{
    public class Pybench
    {
        public static void Main()
        {            
            Test test = new DictCreation();
            test.test();
            test = new DictWithStringKeys();
            test.test();
            test = new DictWithFloatKeys();
            test.test();
            test = new DictWithIntegerKeys();
            test.test();
            test = new SimpleDictManipulation();
            test.test();            
            Console.WriteLine("Pybench completed!!");            
        }
    }

    public abstract class Test
    {
        public abstract void test();
    }

    public class DictCreation : Test
    {
        public override void test()
        {
            var i = 0;

            var two = 2;
            var three = 3;
            var four = 4;
            var five = 5;
            var six = 6;
            var seven = 7;
            var eight = 8;
            var nine = 9;
            var ten = 10;
            var eleven = 11;

            while (i < 80000)
            {
                var d1 = new var[11];
                var d2 = new var[11];
                var d3 = new var[11];
                var d4 = new var[11];
                var d5 = new var[11];

                d1[1] = two;
                d1[3] = four;
                d1[5] = six;

                d1[2] = three;
                d1[4] = five;
                d1[6] = seven;

                d1[3] = four;
                d1[5] = six;
                d1[7] = eight;

                d1[4] = five;
                d1[6] = seven;
                d1[8] = nine;

                d1[6] = seven;
                d1[8] = nine;
                d1[10] = eleven;

                d1 = new var[11];
                d2 = new var[11];
                d3 = new var[11];
                d4 = new var[11];
                d5 = new var[11];

                d1[1] = two;
                d1[3] = four;
                d1[5] = six;

                d1[2] = three;
                d1[4] = five;
                d1[6] = seven;

                d1[3] = four;
                d1[5] = six;
                d1[7] = eight;

                d1[4] = five;
                d1[6] = seven;
                d1[8] = nine;

                d1[6] = seven;
                d1[8] = nine;
                d1[10] = eleven;

                d1 = new var[11];
                d2 = new var[11];
                d3 = new var[11];
                d4 = new var[11];
                d5 = new var[11];

                d1[1] = two;
                d1[3] = four;
                d1[5] = six;

                d1[2] = three;
                d1[4] = five;
                d1[6] = seven;

                d1[3] = four;
                d1[5] = six;
                d1[7] = eight;

                d1[4] = five;
                d1[6] = seven;
                d1[8] = nine;

                d1[6] = seven;
                d1[8] = nine;
                d1[10] = eleven;

                d1 = new var[11];
                d2 = new var[11];
                d3 = new var[11];
                d4 = new var[11];
                d5 = new var[11];

                d1[1] = two;
                d1[3] = four;
                d1[5] = six;

                d1[2] = three;
                d1[4] = five;
                d1[6] = seven;

                d1[3] = four;
                d1[5] = six;
                d1[7] = eight;

                d1[4] = five;
                d1[6] = seven;
                d1[8] = nine;

                d1[6] = seven;
                d1[8] = nine;
                d1[10] = eleven;

                d1 = new var[11];
                d2 = new var[11];
                d3 = new var[11];
                d4 = new var[11];
                d5 = new var[11];

                d1[1] = two;
                d1[3] = four;
                d1[5] = six;

                d1[2] = three;
                d1[4] = five;
                d1[6] = seven;

                d1[3] = four;
                d1[5] = six;
                d1[7] = eight;

                d1[4] = five;
                d1[6] = seven;
                d1[8] = nine;

                d1[6] = seven;
                d1[8] = nine;
                d1[10] = eleven;

                i++;
            }
        }
    }

    public class DictWithStringKeys : Test
    {
        public static var SIZE = 200;

        private static var Hash(var value)
        {
            var hashVal = 0;
            var c;
            var key = value;
            for (var i = 0; i < key.Length; i++)
            {
                c = (int)key.ToCharArray()[i];
                hashVal = hashVal << 5 ^ c ^ hashVal;
            }
            return hashVal % SIZE;
        }

        public override void test()
        {
            var temp;

            var hashABC = Hash("abc");
            var hashDEF = Hash("def");
            var hashGHI = Hash("ghi");
            var hashJKL = Hash("jkl");
            var hashMNO = Hash("mno");
            var hashPQR = Hash("pqr");

            var one = 1;
            var two = 2;
            var three = 3;
            var four = 4;
            var five = 5;
            var six = 6;

            var d = new var[SIZE];

            for (var i = 0; i < 200000; i++)
            {
                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];
            }
        }
    }

    public class DictWithFloatKeys : Test
    {
        public static var SIZE = 50;

        public static var Hash(var value)
        {
            var val = value.GetHashCode();
            if (val < 0)
                return (0 - val) % SIZE;
            return val % SIZE;
        }

        public override void test()
        {
            var temp;

            var hash1234 = Hash(1.234);
            var hash2345 = Hash(2.345);
            var hash3456 = Hash(3.456);
            var hash4567 = Hash(4.567);
            var hash5678 = Hash(5.678);
            var hash6789 = Hash(6.789);

            var one = 1;
            var two = 2;
            var three = 3;
            var four = 4;
            var five = 5;
            var six = 6;

            var d = new var[SIZE];

            for (var i = 0; i < 150000; i++)
            {
                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];
            }
        }
    }

    public class DictWithIntegerKeys : Test
    {
        public override void test()
        {
            var temp;

            var one = 1;
            var two = 2;
            var three = 3;
            var four = 4;
            var five = 5;
            var six = 6;

            var d = new var[7];

            for (var i = 0; i < 200000; i++)
            {
                d[1] = one;
                d[2] = two;
                d[3] = three;
                d[4] = four;
                d[5] = five;
                d[6] = six;

                temp = d[1];
                temp = d[2];
                temp = d[3];
                temp = d[4];
                temp = d[5];
                temp = d[6];

                d[1] = one;
                d[2] = two;
                d[3] = three;
                d[4] = four;
                d[5] = five;
                d[6] = six;

                temp = d[1];
                temp = d[2];
                temp = d[3];
                temp = d[4];
                temp = d[5];
                temp = d[6];

                d[1] = one;
                d[2] = two;
                d[3] = three;
                d[4] = four;
                d[5] = five;
                d[6] = six;

                temp = d[1];
                temp = d[2];
                temp = d[3];
                temp = d[4];
                temp = d[5];
                temp = d[6];

                d[1] = one;
                d[2] = two;
                d[3] = three;
                d[4] = four;
                d[5] = five;
                d[6] = six;

                temp = d[1];
                temp = d[2];
                temp = d[3];
                temp = d[4];
                temp = d[5];
                temp = d[6];

                d[1] = one;
                d[2] = two;
                d[3] = three;
                d[4] = four;
                d[5] = five;
                d[6] = six;

                temp = d[1];
                temp = d[2];
                temp = d[3];
                temp = d[4];
                temp = d[5];
                temp = d[6];
            }
        }
    }

    public class SimpleDictManipulation : Test
    {
        private var HasKey(var[] d, var key)
        {
            if (key >= d.Length)
                return false;
            return d[key] != 0;
        }

        public override void test()
        {
            var x;

            var zero = 0;
            var one = 1;
            var two = 2;
            var three = 3;
            var four = 4;
            var five = 5;
            var six = 6;
            var eight = 8;
            var ten = 10;

            var[] d = new var[6];

            for (var i = 0; i < 100000; i++)
            {
                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;
            }
        }
    }
}