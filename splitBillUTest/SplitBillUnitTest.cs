using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using splitBill;

namespace splitBillUTest
{
    [TestClass]
    public class SplitBillUnitTest
    {
        // This u

        [TestMethod]
        public void StandardInput()
        {
            splitBill.SplitBillBL oTestSplitBillBL = new SplitBillBL();
            //var arrExpenses = oTestSplitBillBL.readExpenseFile("Expenses.txt");
            string[] arrExpenses = { "3","2","10.00","20.00","4","15.00","15.01", "3.00", 
                    "3.01","3","5.00","9.00","4.00","2", "2",  "8.00",  "6.00",  "2", "9.20", "6.75","0" };
            string output = oTestSplitBillBL.splitAllBills(arrExpenses, "Expenses.txt");
            string expected = "($1.99)" + Environment.NewLine + "($8.01)" + Environment.NewLine + "$10.01" + Environment.NewLine + Environment.NewLine + "$0.98" + Environment.NewLine + "($0.98)";
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void EmptyFile()
        {
            splitBill.SplitBillBL oTestSplitBillBL = new SplitBillBL();
            string[] arrExpenses = {  }; //oTestSplitBillBL.readExpenseFile("Empty.txt");
            string output = oTestSplitBillBL.splitAllBills(arrExpenses, "Empty.txt");
            string expected = "";
            Assert.AreEqual(expected, output.TrimEnd());
        }

        [TestMethod]
        public void ZeroOnly()
        {
            splitBill.SplitBillBL oTestSplitBillBL = new SplitBillBL();
            string[] arrExpenses = { "0" };// oTestSplitBillBL.readExpenseFile("ZeroOnly.txt");
            string output = oTestSplitBillBL.splitAllBills(arrExpenses, "ZeroOnly.txt");
            string expected = "";
            Assert.AreEqual(expected, output.TrimEnd());
        }
    }
}
