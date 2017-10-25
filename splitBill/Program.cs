using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace splitBill
{     
    class Program
    {
        static void Main(string[] args)
        {
            // Get input file name from user
            //Console.Write("Enter Expenses File Name ( eg: Expenses.txt ): ");

            string fileName = "";
            if (args.Length == 1)
            {
                fileName = args[0];
            }
            else
            {
                Console.WriteLine("Expecting file name as argument.");
            }

            try {

                SplitBillBL oSplitBillBL = new SplitBillBL();

                //Read contents from input file
                var arrExpenses = oSplitBillBL.readExpenseFile(fileName);  
                
                //Process the file and generate output
                oSplitBillBL.splitAllBills(arrExpenses, fileName);

                

            }
            catch(FileNotFoundException filenotfound)
            { 
                Console.WriteLine("Error: File not found !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

           Console.ReadKey();            
        }
    }   
}
