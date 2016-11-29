using System;
using System.Collections.Generic;
using DynamicVisualizer.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicVisualizerTest
{
    [TestClass]
    public class DataStorageTest
    {
        [TestMethod]
        public void TestConstantScalar()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
        }

        [TestMethod]
        public void TestConstantScalarLong()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "55,5555555555556"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 55.5555555555556,
                double.Epsilon);
        }

        [TestMethod]
        public void TestMathExpressionWithConstantScalar()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            DataStorage.Add(new ScalarExpression("data", "var2", "data.var*2"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var2").CachedValue.AsDouble, 123.0*2, double.Epsilon);
        }

        [TestMethod]
        public void TestComplexMathExpressionWithConstantScalar()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            DataStorage.Add(new ScalarExpression("data", "var2", "data.var - ( 1 + ( ( 2.0 + 3 ) * ( 4 * 5.0 ) ) )"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var2").CachedValue.AsDouble,
                123.0 - (1 + (2.0 + 3)*(4*5.0)), double.Epsilon);
        }

        [TestMethod]
        public void TestMathUnaryOpWithConstantScalar()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var1", "-123"));
            DataStorage.Add(new ScalarExpression("data", "var2", "28*(-11)"));
            //DataStorage.Add("data", "var3", "28 * -11");

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var1").CachedValue.AsDouble, -123, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var2").CachedValue.AsDouble, 28*-11, double.Epsilon);
            //Assert.AreEqual(DataStorage.GetScalarExpression("data.var3").CachedValue.AsDouble, 28 * -11, double.Epsilon);
        }

        [TestMethod]
        public void TestObjectNotSpecified()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            DataStorage.Add(new ScalarExpression("data", "var2", "var*2"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var2").CachedValue.AsDouble, 123.0*2, double.Epsilon);
        }

        [TestMethod]
        public void TestConstantChanged()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            DataStorage.Add(new ScalarExpression("data", "var2", "data.var*2"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var2").CachedValue.AsDouble, 123.0*2, double.Epsilon);

            DataStorage.GetScalarExpression("data.var").SetRawExpression("10");

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 10.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var2").CachedValue.AsDouble, 10.0*2, double.Epsilon);
        }

        [TestMethod]
        public void TestTextExpressionWithConstant()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            DataStorage.Add(new ScalarExpression("data", "str", "var + \" is 123\""));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.str").CachedValue.AsString, "123 is 123");
        }

        [TestMethod]
        public void TestFuncExpressionWithConstant()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "1024"));
            DataStorage.Add(new ScalarExpression("data", "varsqrt", "sqrt(var)"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 1024.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.varsqrt").CachedValue.AsDouble, 32.0, double.Epsilon);
        }

        [TestMethod]
        public void TestFuncExpressionWithMathExpression()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "1024"));
            DataStorage.Add(new ScalarExpression("data", "vardiv", "data.var/4"));
            DataStorage.Add(new ScalarExpression("data", "vardivsqrt", "sqrt(vardiv)"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 1024.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.vardiv").CachedValue.AsDouble, 1024.0/4,
                double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.vardivsqrt").CachedValue.AsDouble, Math.Sqrt(1024.0/4),
                double.Epsilon);
        }

        [TestMethod]
        public void TestConstantArray()
        {
            DataStorage.WipeNonDataFields();

            var arr = new[] {"1", "2", "3", "4", "5", "6", "7"};

            DataStorage.Add(new ArrayExpression("data", "item", arr));
            var a = DataStorage.GetArrayExpression("data.item");
            Assert.AreEqual(arr.Length, a.Exprs.Length);
            for (var i = 0; i < a.Exprs.Length; ++i)
                Assert.AreEqual(a.Exprs[i].CachedValue.AsDouble + "", arr[i]);
        }

        [TestMethod]
        public void TestMathExpressionWithConstantArray()
        {
            DataStorage.WipeNonDataFields();

            var arr = new[] {"1", "2", "3", "4", "5", "6", "7"};

            DataStorage.Add(new ArrayExpression("data", "item", arr));
            var a = DataStorage.GetArrayExpression("data.item");
            Assert.AreEqual(arr.Length, a.Exprs.Length);
            for (var i = 0; i < a.Exprs.Length; ++i)
                Assert.AreEqual(a.Exprs[i].CachedValue.AsDouble + "", arr[i]);

            DataStorage.Add(new ArrayExpression("data", "item2", "data.item*2", a.Exprs.Length));
            var b = DataStorage.GetArrayExpression("data.item2");
            Assert.AreEqual(arr.Length, b.Exprs.Length);
            for (var i = 0; i < b.Exprs.Length; ++i)
                Assert.AreEqual(b.Exprs[i].CachedValue.AsDouble/2 + "", arr[i]);
        }

        [TestMethod]
        public void TestFuncScalarExpressionWithMathExpressionArray()
        {
            DataStorage.WipeNonDataFields();

            var arr = new[] {"1", "2", "3", "4", "5", "6", "7"};

            DataStorage.Add(new ArrayExpression("data", "item", arr));
            var a = DataStorage.GetArrayExpression("data.item");
            Assert.AreEqual(arr.Length, a.Exprs.Length);
            for (var i = 0; i < a.Exprs.Length; ++i)
                Assert.AreEqual(a.Exprs[i].CachedValue.AsDouble + "", arr[i]);

            DataStorage.Add(new ArrayExpression("data", "item2", "data.item*2", a.Exprs.Length));
            var b = DataStorage.GetArrayExpression("data.item2");
            Assert.AreEqual(arr.Length, b.Exprs.Length);
            for (var i = 0; i < b.Exprs.Length; ++i)
                Assert.AreEqual(b.Exprs[i].CachedValue.AsDouble/2 + "", arr[i]);

            DataStorage.Add(new ScalarExpression("data", "arrlen", "len(data.item2)"));
            Assert.AreEqual(DataStorage.GetScalarExpression("data.arrlen").CachedValue.AsDouble, arr.Length,
                double.Epsilon);
        }

        [TestMethod]
        public void TestFuncScalarExpressionWithMathExpressionArrayChanged()
        {
            DataStorage.WipeNonDataFields();

            var arr = new[] {"1", "2", "3", "4", "5", "6", "7"};

            DataStorage.Add(new ArrayExpression("data", "item", arr));
            var a = DataStorage.GetArrayExpression("data.item");
            Assert.AreEqual(arr.Length, a.Exprs.Length);
            for (var i = 0; i < a.Exprs.Length; ++i)
                Assert.AreEqual(a.Exprs[i].CachedValue.AsDouble + "", arr[i]);

            DataStorage.Add(new ArrayExpression("data", "item2", "data.item*2", a.Exprs.Length));
            var b = DataStorage.GetArrayExpression("data.item2");
            Assert.AreEqual(arr.Length, b.Exprs.Length);
            for (var i = 0; i < b.Exprs.Length; ++i)
                Assert.AreEqual(b.Exprs[i].CachedValue.AsDouble/2 + "", arr[i]);

            DataStorage.Add(new ScalarExpression("data", "arrlen", "len(data.item2)"));
            Assert.AreEqual(DataStorage.GetScalarExpression("data.arrlen").CachedValue.AsDouble, arr.Length,
                double.Epsilon);

            DataStorage.Add(new ScalarExpression("data", "arrmean", "mean(data.item2)"));
            // mean (2, 4, 6, 8, 10, 12, 14) = 8
            Assert.AreEqual(DataStorage.GetScalarExpression("data.arrmean").CachedValue.AsDouble, 8, double.Epsilon);

            a.Exprs[5].SetRawExpression("97");
            arr[5] = "97";

            // mean (2, 4, 6, 8, 10, 194, 14) = 34
            Assert.AreEqual(DataStorage.GetScalarExpression("data.arrmean").CachedValue.AsDouble, 34, double.Epsilon);

            for (var i = 0; i < a.Exprs.Length; ++i)
                Assert.AreEqual(a.Exprs[i].CachedValue.AsDouble + "", arr[i]);

            for (var i = 0; i < b.Exprs.Length; ++i)
                Assert.AreEqual(b.Exprs[i].CachedValue.AsDouble/2 + "", arr[i]);

            DataStorage.Add(new ScalarExpression("data", "arrmax", "max(data.item2)"));
            // max (2, 4, 6, 8, 10, 194, 14) = 194
            Assert.AreEqual(DataStorage.GetScalarExpression("data.arrmax").CachedValue.AsDouble, 194, double.Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestRemoveByName()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            DataStorage.Remove("data.var");
            DataStorage.GetScalarExpression("data.var");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestRemoveByValue()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            DataStorage.Remove(DataStorage.GetScalarExpression("data.var"));
            DataStorage.GetScalarExpression("data.var");
        }

        [TestMethod]
        public void TestCanBeRemoved()
        {
            DataStorage.WipeNonDataFields();

            DataStorage.Add(new ScalarExpression("data", "var", "123"));
            DataStorage.Add(new ScalarExpression("data", "var2", "data.var*2"));

            Assert.AreEqual(DataStorage.GetScalarExpression("data.var").CachedValue.AsDouble, 123.0, double.Epsilon);
            Assert.AreEqual(DataStorage.GetScalarExpression("data.var2").CachedValue.AsDouble, 123.0*2, double.Epsilon);

            Assert.IsFalse(DataStorage.GetScalarExpression("data.var").CanBeRemoved);
            Assert.IsTrue(DataStorage.GetScalarExpression("data.var2").CanBeRemoved);
        }
    }
}