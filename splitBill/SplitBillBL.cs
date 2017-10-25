using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splitBill
{
    public struct Participants
    {
        public decimal expenses;
        public decimal pending;
    }

    public class SplitBillBL
    {

        public string[] readExpenseFile(string fileName)
        {
            var arrExpenses = File.ReadAllLines(Environment.CurrentDirectory + "/" + fileName);
            arrExpenses = arrExpenses.Where(exp => !string.IsNullOrEmpty(exp)).ToArray();
            return arrExpenses;
        }
        
        public string splitAllBills(string[] arrExpenses, string fileName)
        {
            int currlineIndex = 0;
            int partCount = 0;
            List<Participants[]> lstParticipants = new List<Participants[]>();
            if (arrExpenses.Length > 0)
            {
                //Read and calculate shares for each trip
                partCount = Convert.ToInt32(arrExpenses[currlineIndex]);
                while (partCount != 0 && currlineIndex < arrExpenses.Length)
                {
                    Participants[] participants = getExepnses(arrExpenses, ref currlineIndex, partCount);
                    calculateSharePerPerson(ref participants);
                    lstParticipants.Add(participants);

                    currlineIndex++;
                    partCount = Convert.ToInt32(arrExpenses[currlineIndex]);
                }
            }
            //Write out to a text file in the same directory
            return writeOutput(lstParticipants, fileName);
        }

        //Calculates each trip costs and how much each member owes
        public void calculateSharePerPerson(ref Participants[] participants)
        {
            decimal totalExpenseForTrip = 0;
            decimal sharePerPerson = 0;

            for (int i = 0; i < participants.Count(); i++)
            {
                totalExpenseForTrip = totalExpenseForTrip + participants[i].expenses;
            }
            sharePerPerson = totalExpenseForTrip / participants.Count();
            for (int i = 0; i < participants.Count(); i++)
            {
                participants[i].pending = Math.Round((sharePerPerson - participants[i].expenses), 2);
            }
        }

        // Read all expenses from array and construct the Participant array with expenses.
        public Participants[] getExepnses(string[] arrExpenses, ref int curreLineIndex, int partCount)
        {
            int expCount = 0;
            decimal totalExp = 0;
            Participants[] part = new Participants[partCount];
            for (int i = 0; i < partCount; i++)
            {
                curreLineIndex++;
                expCount = Convert.ToInt32(arrExpenses[curreLineIndex]);
                for (int j = 0; j < expCount; j++)
                {
                    curreLineIndex++;
                    totalExp = totalExp + Convert.ToDecimal(arrExpenses[curreLineIndex]);
                }
                part[i].expenses = totalExp;
                totalExp = 0;
            }

            return part;
        }

        // Format output values and creates the output file
        public string writeOutput(List<Participants[]> participants, string fileName)
        {
            StringBuilder strOut = new StringBuilder();
            foreach (Participants[] part in participants)
            {
                for (int i = 0; i < part.Count(); i++)
                {
                    if (part[i].pending < 0)
                        strOut.Append("($" + Math.Abs(part[i].pending) + ")" + Environment.NewLine);
                    else
                        strOut.Append("$" + part[i].pending + Environment.NewLine);
                }
                strOut.Append(Environment.NewLine);
            }

            using (StreamWriter outputFile = new StreamWriter(Environment.CurrentDirectory + "/" + fileName + ".out"))
            {
                outputFile.WriteLine(Convert.ToString(strOut).TrimEnd());
            }

            return Convert.ToString(strOut).TrimEnd();

        }       
       
    }
}
