﻿using System;
using System.Diagnostics;
using NUnit.Framework;
using Vietbait.Lablink.TestResult;

namespace Vietbait.Lablink.Devices.Immunology
{
    [TestFixture]
    internal class _CobasE411Test
    {
        [Test]
        public void HeaderTest()
        {
            try
            {
                const string rawHeader = @"H|\^&|||cobas6000^1|||||host|RSUPL^BATCH|P|1";
                var oldHeader = new CobasE411HeaderRecord();
                oldHeader.Parse(rawHeader);
                //h.Parse(rawHeader);
                Debug.WriteLine(oldHeader.RecordType);
                var newHeader = (CobasE411HeaderRecord) oldHeader.Clone();
                newHeader.ReveiverId.Data = oldHeader.SenderNameOrId.Data;
                newHeader.SenderNameOrId.Data = oldHeader.ReveiverId.Data;
                Debug.WriteLine(newHeader.Create());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        [Test]
        public void OrderTest()
        {
            try
            {
                const string rawHeader = @"Q|1|^^000663^32^@7^2^^S1^SC ||ALL||||||||O";
                var order = new CobasE411TestOrderRecord(rawHeader);
                Debug.WriteLine(order.Create());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        [Test]
        public void PatientInforTest()
        {
            try
            {
                var newHeader = new CobasE411PatientInformationRecord(@"P|1|||||||M||||||35^Y");
                var tempP = new CobasE411PatientInformationRecord(@"P|1|||||||M||||||35^Y||||||||||||||||||||");
                Debug.WriteLine(tempP.Create());
                Debug.WriteLine(newHeader.Create());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        [Test]
        public void QueryTest()
        {
            try
            {
                var newHeader = new CobasE411RequestInformationRecord(@"Q|1|^^000663^32^@7^2^^S1^SC ||ALL||||||||O");
                //var tempP = new CobasE411PatientInformationRecord(@"P|1|||||||M||||||35^Y||||||||||||||||||||");
                //Debug.WriteLine(tempP.Create());
                Debug.WriteLine(newHeader.GetOrderBarcode());
                Debug.WriteLine(newHeader.Create());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        [Test]
        public void ResultTest()
        {
            try
            {
                //var newHeader = new CobasE411ResultRecord(@"R|1|^^^2/1/not|8.60|nmol/L||N||F||BMSERV|||E11");
                var newHeader =
                    new CobasE411ResultRecord(@"R|1|^^^64/1/not|46.22|IU/L||H||F||BMSERV|20050915150011||E11");

                //Debug.WriteLine(tempP.Create());
                Debug.WriteLine(newHeader.Create());
                ResultItem resultItem = newHeader.GetResult();
                Debug.WriteLine(resultItem.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        [Test]
        public void TerminationTest()
        {
            try
            {
                var newHeader = new CobasE411TerminationRecord();
                Debug.WriteLine(newHeader.Create());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}