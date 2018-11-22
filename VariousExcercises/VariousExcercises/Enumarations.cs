using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VariousExcercises
{
    [TestClass]
    public class Enumarations
    {
        [TestMethod]
        [DataRow(ActionTypeEnum.Active)]
        [DataRow(ActionTypeEnum.Inactive)]
        public void PrintEnumValue(ActionTypeEnum actionType)
        {
            int value = (int)actionType;

            Debug.WriteLine($"Enum value: {value}");
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Print(int i)
        {
            Debug.WriteLine(i);
        }
    }

    public enum ActionTypeEnum
    {
        Create = 1,
        Active = 2,
        Inactive = 3
    }
}
