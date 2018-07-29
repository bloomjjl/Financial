using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Utilities
{
    public class TransactionUtility
    {
        public static int TransactionTypeIdForExpense = 1;
        public static int TransactionTypeIdForIncome = 2;


        public static decimal FormatAmount(int TransactionTypeId, decimal amount)
        {
            if(TransactionTypeId == TransactionTypeIdForExpense)
            {
                return amount * -1;
            }

            return amount;
        }

        /*
        public static decimal CalculateTransaction(decimal initialBalance, decimal transactionAmount, string transactionType)
        {
            if (transactionType == "Income")
            {
                return initialBalance + transactionAmount;
            }
            else if (transactionType == "Expense")
            {
                return initialBalance - transactionAmount;
            }

            return initialBalance;
        }

        public static string FormatCheckNumber(string checkNumber)
        {
            return string.IsNullOrWhiteSpace(checkNumber)
                ? string.Empty
                : checkNumber;
        }

        public static string FormatTransactionNote(string note)
        {
            return string.IsNullOrWhiteSpace(note)
                ? string.Empty
                : note;
        }
        */
    }
}
